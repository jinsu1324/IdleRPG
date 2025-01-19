using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    [Title("제너럴 데이터", Bold = false)]
    [SerializeField] public EnemyDatasSO EnemyDatasSO { get; private set; }                 // 적 데이터
    [SerializeField] public StageDatasSO StageDatasSO { get; private set; }                 // 스테이지 데이터
    [SerializeField] public StartingUpgradeDatasSO StartingUpgradeDatasSO { get; private set; }   // 스타팅 업그레이드 데이터

    [Title("장비 데이터", Bold = false)]
    [SerializeField] private List<GearDataSO> _gearDataSOList;      // 장비 데이터 스크립터블 오브젝트 리스트

    [Title("스킬 데이터", Bold = false)]
    [SerializeField] private List<SkillDataSO> _skillDataSOList;    // 스킬 데이터 스크립터블 오브젝트 리스트


    public void GOGO(Item item)
    {
        ItemType type = item.ItemType;  
    }





    /// <summary>
    /// Awake 일단...
    /// </summary>
    protected override void Awake()
    {
        // 싱글톤 먼저
        base.Awake(); 

        // 스타팅 업그레이드 데이터 셋팅
        UpgradeManager upgradeManager = new UpgradeManager();
        upgradeManager.Init(StartingUpgradeDatasSO.DataList);

        // 적 드랍 골드량 데이터 셋팅
        EnemyDropGoldManager enemyDropGoldManager = new EnemyDropGoldManager();
        enemyDropGoldManager.Init(EnemyDatasSO);
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // 초기 골드와 젬 설정
        GoldManager.AddGold(10000);
        GemManager.AddGem(100);
    }


    // 데이터들 set 함수들
    public void SetEnemyDatasSO(EnemyDatasSO data) => EnemyDatasSO = data;
    public void SetStageDatasSO(StageDatasSO data) => StageDatasSO = data;
    public void SetStartingUpgradeDatasSO(StartingUpgradeDatasSO data) => StartingUpgradeDatasSO = data;


    /// <summary>
    /// 모든 장비데이터 리스트 가져오기
    /// </summary>
    public List<GearDataSO> GetAllGearDataSO(ItemType itemType)
    {
        return _gearDataSOList.FindAll(x => x.ItemType == itemType.ToString());
    }

    /// <summary>
    /// 특정 장비 데이터 가져오기
    /// </summary>
    public GearDataSO GetGearDataSO(string id)
    {
        return _gearDataSOList.Find(x => x.ID == id);
    }

    /// <summary>
    /// 모든 스킬데이터 리스트 가져오기
    /// </summary>
    public List<SkillDataSO> GetAllSkillDataSO(ItemType itemType)
    {
        return _skillDataSOList.FindAll(x => x.ItemType == itemType.ToString());
    }
}
