using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 에너미 데이터 관리
/// </summary>
public class EnemyDataManager : SingletonBase<EnemyDataManager>
{
    [SerializeField] private EnemyDatasSO _enemyDatasSO;        // 에너미 데이터들 스크립터블 오브젝트
    private static Dictionary<string, EnemyData> _enemyDataDict;   // 에너미 데이터 딕셔너리
    
    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Set_EnemyDataDict();
    }

    /// <summary>
    /// 에너미 데이터 딕셔너리 셋팅
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
    /// 특정 ID에 맞는 에너미 데이터 가져오기
    /// </summary>
    public static EnemyData GetEnemyData(string id)
    {
        if (_enemyDataDict.TryGetValue(id, out EnemyData enemyData))
            return enemyData;
        else
        {
            Debug.Log($"{id}에 맞는 에너미 데이터를 찾을 수 없습니다.");
            return null;
        }
    }

    /// <summary>
    /// 적 골드량에 따라 골드 추가
    /// </summary>
    public static void AddGoldByEnemy(EnemyID enemyID)
    {
        EnemyData enemyData = GetEnemyData(enemyID.ToString());
        int dropGold = enemyData.DropGold;

        GoldManager.AddGold(dropGold);
    }
}
