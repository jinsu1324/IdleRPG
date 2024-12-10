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
    [SerializeField] public StartingStatDatasSO StartingStatDatasSO { get; private set; }   // ��Ÿ�� ���� ������

    [Title("��� ������", Bold = false)]
    [SerializeField] private List<EquipmentDataSO> _equipmentDataSOList;      // ��� ������ ����Ʈ
    private Dictionary<string, EquipmentDataSO> _equipmentDataSODict;         // ��� ������ ��ųʸ�

    // �����͵� set �Լ���
    public void SetEnemyDatasSO(EnemyDatasSO data) => EnemyDatasSO = data;
    public void SetSkillDatasSO(SkillDatasSO data) => SkillDatasSO = data;
    public void SetStageDatasSO(StageDatasSO data) => StageDatasSO = data;
    public void SetStartingStatDatasSO(StartingStatDatasSO data) => StartingStatDatasSO = data;
    

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake(); // �̱��� ����


        InitEquipmentDataSODict();    // ��� ������ �ʱ�ȭ


        // �÷��̾� ���� ������ ����
        PlayerStatManager playerStatManager = new PlayerStatManager();
        playerStatManager.Init(StartingStatDatasSO.DataList);

        // �� ��� ��差 ������ ����
        EnemyDropGoldManager enemyDropGoldManager = new EnemyDropGoldManager();
        enemyDropGoldManager.Init(EnemyDatasSO);

    }

    /// <summary>
    /// ��� ������ ����Ʈ -> ��ųʸ� �ʱ�ȭ
    /// </summary>
    private void InitEquipmentDataSODict()
    {
        _equipmentDataSODict = new Dictionary<string, EquipmentDataSO>();

        foreach (EquipmentDataSO equipmentDataSO in _equipmentDataSOList)
        {
            if (_equipmentDataSODict.ContainsKey(equipmentDataSO.ID) == false)
                _equipmentDataSODict.Add(equipmentDataSO.ID, equipmentDataSO);
            else
                Debug.LogWarning($"InitEquipmentDataDict �� �ߺ��� ���̵��Դϴ�. : {equipmentDataSO.ID}");
        }
    }

    /// <summary>
    /// ��� ��������
    /// </summary>
    public EquipmentDataSO GetEquipmentDataSOByID(string id)
    {
        if (_equipmentDataSODict.TryGetValue(id, out var equipment))
        {
            return equipment;
        }

        Debug.LogError($"Equipment ID �� ã�� �� �����ϴ�: {id}");
        return null;
    }

    /// <summary>
    /// ��� ��� ����Ʈ ��ȯ
    /// </summary>
    public List<EquipmentDataSO> GetAllEquipmentDataSO()
    {
        return new List<EquipmentDataSO>(_equipmentDataSODict.Values);
    }
}
