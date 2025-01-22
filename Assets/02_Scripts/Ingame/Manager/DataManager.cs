using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    [Title("제너럴 데이터", Bold = false)]
    [SerializeField] public StartingUpgradeDatasSO StartingUpgradeDatasSO { get; private set; }   // 스타팅 업그레이드 데이터


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
        enemyDropGoldManager.Init();
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
    public void SetStartingUpgradeDatasSO(StartingUpgradeDatasSO data) => StartingUpgradeDatasSO = data;
}
