using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ÿ���� �ѱ۷� �������ִ� Ŭ����
/// </summary>
public class StatTypeTranslator : MonoBehaviour
{
    /// <summary>
    /// ����Ÿ�� ��� �ѱ۷� ��ȯ
    /// </summary>
    public static string Translate(StatType statType)
    {
        return statType switch
        {
            StatType.AttackPower        => "���ݷ�",
            StatType.AttackSpeed        => "���ݼӵ�",
            StatType.MaxHp              => "�ִ�ü��",
            StatType.CriticalRate       => "ġ��ŸȮ��",
            StatType.CriticalMultiple   => "ġ��Ÿ����",
            _                           => ""
        };
    }
}
