using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberConverter
{
    // 단위들
    private static string[] _units =
        { "",
        "A", "B", "C", "D", "E", "F", "G",
        "H", "I", "J", "K", "L", "M", "N",
        "O", "P", "Q", "R", "S", "T", "U",
        "V", "W", "X", "Y", "Z"
        };

    /// <summary>
    /// 숫자를 알파벳 단위숫자로 변환
    /// </summary>
    public static string ConvertAlphabet(double number)
    {
        if (number < 1000) // 1000 미만이면 변환 없이 숫자 그대로 반환 + 소수점 제거
            return number.ToString("F0"); // 소수점 이하 0자리 표시 ( = 소수점 표시x)

        int unitIndex = 0;
        while (number >= 1000 && unitIndex < _units.Length - 1)
        {
            number /= 1000; // 1000으로 나누어 단위를 올림
            unitIndex++;
        }

        return $"{number:F2}{_units[unitIndex]}"; // 2자리 소수와 단위 추가
    }

    /// <summary>
    /// 소수점 숫자를 퍼센티지로 변환 (ex) 0.05 -> 5%)
    /// </summary>
    public static string ConvertPercentage(float number)
    {
        return $"{(number * 100.0f):F1}%"; // 1자리 소수와 퍼센티지로 표시
    }

    /// <summary>
    /// 숫자를 소수점 너무 넘어가지않게 자릿수 확정해서 표시
    /// </summary>
    public static string ConvertFixedDecimals(float number)
    {
        return $"{number:F1}"; // 1자리 소수로 표시
    }
}
