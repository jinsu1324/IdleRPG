using Firebase.Database;
using Newtonsoft.Json; // Newtonsoft.Json 사용
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 세이브할 필드에만 붙일 어트리뷰트
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Field)]
public class SaveField : System.Attribute 
{ 

}

/// <summary>
/// 세이브 로드 관리
/// </summary>
public class SaveLoadManager : SingletonBase<SaveLoadManager>
{
    private string _userID = "jinsu";               // Firebase에서 사용할 사용자 ID
    private DatabaseReference _databaseReference;   // 데이터베이스 레퍼런스
    private List<ISavable> _managerList;            // 저장할 매니저 리스트

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // 저장할 모든 매니저를 리스트에 등록
        _managerList = new List<ISavable>
        {
            new GoldManager(),
            new GemManager()
            // 여기에 다른 매니저를 추가
        };
    }

    /// <summary>
    /// 데이터 저장 (ISavable을 상속받은 클래스의 SaveField가 붙은 필드만 저장)
    /// </summary>
    public async Task SaveAsync(ISavable savable)
    {
        // SaveField 어트리뷰트들만 가져오기
        var fields = savable.GetType()
                            .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                            .Where(field => System.Attribute.IsDefined(field, typeof(SaveField))); 

        // 필드이름 - 필드값 딕셔너리 형태로 변환
        var data = new Dictionary<string, object>();
        foreach (var field in fields)
            data[field.Name] = field.GetValue(savable); 

        // Firebase에 저장
        string json = JsonConvert.SerializeObject(data);
        await _databaseReference.Child("users").Child(_userID).Child(savable.Key).SetRawJsonValueAsync(json);

        Debug.Log($"저장 완료! {savable.Key} : {json}");
    }

    /// <summary>
    /// 데이터 로드 (저장된 데이터를 해당 클래스의 필드에 덮어씀)
    /// </summary>
    public async Task LoadAsync(ISavable savable)
    {
        var dataSnapshot = await _databaseReference.Child("users").Child(_userID).Child(savable.Key).GetValueAsync();

        if (dataSnapshot.Exists)
        {
            // 가져온 데이터 역직렬화
            string json = dataSnapshot.GetRawJsonValue();
            Dictionary<string, object> loadedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            // SaveField 어트리뷰트들만 가져오기
            var fields = savable.GetType()
                                .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                                .Where(field => System.Attribute.IsDefined(field, typeof(SaveField))); 

            // 가져온 데이터를 필드에 덮어씌움
            foreach (var field in fields)
            {
                if (loadedData.TryGetValue(field.Name, out object value))
                    field.SetValue(savable, Convert.ChangeType(value, field.FieldType));
            }

            Debug.Log($"불러오기 완료! {savable.Key} : {json}");
        }
        else
            Debug.Log($"{savable.Key} 데이터를 찾을 수 없습니다.");
    }

    /// <summary>
    /// 데이터 제거
    /// </summary>
    public async Task RemoveAsync(ISavable savable)
    {
        var reference = _databaseReference.Child("users").Child(_userID).Child(savable.Key);
        await reference.RemoveValueAsync(); // Firebase 경로 데이터 삭제

        Debug.Log($"제거완료! {savable.Key}.");
    }

    /// <summary>
    /// 모든데이터 제거
    /// </summary>
    public async Task RemoveAllAsync()
    {
        var reference = _databaseReference.Child("users").Child(_userID);
        await reference.RemoveValueAsync(); // 사용자 전체 데이터 삭제

        Debug.Log("전체 users 데이터 제거 완료!.");
    }



    #region 저장, 불러오기, 제거 버튼들
    /// <summary>
    /// 데이터 저장 버튼
    /// </summary>
    public async void OnClickSaveButton()
    {
        foreach (var manager in _managerList)
        {
            await SaveAsync(manager);
        }

        Debug.Log("전체 데이터 저장 완료!(버튼)");
    }

    /// <summary>
    /// 데이터 불러오기 버튼
    /// </summary>
    public async void OnClickLoadButton()
    {
        foreach (var manager in _managerList)
        {
            await LoadAsync(manager);
            manager.NotifyLoaded(); // 로드 후 이벤트 호출
        }
        
        Debug.Log($"전체 데이터 불러오기 완료!(버튼)");
    }

    /// <summary>
    /// 데이터 제거 버튼
    /// </summary>
    public async void OnClickRemoveButton()
    {
        foreach (var manager in _managerList)
        {
            await RemoveAsync(manager);
        }

        Debug.Log("전체 데이터 제거 완료!(버튼)");
    }
    #endregion
}
