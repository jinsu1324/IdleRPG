using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : SerializedMonoBehaviour
{
    [Title("������ ��ũ���ͺ� ������Ʈ��", Bold = false)]
    [SerializeField] public WeaponDatasSO WeaponData { get; private set; }           // ���� ������
    [SerializeField] public ArmorDatasSO ArmorData { get; private set; }             // �� ������
    [SerializeField] public EnemyDatasSO EnemyData { get; private set; }             // �� ������
    [SerializeField] public SkillDatasSO SkillData { get; private set; }             // ��ų ������
    [SerializeField] public StageDatasSO StageData { get; private set; }             // �������� ������

    // �����͵� set �Լ���
    public void SetWeaponData(WeaponDatasSO data) => WeaponData = data;
    public void SetArmorData(ArmorDatasSO data) => ArmorData = data;
    public void SetEnemyData(EnemyDatasSO data) => EnemyData = data;
    public void SetSkillData(SkillDatasSO data) => SkillData = data;
    public void SetStageData(StageDatasSO data) => StageData = data;
}
