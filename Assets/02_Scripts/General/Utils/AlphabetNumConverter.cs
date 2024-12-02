using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphabetNumConverter
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
    public static string Convert(double number)
    {
        if (number < 1000) // 1000 �̸��̸� ��ȯ ���� ���� �״�� ��ȯ
            return number.ToString("F0"); // �Ҽ��� ����

        int unitIndex = 0;
        while (number >= 1000 && unitIndex < _units.Length - 1)
        {
            number /= 1000; // 1000���� ������ ������ �ø�
            unitIndex++;
        }

        return $"{number:F2}{_units[unitIndex]}"; // 2�ڸ� �Ҽ��� ���� �߰�
    }
}
