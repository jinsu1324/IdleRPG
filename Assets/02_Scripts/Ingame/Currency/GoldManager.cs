using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ������
/// </summary>
public class GoldManager
{
    private static int _currentGold = 100000;                   // ���� ���
    public static event Action<int> OnGoldChanged;              // ��� ���� �Ǿ��� �� �̺�Ʈ

    /// <summary>
    /// ��� �߰�
    /// </summary>
    public static void AddGold(int amount)
    {
        _currentGold += amount;
        NotifyChanged();
    }

    /// <summary>
    /// ��� ����
    /// </summary>
    public static void ReduceGold(int amount)
    {
        if (_currentGold >= amount)
        {
            _currentGold -= amount;
            NotifyChanged();
        }
        else
        {
            Debug.LogWarning("������ ��尡 �����մϴ�!");
        }
    }

    /// <summary>
    /// ��� ��������
    /// </summary>
    public static int GetGold()
    {
        return _currentGold;
    }

    /// <summary>
    /// ��� ������� üũ
    /// </summary>
    public static bool HasEnoughGold(int cost)
    {
        return _currentGold >= cost;
    }

    /// <summary>
    /// ��� ���� �̺�Ʈ ȣ��
    /// </summary>
    private static void NotifyChanged()
    {
        OnGoldChanged?.Invoke(_currentGold);
    }
}
