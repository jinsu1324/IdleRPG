using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ʹ� ������ ����
/// </summary>
public class EnemyDataManager : SingletonBase<EnemyDataManager>
{
    [SerializeField] private EnemyDatasSO _enemyDatasSO;        // ���ʹ� �����͵� ��ũ���ͺ� ������Ʈ
    private static Dictionary<string, EnemyData> _enemyDataDict;   // ���ʹ� ������ ��ųʸ�
    
    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Set_EnemyDataDict();
    }

    /// <summary>
    /// ���ʹ� ������ ��ųʸ� ����
    /// </summary>
    public void Set_EnemyDataDict()
    {
        _enemyDataDict = new Dictionary<string, EnemyData>();

        foreach (EnemyData enemyData in _enemyDatasSO.EnemyDataList)
        {
            if (_enemyDataDict.ContainsKey(enemyData.ID) == false)
                _enemyDataDict[enemyData.ID] = enemyData;
        }
    }

    /// <summary>
    /// Ư�� ID�� �´� ���ʹ� ������ ��������
    /// </summary>
    public static EnemyData GetEnemyData(string id)
    {
        if (_enemyDataDict.TryGetValue(id, out EnemyData enemyData))
            return enemyData;
        else
        {
            Debug.Log($"{id}�� �´� ���ʹ� �����͸� ã�� �� �����ϴ�.");
            return null;
        }
    }

    /// <summary>
    /// �� ��差�� ���� ��� �߰�
    /// </summary>
    public static void AddGoldByEnemy(EnemyID enemyID)
    {
        EnemyData enemyData = GetEnemyData(enemyID.ToString());
        int dropGold = enemyData.DropGold;

        GoldManager.AddGold(dropGold);
    }
}
