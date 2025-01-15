using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberConverter
{
    // ������
    private static string[] _units =
        { "",
        "A", "B", "C", "D", "E", "F", "G",
        "H", "I", "J", "K", "L", "M", "N",
        "O", "P", "Q", "R", "S", "T", "U",
        "V", "W", "X", "Y", "Z"
        };

    /// <summary>
    /// ���ڸ� ���ĺ� �������ڷ� ��ȯ
    /// </summary>
    public static string ConvertAlphabet(double number)
    {
        if (number < 1000) // 1000 �̸��̸� ��ȯ ���� ���� �״�� ��ȯ + �Ҽ��� ����
            return number.ToString("F0"); // �Ҽ��� ���� 0�ڸ� ǥ�� ( = �Ҽ��� ǥ��x)

        int unitIndex = 0;
        while (number >= 1000 && unitIndex < _units.Length - 1)
        {
            number /= 1000; // 1000���� ������ ������ �ø�
            unitIndex++;
        }

        return $"{number:F2}{_units[unitIndex]}"; // 2�ڸ� �Ҽ��� ���� �߰�
    }

    /// <summary>
    /// �Ҽ��� ���ڸ� �ۼ�Ƽ���� ��ȯ (ex) 0.05 -> 5%)
    /// </summary>
    public static string ConvertPercentage(float number)
    {
        return $"{(number * 100.0f):F1}%"; // 1�ڸ� �Ҽ��� �ۼ�Ƽ���� ǥ��
    }

    /// <summary>
    /// ���ڸ� �Ҽ��� �ʹ� �Ѿ���ʰ� �ڸ��� Ȯ���ؼ� ǥ��
    /// </summary>
    public static string ConvertFixedDecimals(float number)
    {
        return $"{number:F1}"; // 1�ڸ� �Ҽ��� ǥ��
    }
}
