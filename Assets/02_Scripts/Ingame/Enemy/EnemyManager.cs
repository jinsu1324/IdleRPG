using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Dictionary<EnemyID, int> _enemyDropGoldDict;    // Àû µå¶ø°ñµå µñ¼Å³Ê¸®

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        Enemy.OnEnemyDie += AddGoldByEnemy; // Àû Á×¾úÀ» ¶§, °ñµå Ãß°¡
    }

    /// <summary>
    /// ÀÓ½Ã µñ¼Å³Ê¸® ÃÊ±âÈ­
    /// </summary>
    private void Start()
    {
        SetEnemyDropGoldDict();
    }

    /// <summary>
    /// Àû µå¶ø°ñµå µñ¼Å³Ê¸® ¼ÂÆÃ
    /// </summary>
    private void SetEnemyDropGoldDict()
    {
        _enemyDropGoldDict = new Dictionary<EnemyID, int>();
        EnemyID[] enemyIDArr = (EnemyID[])Enum.GetValues(typeof(EnemyID));

        foreach (EnemyID enemyID in enemyIDArr)
        {
            EnemyData enemyData = DataManager.Instance.EnemyDatasSO.GetDataByID(enemyID.ToString());
            int dropGold = enemyData.DropGold;

            if (_enemyDropGoldDict.ContainsKey(enemyID) == false)
                _enemyDropGoldDict.Add(enemyID, dropGold);
        }
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        Enemy.OnEnemyDie -= AddGoldByEnemy;
    }

    /// <summary>
    /// Àû °ñµå·®¿¡ µû¶ó °ñµå Ãß°¡
    /// </summary>
    private void AddGoldByEnemy(EnemyEventArgs args)
    {
        int gold = _enemyDropGoldDict[args.EnemyID];
        GoldManager.Instance.AddCurrency(gold);
    }
}
