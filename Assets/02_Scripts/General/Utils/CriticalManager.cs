using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ġ��Ÿ ���� ���� �� ���
/// </summary>
public class CriticalManager
{
    /// <summary>
    /// ġ��Ÿ ���� ���� �� ��ȯ
    /// </summary>
    public static bool IsCritical()
    {
        return Random.value <= PlayerStats.GetStatValue(StatType.CriticalRate);
    }

    /// <summary>
    /// ġ��Ÿ ���α��� ����Ͽ� ���� �������� ��ȯ
    /// </summary>
    public static float CalculateFinalDamage(float attackPower, bool isCritical)
    {
        if (isCritical)  // ġ��Ÿ ���� ����Ȱɷ� ��ȯ 
            return attackPower * PlayerStats.GetStatValue(StatType.CriticalMultiple); 
        else
            return attackPower; // �׳� ���ݷ����� ��ȯ
    }
}
