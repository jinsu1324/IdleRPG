using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : SerializedMonoBehaviour
{
    #region Singleton
    public static DataManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Title("데이터 스크립터블 오브젝트들", Bold = false)]
    [SerializeField] public WeaponDatasSO WeaponDatasSO { get; private set; }           // 무기 데이터
    [SerializeField] public ArmorDatasSO ArmorDatasSO { get; private set; }             // 방어구 데이터
    [SerializeField] public EnemyDatasSO EnemyDatasSO { get; private set; }             // 적 데이터
    [SerializeField] public SkillDatasSO SkillDatasSO { get; private set; }             // 스킬 데이터
    [SerializeField] public StageDatasSO StageDatasSO { get; private set; }             // 스테이지 데이터
    [SerializeField] public StartingStatsSO StartingStatsSO { get; private set; }       // 스타팅 스탯 데이터

    // 데이터들 set 함수들
    public void SetWeaponDatasSO(WeaponDatasSO data) => WeaponDatasSO = data;
    public void SetArmorDatasSO(ArmorDatasSO data) => ArmorDatasSO = data;
    public void SetEnemyDatasSO(EnemyDatasSO data) => EnemyDatasSO = data;
    public void SetSkillDatasSO(SkillDatasSO data) => SkillDatasSO = data;
    public void SetStageDatasSO(StageDatasSO data) => StageDatasSO = data;
    public void SetStartingStatsSO(StartingStatsSO data) => StartingStatsSO = data;
}
