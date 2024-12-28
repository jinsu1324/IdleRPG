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
    [SerializeField] public StartingUpgradeDatasSO StartingUpgradeDatasSO { get; private set; }   // 스타팅 업그레이드 데이터

    [Title("아이템 데이터", Bold = false)]
    [SerializeField] private List<ItemDataSO> _itemDataSOList;      // 아이템 데이터 리스트
    private Dictionary<string, ItemDataSO> _itemDataSODict;         // 아이템 데이터 딕셔너리

    [Title("퀘스트 데이터", Bold = false)]
    [SerializeField] public QuestDatasSO QuestDatasSO { get; private set; }            // 퀘스트 데이터

    // 데이터들 set 함수들
    public void SetEnemyDatasSO(EnemyDatasSO data) => EnemyDatasSO = data;
    public void SetSkillDatasSO(SkillDatasSO data) => SkillDatasSO = data;
    public void SetStageDatasSO(StageDatasSO data) => StageDatasSO = data;
    public void SetStartingUpgradeDatasSO(StartingUpgradeDatasSO data) => StartingUpgradeDatasSO = data;
    

    /// <summary>
    /// Awake 일단...
    /// </summary>
    protected override void Awake()
    {
        // 싱글톤 먼저
        base.Awake(); 

        // 아이템 데이터 초기화
        Init_ItemDataSODict();    


        // 플레이어 스탯 모디파이어 딕셔너리를 먼저 초기화
        PlayerStats.Instance.InitStatModifierDict(); 


        // 스타팅 업그레이드 데이터 셋팅
        UpgradeManager upgradeManager = new UpgradeManager();
        upgradeManager.Init(StartingUpgradeDatasSO.DataList);

        // 적 드랍 골드량 데이터 셋팅
        EnemyDropGoldManager enemyDropGoldManager = new EnemyDropGoldManager();
        enemyDropGoldManager.Init(EnemyDatasSO);

    }

    /// <summary>
    /// 아이템 데이터 리스트 -> 딕셔너리 초기화
    /// </summary>
    private void Init_ItemDataSODict()
    {
        _itemDataSODict = new Dictionary<string, ItemDataSO>();

        foreach (ItemDataSO itemDataSO in _itemDataSOList)
        {
            if (_itemDataSODict.ContainsKey(itemDataSO.ID) == false)
                _itemDataSODict.Add(itemDataSO.ID, itemDataSO);
            else
                Debug.LogWarning($"Init_ItemDataSODict 중 중복된 아이디입니다. : {itemDataSO.ID}");
        }
    }

    /// <summary>
    /// 아이템 가져오기
    /// </summary>
    public ItemDataSO GetItemDataSO_ByID(string id)
    {
        if (_itemDataSODict.TryGetValue(id, out var equipment))
        {
            return equipment;
        }

        Debug.LogError($"Equipment ID 를 찾을 수 없습니다: {id}");
        return null;
    }

    /// <summary>
    /// 아이템 타입에 맞는 모든 아이템데이터 리스트로 가져오기
    /// </summary>
    /// <returns></returns>
    public List<ItemDataSO> GetAllItemDataSO_ByItemType(ItemType itemType)
    {
        return _itemDataSOList.FindAll(x => x.ItemType == itemType.ToString());
    }

    /// <summary>
    /// 모든 아이템 리스트 반환
    /// </summary>
    public List<ItemDataSO> GetAllItemDataSO()
    {
        return new List<ItemDataSO>(_itemDataSODict.Values);
    }
}
