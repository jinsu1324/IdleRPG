using Firebase.Database;
using Newtonsoft.Json; // Newtonsoft.Json ���
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class SaveField : System.Attribute
{
}

public class SaveLoadManager : SingletonBase<SaveLoadManager>
{
    private DatabaseReference _databaseReference;
    private string _userID = "jinsu"; // Firebase���� ����� ����� ID

    private List<ISavable> _managers; // �Ŵ��� ����Ʈ


    protected override void Awake()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // ��� �Ŵ����� ����Ʈ�� ���
        _managers = new List<ISavable>
        {
            new GoldManager(),
            new GemManager()
            // ���⿡ �ٸ� �Ŵ����� �߰�
        };
    }

    /// <summary>
    /// ����: ISavable�� ��ӹ��� Ŭ������ SaveField�� ���� �ʵ常 ����
    /// </summary>
    public async Task SaveAsync(ISavable savable)
    {
        var fields = savable.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(field => System.Attribute.IsDefined(field, typeof(SaveField))); // SaveField ��Ʈ����Ʈ�鸸 ����

        var data = new Dictionary<string, object>();

        // ������ ����
        foreach (var field in fields)
        {
            data[field.Name] = field.GetValue(savable); // �ʵ� �̸��� ���� Dictionary�� �߰�
        }

        // Newtonsoft.Json�� ����Ͽ� �����͸� JSON���� ����ȭ
        string json = JsonConvert.SerializeObject(data);

        // Firebase�� ����
        await _databaseReference.Child("users").Child(_userID).Child(savable.Key).SetRawJsonValueAsync(json);

        Debug.Log($"Saved {savable.Key}: {json}");
    }

    /// <summary>
    /// �ε�: ����� �����͸� �ش� Ŭ������ �ʵ忡 ���
    /// </summary>
    public async Task LoadAsync(ISavable savable)
    {
        var dataSnapshot = await _databaseReference.Child("users").Child(_userID).Child(savable.Key).GetValueAsync();

        if (dataSnapshot.Exists)
        {
            string json = dataSnapshot.GetRawJsonValue();

            // JSON�� Dictionary<string, object>�� ������ȭ
            var loadedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            var fields = savable.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(field => System.Attribute.IsDefined(field, typeof(SaveField))); // SaveField ��Ʈ����Ʈ�鸸 ����

            // ������ ����
            foreach (var field in fields)
            {
                if (loadedData.TryGetValue(field.Name, out var value))
                {
                    // �ʵ� Ÿ�Կ� �°� �� ��ȯ �� ����
                    field.SetValue(savable, Convert.ChangeType(value, field.FieldType));
                }
            }

            Debug.Log($"Loaded {savable.Key}: {json}");
        }
        else
        {
            Debug.LogWarning($"{savable.Key} data not found.");
        }
    }

    /// <summary>
    /// ������ ����: Ư�� ISavable ������ ����
    /// </summary>
    public async Task RemoveAsync(ISavable savable)
    {
        var reference = _databaseReference.Child("users").Child(_userID).Child(savable.Key);

        await reference.RemoveValueAsync(); // Firebase ��� ������ ����

        Debug.Log($"Removed {savable.Key} data.");
    }

    /// <summary>
    /// ��� ������ ����
    /// </summary>
    public async Task RemoveAllAsync()
    {
        var reference = _databaseReference.Child("users").Child(_userID);

        await reference.RemoveValueAsync(); // ����� ��ü ������ ����

        Debug.Log("All user data removed.");
    }

    /// <summary>
    /// ��ü �����͸� ����
    /// </summary>
    public async void SaveAll()
    {
        foreach (var manager in _managers)
        {
            await SaveAsync(manager);
        }

        Debug.Log("All data saved!");
    }

    /// <summary>
    /// ��ü �����͸� �ε�
    /// </summary>
    public async void LoadAll()
    {
        foreach (var manager in _managers)
        {
            await LoadAsync(manager);
            manager.NotifyLoaded(); // �ε� �� �̺�Ʈ ȣ��
        }
        
        Debug.Log($"All Data Loaded!");
    }

    /// <summary>
    /// ��ü ������ ����
    /// </summary>
    public async void RemoveAll()
    {
        foreach (var manager in _managers)
        {
            await RemoveAsync(manager);
        }

        Debug.Log("All  data removed!");
    }
}
