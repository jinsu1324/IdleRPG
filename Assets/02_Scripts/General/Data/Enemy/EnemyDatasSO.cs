using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyDatasSO : ScriptableObject
{
    public List<EnemyData> EnemyDataList = new List<EnemyData>(); // 적 데이터 리스트
}
