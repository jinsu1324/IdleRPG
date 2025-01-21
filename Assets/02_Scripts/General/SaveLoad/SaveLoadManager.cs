using Firebase.Database;
using Newtonsoft.Json; // Newtonsoft.Json ���
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// ���̺��� �ʵ忡�� ���� ��Ʈ����Ʈ
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class SaveField : Attribute
{

}

/// <summary>
/// ���̺� �ε� ����
/// </summary>
public class SaveLoadManager : SingletonBase<SaveLoadManager>
{
    private string _userID = "jinsu";               // Firebase���� ����� ����� ID
    private DatabaseReference _databaseReference;   // �����ͺ��̽� ���۷���
    private List<ISavable> _managerList;            // ������ �Ŵ��� ����Ʈ

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // ������ ��� �Ŵ����� ����Ʈ�� ���
        _managerList = new List<ISavable>
        {
            //new GoldManager(),
            //new GemManager(),
            new ItemInven()
            // ���⿡ �ٸ� �Ŵ����� �߰�
        };
    }

    /// <summary>
    /// ������ ���� (ISavable�� ��ӹ��� Ŭ������ SaveField�� ���� �ʵ常 ����)
    /// </summary>
    public async Task SaveAsync(ISavable savable)
    {
        // SaveField ��Ʈ����Ʈ�鸸 ��������
        var fields = savable.GetType()
                            .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                            .Where(field => Attribute.IsDefined(field, typeof(SaveField)));

        // JSON ����ȭ�� ���� �ʵ�� �� ����
        var data = new Dictionary<string, object>();
        foreach (var field in fields)
            data[field.Name] = field.GetValue(savable);

        // JSON ����ȭ �� Firebase�� ����
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        await _databaseReference.Child("users").Child(_userID).Child(savable.Key).SetRawJsonValueAsync(json);

        Debug.Log($"���� �Ϸ�! {savable.Key} : {json}");
    }

    /// <summary>
    /// ������ �ε� (����� �����͸� �ش� Ŭ������ �ʵ忡 ���)
    /// </summary>
    public async Task LoadAsync(ISavable savable)
    {
        var dataSnapshot = await _databaseReference.Child("users").Child(_userID).Child(savable.Key).GetValueAsync();

        // �����ͽ����� ��ã������ �׳� ����
        if (!dataSnapshot.Exists)
        {
            Debug.LogWarning($"{savable.Key} �����͸� ã�� �� �����ϴ�.");
            return;
        }

        // ����� ������ ��������
        string json = dataSnapshot.GetRawJsonValue();
        var loadedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);


        // SaveField ��Ʈ����Ʈ�� ���� �ʵ常 ��������
        var fields = savable.GetType()
                            .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                            .Where(field => Attribute.IsDefined(field, typeof(SaveField)));

        // �ʵ尪�� �̸� ��Ī�ؼ� ������ ������
        foreach (var field in fields)
        {
            if (loadedData.TryGetValue(field.Name, out var value))
            {
                try
                {
                    // ������ Ÿ�Կ� ���� ������ ��ȯ
                    var convertedValue = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(value), field.FieldType);
                    field.SetValue(savable, convertedValue);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"�ʵ� '{field.Name}' �ε� �� ���� �߻�: {ex.Message}");
                }
            }
        }

        Debug.Log($"�ҷ����� �Ϸ�! {savable.Key} : {json}");
    }

    
    
    
    
    
    
    /// <summary>
    /// ������ ����
    /// </summary>
    public async Task RemoveAsync(ISavable savable)
    {
        var reference = _databaseReference.Child("users").Child(_userID).Child(savable.Key);
        await reference.RemoveValueAsync(); // Firebase ��� ������ ����

        Debug.Log($"���ſϷ�! {savable.Key}.");
    }

    /// <summary>
    /// ��絥���� ����
    /// </summary>
    public async Task RemoveAllAsync()
    {
        var reference = _databaseReference.Child("users").Child(_userID);
        await reference.RemoveValueAsync(); // ����� ��ü ������ ����

        Debug.Log("��ü users ������ ���� �Ϸ�!.");
    }










    #region ����, �ҷ�����, ���� ��ư��
    /// <summary>
    /// ������ ���� ��ư
    /// </summary>
    public async void OnClickSaveButton()
    {
        foreach (var manager in _managerList)
        {
            await SaveAsync(manager);
        }

        Debug.Log("��ü ������ ���� �Ϸ�!(��ư)");

        //DataSaveJinsu();
    }

    /// <summary>
    /// ������ �ҷ����� ��ư
    /// </summary>
    public async void OnClickLoadButton()
    {
        foreach (var manager in _managerList)
        {
            await LoadAsync(manager);
            //manager.NotifyLoaded(); // �ε� �� �̺�Ʈ ȣ��
        }

        Debug.Log($"��ü ������ �ҷ����� �Ϸ�!(��ư)");

        //DataLoadJinsu();
    }

    /// <summary>
    /// ������ ���� ��ư
    /// </summary>
    public async void OnClickRemoveButton()
    {
        foreach (var manager in _managerList)
        {
            await RemoveAsync(manager);
        }

        Debug.Log("��ü ������ ���� �Ϸ�!(��ư)");
    }
    #endregion
}
