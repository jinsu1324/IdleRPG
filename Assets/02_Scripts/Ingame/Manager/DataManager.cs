using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    [Title("제너럴 데이터", Bold = false)]
    [SerializeField] public EnemyDatasSO EnemyDatasSO { get; private set; }                 // 적 데이터
    [SerializeField] public SkillDatasSO SkillDatasSO { get; private set; }                 // 스킬 데이터
    [SerializeField] public StageDatasSO StageDatasSO { get; private set; }                 // 스테이지 데이터
    [SerializeField] public StartingStatDatasSO StartingStatDatasSO { get; private set; }   // 스타팅 스탯 데이터

    [Title("장비 데이터", Bold = false)]
    [SerializeField] private List<EquipmentDataSO> _equipmentDataSOList;      // 장비 데이터 리스트
    private Dictionary<string, EquipmentDataSO> _equipmentDataSODict;         // 장비 데이터 딕셔너리

    // 데이터들 set 함수들
    public void SetEnemyDatasSO(EnemyDatasSO data) => EnemyDatasSO = data;
    public void SetSkillDatasSO(SkillDatasSO data) => SkillDatasSO = data;
    public void SetStageDatasSO(StageDatasSO data) => StageDatasSO = data;
    public void SetStartingStatDatasSO(StartingStatDatasSO data) => StartingStatDatasSO = data;
    

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake(); // 싱글톤 먼저


        InitEquipmentDataSODict();    // 장비 데이터 초기화


        // 플레이어 스탯 데이터 셋팅
        PlayerStatManager playerStatManager = new PlayerStatManager();
        playerStatManager.Init(StartingStatDatasSO.DataList);

        // 적 드랍 골드량 데이터 셋팅
        EnemyDropGoldManager enemyDropGoldManager = new EnemyDropGoldManager();
        enemyDropGoldManager.Init(EnemyDatasSO);

    }

    /// <summary>
    /// 장비 데이터 리스트 -> 딕셔너리 초기화
    /// </summary>
    private void InitEquipmentDataSODict()
    {
        _equipmentDataSODict = new Dictionary<string, EquipmentDataSO>();

        foreach (EquipmentDataSO equipmentDataSO in _equipmentDataSOList)
        {
            if (_equipmentDataSODict.ContainsKey(equipmentDataSO.ID) == false)
                _equipmentDataSODict.Add(equipmentDataSO.ID, equipmentDataSO);
            else
                Debug.LogWarning($"InitEquipmentDataDict 중 중복된 아이디입니다. : {equipmentDataSO.ID}");
        }
    }

    /// <summary>
    /// 장비 가져오기
    /// </summary>
    public EquipmentDataSO GetEquipmentDataSOByID(string id)
    {
        if (_equipmentDataSODict.TryGetValue(id, out var equipment))
        {
            return equipment;
        }

        Debug.LogError($"Equipment ID 를 찾을 수 없습니다: {id}");
        return null;
    }

    /// <summary>
    /// 모든 장비 리스트 반환
    /// </summary>
    public List<EquipmentDataSO> GetAllEquipmentDataSO()
    {
        return new List<EquipmentDataSO>(_equipmentDataSODict.Values);
    }
}
