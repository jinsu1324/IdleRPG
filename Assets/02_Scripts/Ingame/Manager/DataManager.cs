using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    [Title("���ʷ� ������", Bold = false)]
    [SerializeField] public EnemyDatasSO EnemyDatasSO { get; private set; }                 // �� ������
    [SerializeField] public SkillDatasSO SkillDatasSO { get; private set; }                 // ��ų ������
    [SerializeField] public StageDatasSO StageDatasSO { get; private set; }                 // �������� ������
    [SerializeField] public StartingUpgradeDatasSO StartingUpgradeDatasSO { get; private set; }   // ��Ÿ�� ���׷��̵� ������

    [Title("������ ������", Bold = false)]
    [SerializeField] private List<ItemDataSO> _itemDataSOList;      // ������ ������ ����Ʈ
    private Dictionary<string, ItemDataSO> _itemDataSODict;         // ������ ������ ��ųʸ�

    [Title("����Ʈ ������", Bold = false)]
    [SerializeField] public QuestDatasSO QuestDatasSO { get; private set; }            // ����Ʈ ������

    // �����͵� set �Լ���
    public void SetEnemyDatasSO(EnemyDatasSO data) => EnemyDatasSO = data;
    public void SetSkillDatasSO(SkillDatasSO data) => SkillDatasSO = data;
    public void SetStageDatasSO(StageDatasSO data) => StageDatasSO = data;
    public void SetStartingUpgradeDatasSO(StartingUpgradeDatasSO data) => StartingUpgradeDatasSO = data;
    

    /// <summary>
    /// Awake �ϴ�...
    /// </summary>
    protected override void Awake()
    {
        // �̱��� ����
        base.Awake(); 

        // ������ ������ �ʱ�ȭ
        Init_ItemDataSODict();    


        // �÷��̾� ���� ������̾� ��ųʸ��� ���� �ʱ�ȭ
        PlayerStats.Instance.InitStatModifierDict(); 


        // ��Ÿ�� ���׷��̵� ������ ����
        UpgradeManager upgradeManager = new UpgradeManager();
        upgradeManager.Init(StartingUpgradeDatasSO.DataList);

        // �� ��� ��差 ������ ����
        EnemyDropGoldManager enemyDropGoldManager = new EnemyDropGoldManager();
        enemyDropGoldManager.Init(EnemyDatasSO);

    }

    /// <summary>
    /// ������ ������ ����Ʈ -> ��ųʸ� �ʱ�ȭ
    /// </summary>
    private void Init_ItemDataSODict()
    {
        _itemDataSODict = new Dictionary<string, ItemDataSO>();

        foreach (ItemDataSO itemDataSO in _itemDataSOList)
        {
            if (_itemDataSODict.ContainsKey(itemDataSO.ID) == false)
                _itemDataSODict.Add(itemDataSO.ID, itemDataSO);
            else
                Debug.LogWarning($"Init_ItemDataSODict �� �ߺ��� ���̵��Դϴ�. : {itemDataSO.ID}");
        }
    }

    /// <summary>
    /// ������ ��������
    /// </summary>
    public ItemDataSO GetItemDataSO_ByID(string id)
    {
        if (_itemDataSODict.TryGetValue(id, out var equipment))
        {
            return equipment;
        }

        Debug.LogError($"Equipment ID �� ã�� �� �����ϴ�: {id}");
        return null;
    }

    /// <summary>
    /// ������ Ÿ�Կ� �´� ��� �����۵����� ����Ʈ�� ��������
    /// </summary>
    /// <returns></returns>
    public List<ItemDataSO> GetAllItemDataSO_ByItemType(ItemType itemType)
    {
        return _itemDataSOList.FindAll(x => x.ItemType == itemType.ToString());
    }

    /// <summary>
    /// ��� ������ ����Ʈ ��ȯ
    /// </summary>
    public List<ItemDataSO> GetAllItemDataSO()
    {
        return new List<ItemDataSO>(_itemDataSODict.Values);
    }
}
