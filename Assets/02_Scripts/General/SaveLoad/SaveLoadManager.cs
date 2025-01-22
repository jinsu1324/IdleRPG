using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 세이브할 데이터에 붙일 어트리뷰트 (필드, 프로퍼티, 메소드 가능)
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
public class SaveField : Attribute { }

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
            new GemManager(),
            //new CurrentStageData(),
            //new ItemInven(),
            //new EquipGearManager(),
            //new EquipSkillManager(),
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
                            .Where(field => Attribute.IsDefined(field, typeof(SaveField)));

        // JSON 직렬화를 위한 필드와 값 설정
        var data = new Dictionary<string, object>();
        foreach (var field in fields)
            data[field.Name] = field.GetValue(savable);

        // JSON 직렬화 및 Firebase에 저장
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        await _databaseReference.Child("users").Child(_userID).Child(savable.Key).SetRawJsonValueAsync(json);

        Debug.Log($"저장 완료! {savable.Key} : {json}");
    }

    /// <summary>
    /// 데이터 로드 (저장된 데이터를 해당 클래스의 필드에 덮어씀)
    /// </summary>
    public async Task LoadAsync(ISavable savable)
    {
        var dataSnapshot = await _databaseReference.Child("users").Child(_userID).Child(savable.Key).GetValueAsync();

        // 데이터스냅샷 못찾았으면 그냥 리턴
        if (!dataSnapshot.Exists)
        {
            Debug.LogWarning($"{savable.Key} 데이터를 찾을 수 없습니다.");
            return;
        }

        // 저장된 데이터 가져오기
        string json = dataSnapshot.GetRawJsonValue();
        var loadedData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);


        // SaveField 어트리뷰트가 붙은 필드만 가져오기
        var fields = savable.GetType()
                            .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                            .Where(field => Attribute.IsDefined(field, typeof(SaveField)));

        // 필드값들 이름 매칭해서 데이터 덮어씌우기
        foreach (var field in fields)
        {
            if (loadedData.TryGetValue(field.Name, out var value))
            {
                try
                {
                    // 데이터 타입에 따라 적절히 변환
                    var convertedValue = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(value), field.FieldType);
                    field.SetValue(savable, convertedValue);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"필드 '{field.Name}' 로드 중 오류 발생: {ex.Message}");
                }
            }
        }

        Debug.Log($"불러오기 완료! {savable.Key} : {json}");
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

    /// <summary>
    /// 전체 데이터 저장
    /// </summary>
    public async Task SaveAll()
    {
        foreach (var manager in _managerList)
            await SaveAsync(manager);

        Debug.Log("전체 데이터 저장 완료!(버튼)");
    }

    /// <summary>
    /// 전체 데이터 불러오기
    /// </summary>
    public async Task LoadAll()
    {
        foreach (var manager in _managerList)
            await LoadAsync(manager);

        Debug.Log($"전체 데이터 불러오기 완료!(버튼)");
    }

    /// <summary>
    /// 전체 데이터 제거
    /// </summary>
    public async Task RemoveAll()
    {
        foreach (var manager in _managerList)
            await RemoveAsync(manager);

        Debug.Log("전체 데이터 제거 완료!(버튼)");
    }
    
    /// <summary>
    /// 전체 데이터 저장 버튼
    /// </summary>
    public void SaveAllButton() => _ = SaveAll(); // '_ =' 반환값 무시 (디스카드(discard))

    /// <summary>
    /// 전체 데이터 불러오기 버튼
    /// </summary>
    public void LoadAllButton() => _ = LoadAll();

    /// <summary>
    /// 전체 데이터 제거 버튼
    /// </summary>
    public void RemoveAllButton() => _ = RemoveAll();
}
