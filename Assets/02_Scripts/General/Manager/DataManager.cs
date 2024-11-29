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

    [Title("������ ��ũ���ͺ� ������Ʈ��", Bold = false)]
    [SerializeField] public WeaponDatasSO WeaponDatasSO { get; private set; }           // ���� ������
    [SerializeField] public ArmorDatasSO ArmorDatasSO { get; private set; }             // �� ������
    [SerializeField] public EnemyDatasSO EnemyDatasSO { get; private set; }             // �� ������
    [SerializeField] public SkillDatasSO SkillDatasSO { get; private set; }             // ��ų ������
    [SerializeField] public StageDatasSO StageDatasSO { get; private set; }             // �������� ������
    [SerializeField] public StartingStatsSO StartingStatsSO { get; private set; }       // ��Ÿ�� ���� ������

    // �����͵� set �Լ���
    public void SetWeaponDatasSO(WeaponDatasSO data) => WeaponDatasSO = data;
    public void SetArmorDatasSO(ArmorDatasSO data) => ArmorDatasSO = data;
    public void SetEnemyDatasSO(EnemyDatasSO data) => EnemyDatasSO = data;
    public void SetSkillDatasSO(SkillDatasSO data) => SkillDatasSO = data;
    public void SetStageDatasSO(StageDatasSO data) => StageDatasSO = data;
    public void SetStartingStatsSO(StartingStatsSO data) => StartingStatsSO = data;
}
