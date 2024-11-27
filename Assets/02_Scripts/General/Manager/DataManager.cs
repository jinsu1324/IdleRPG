using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : SerializedMonoBehaviour
{
    [Title("데이터 스크립터블 오브젝트들", Bold = false)]
    [SerializeField] public WeaponDatasSO WeaponData { get; private set; }           // 무기 데이터
    [SerializeField] public ArmorDatasSO ArmorData { get; private set; }             // 방어구 데이터
    [SerializeField] public EnemyDatasSO EnemyData { get; private set; }             // 적 데이터
    [SerializeField] public SkillDatasSO SkillData { get; private set; }             // 스킬 데이터
    [SerializeField] public StageDatasSO StageData { get; private set; }             // 스테이지 데이터

    // 데이터들 set 함수들
    public void SetWeaponData(WeaponDatasSO data) => WeaponData = data;
    public void SetArmorData(ArmorDatasSO data) => ArmorData = data;
    public void SetEnemyData(EnemyDatasSO data) => EnemyData = data;
    public void SetSkillData(SkillDatasSO data) => SkillData = data;
    public void SetStageData(StageDatasSO data) => StageData = data;
}
