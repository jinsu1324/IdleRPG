using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    [Title("���ʷ� ������", Bold = false)]
    [SerializeField] public EnemyDatasSO EnemyDatasSO { get; private set; }                 // �� ������
    [SerializeField] public StageDatasSO StageDatasSO { get; private set; }                 // �������� ������
    [SerializeField] public StartingUpgradeDatasSO StartingUpgradeDatasSO { get; private set; }   // ��Ÿ�� ���׷��̵� ������

    [Title("��� ������", Bold = false)]
    [SerializeField] private List<GearDataSO> _gearDataSOList;      // ��� ������ ��ũ���ͺ� ������Ʈ ����Ʈ

    [Title("��ų ������", Bold = false)]
    [SerializeField] private List<SkillDataSO> _skillDataSOList;    // ��ų ������ ��ũ���ͺ� ������Ʈ ����Ʈ


    public void GOGO(Item item)
    {
        ItemType type = item.ItemType;  
    }





    /// <summary>
    /// Awake �ϴ�...
    /// </summary>
    protected override void Awake()
    {
        // �̱��� ����
        base.Awake(); 

        // ��Ÿ�� ���׷��̵� ������ ����
        UpgradeManager upgradeManager = new UpgradeManager();
        upgradeManager.Init(StartingUpgradeDatasSO.DataList);

        // �� ��� ��差 ������ ����
        EnemyDropGoldManager enemyDropGoldManager = new EnemyDropGoldManager();
        enemyDropGoldManager.Init(EnemyDatasSO);
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // �ʱ� ���� �� ����
        GoldManager.AddGold(10000);
        GemManager.AddGem(100);
    }


    // �����͵� set �Լ���
    public void SetEnemyDatasSO(EnemyDatasSO data) => EnemyDatasSO = data;
    public void SetStageDatasSO(StageDatasSO data) => StageDatasSO = data;
    public void SetStartingUpgradeDatasSO(StartingUpgradeDatasSO data) => StartingUpgradeDatasSO = data;


    /// <summary>
    /// ��� ������� ����Ʈ ��������
    /// </summary>
    public List<GearDataSO> GetAllGearDataSO(ItemType itemType)
    {
        return _gearDataSOList.FindAll(x => x.ItemType == itemType.ToString());
    }

    /// <summary>
    /// Ư�� ��� ������ ��������
    /// </summary>
    public GearDataSO GetGearDataSO(string id)
    {
        return _gearDataSOList.Find(x => x.ID == id);
    }

    /// <summary>
    /// ��� ��ų������ ����Ʈ ��������
    /// </summary>
    public List<SkillDataSO> GetAllSkillDataSO(ItemType itemType)
    {
        return _skillDataSOList.FindAll(x => x.ItemType == itemType.ToString());
    }
}
