using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 치명타 여부 결정 및 계산
/// </summary>
public class CriticalManager
{
    /// <summary>
    /// 치명타 여부 결정 및 반환
    /// </summary>
    public static bool IsCritical()
    {
        return Random.value <= PlayerStats.GetStatValue(StatType.CriticalRate);
    }

    /// <summary>
    /// 치명타 여부까지 계산하여 최종 데미지를 반환
    /// </summary>
    public static float CalculateFinalDamage(float attackPower, bool isCritical)
    {
        if (isCritical)  // 치명타 피해 적용된걸로 반환 
            return attackPower * PlayerStats.GetStatValue(StatType.CriticalMultiple); 
        else
            return attackPower; // 그냥 공격력으로 반환
    }
}
