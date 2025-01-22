using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropGoldManager
{
    private static Dictionary<EnemyID, int> _enemyDropGoldDict;    // �� ������ ��ųʸ�

    /// <summary>
    /// �� ������ ��ųʸ� ����
    /// </summary>
    public void Init()
    {
        _enemyDropGoldDict = new Dictionary<EnemyID, int>();
        EnemyID[] enemyIDArr = (EnemyID[])Enum.GetValues(typeof(EnemyID));

        foreach (EnemyID enemyID in enemyIDArr)
        {
            EnemyData enemyData = EnemyDataManager.GetEnemyData(enemyID.ToString());
            int dropGold = enemyData.DropGold;

            if (_enemyDropGoldDict.ContainsKey(enemyID) == false)
                _enemyDropGoldDict.Add(enemyID, dropGold);
        }
    }

    /// <summary>
    /// �� ��差�� ���� ��� �߰�
    /// </summary>
    public static void AddGoldByEnemy(EnemyID enemyID)
    {
        int gold = _enemyDropGoldDict[enemyID];
        GoldManager.AddGold(gold);
    }

}
