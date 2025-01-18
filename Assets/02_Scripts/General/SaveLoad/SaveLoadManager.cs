using Firebase.Database;
using Newtonsoft.Json; // Newtonsoft.Json 사용
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
    private string _userID = "jinsu"; // Firebase에서 사용할 사용자 ID

    private List<ISavable> _managers; // 매니저 리스트


    protected override void Awake()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // 모든 매니저를 리스트에 등록
        _managers = new List<ISavable>
        {
            new GoldManager(),
            new GemManager()
            // 여기에 다른 매니저를 추가
        };
    }

    /// <summary>
    /// 저장: ISavable을 상속받은 클래스의 SaveField가 붙은 필드만 저장
    /// </summary>
    public async Task SaveAsync(ISavable savable)
    {
        var fields = savable.GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
            .Where(field => System.Attribute.IsDefined(field, typeof(SaveField))); // SaveField 어트리뷰트들만 선택

        var data = new Dictionary<string, object>();

        // 데이터 수집
        foreach (var field in fields)
        {
            data[field.Name] = field.GetValue(savable); // 필드 이름과 값을 Dictionary에 추가
        }

        // Newtonsoft.Json을 사용하여 데이터를 JSON으로 직렬화
        string json = JsonConvert.SerializeObject(data);

        // Firebase에 저장
        await _databaseReference.Child("users").Child(_userID).Child(savable.Key).SetRawJsonValueAsync(json);

        Debug.Log($"Saved {savable.Key}: {json}");
    }

    /// <summary>
    /// 로드: 저장된 데이터를 해당 클래스의 필드에 덮어씀
    /// </summary>
    public async Task LoadAsync(ISavable savable)
    {
        var dataSnapshot = await _databaseReference.Child("users").Child(_userID).Child(savable.Key).GetValueAsync();

        if (dataSnapshot.Exists)
        {
            string json = dataSnapshot.GetRawJsonValue();

            // JSON을 Dictionary<string, object>로 역직렬화
            var loadedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            var fields = savable.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(field => System.Attribute.IsDefined(field, typeof(SaveField))); // SaveField 어트리뷰트들만 선택

            // 데이터 복원
            foreach (var field in fields)
            {
                if (loadedData.TryGetValue(field.Name, out var value))
                {
                    // 필드 타입에 맞게 값 변환 후 설정
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
    /// 데이터 제거: 특정 ISavable 데이터 제거
    /// </summary>
    public async Task RemoveAsync(ISavable savable)
    {
        var reference = _databaseReference.Child("users").Child(_userID).Child(savable.Key);

        await reference.RemoveValueAsync(); // Firebase 경로 데이터 삭제

        Debug.Log($"Removed {savable.Key} data.");
    }

    /// <summary>
    /// 모든 데이터 제거
    /// </summary>
    public async Task RemoveAllAsync()
    {
        var reference = _databaseReference.Child("users").Child(_userID);

        await reference.RemoveValueAsync(); // 사용자 전체 데이터 삭제

        Debug.Log("All user data removed.");
    }

    /// <summary>
    /// 전체 데이터를 저장
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
    /// 전체 데이터를 로드
    /// </summary>
    public async void LoadAll()
    {
        foreach (var manager in _managers)
        {
            await LoadAsync(manager);
            manager.NotifyLoaded(); // 로드 후 이벤트 호출
        }
        
        Debug.Log($"All Data Loaded!");
    }

    /// <summary>
    /// 전체 데이터 제거
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
