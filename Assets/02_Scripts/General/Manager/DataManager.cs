using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SerializedMonoBehaviour
{
    #region �̱���_���̵� O
    private static DataManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static DataManager Instance
    {
        get
        {
            if ( _instance == null )
            {
                return null;
            }

            return _instance;
        }
    }
    #endregion

    [Title("������ ��ũ���ͺ� ������Ʈ��")]
    [SerializeField]
    public WeaponDatasSO WeaponData { get; private set; }

    [SerializeField]
    public ArmorDatasSO ArmorData { get; private set; }

    [SerializeField]
    public EnemyDatasSO EnemyData { get; private set; }

    [SerializeField]
    public StageDatasSO StageData { get; private set; }

    [SerializeField]
    public SkillDatasSO SkillData { get; private set; }


    // �����͵� set �Լ�
    public void SetWeaponData(WeaponDatasSO data) => WeaponData = data;
    public void SetArmorData(ArmorDatasSO data) => ArmorData = data;
    public void SetEnemyData(EnemyDatasSO data) => EnemyData = data;
    public void SetStageData(StageDatasSO data) => StageData = data;
    public void SetSkillData(SkillDatasSO data) => SkillData = data;
}
