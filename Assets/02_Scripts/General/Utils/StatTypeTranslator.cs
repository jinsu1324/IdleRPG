using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스탯타입을 한글로 번역해주는 클래스
/// </summary>
public class StatTypeTranslator : MonoBehaviour
{
    /// <summary>
    /// 스탯타입 영어를 한글로 변환
    /// </summary>
    public static string Translate(StatType statType)
    {
        return statType switch
        {
            StatType.AttackPower        => "공격력",
            StatType.AttackSpeed        => "공격속도",
            StatType.MaxHp              => "최대체력",
            StatType.CriticalRate       => "치명타확률",
            StatType.CriticalMultiple   => "치명타배율",
            _                           => ""
        };
    }
}
