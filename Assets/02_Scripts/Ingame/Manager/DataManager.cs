using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    [Title("데이터 스크립터블 오브젝트들", Bold = false)]
    [SerializeField] public WeaponDatasSO WeaponDatasSO { get; private set; }               // 무기 데이터
    [SerializeField] public ArmorDatasSO ArmorDatasSO { get; private set; }                 // 방어구 데이터
    [SerializeField] public EnemyDatasSO EnemyDatasSO { get; private set; }                 // 적 데이터
    [SerializeField] public SkillDatasSO SkillDatasSO { get; private set; }                 // 스킬 데이터
    [SerializeField] public StageDatasSO StageDatasSO { get; private set; }                 // 스테이지 데이터
    [SerializeField] public StartingStatDatasSO StartingStatDatasSO { get; private set; }   // 스타팅 스탯 데이터

    // 데이터들 set 함수들
    public void SetWeaponDatasSO(WeaponDatasSO data) => WeaponDatasSO = data;
    public void SetArmorDatasSO(ArmorDatasSO data) => ArmorDatasSO = data;
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

        // 플레이어 스탯 데이터 셋팅
        PlayerStatManager playerStatManager = new PlayerStatManager();
        playerStatManager.Init(StartingStatDatasSO.DataList);

        // 적 드랍 골드량 데이터 셋팅
        EnemyDropGoldManager enemyDropGoldManager = new EnemyDropGoldManager();
        enemyDropGoldManager.Init(EnemyDatasSO);
    }
}
