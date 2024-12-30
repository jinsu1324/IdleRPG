using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �� ������
/// </summary>
public class GemManager : MonoBehaviour
{
    private static int _currentGem = 100;                   // ���� ��
    public static event Action<int> OnGemChanged;           // �� ���� �Ǿ��� �� �̺�Ʈ

    /// <summary>
    /// �� �߰�
    /// </summary>
    public static void AddGem(int amount)
    {
        _currentGem += amount;
        NotifyChanged();
    }

    /// <summary>
    /// �� ����
    /// </summary>
    public static void ReduceGem(int amount)
    {
        if (_currentGem >= amount)
        {
            _currentGem -= amount;
            NotifyChanged();
        }
        else
        {
            Debug.LogWarning("������ ���� �����մϴ�!");
        }
    }

    /// <summary>
    /// �� ��������
    /// </summary>
    public static int GetGem()
    {
        return _currentGem;
    }

    /// <summary>
    /// �� ������� üũ
    /// </summary>
    public static bool HasEnoughGem(int cost)
    {
        return _currentGem >= cost;
    }

    /// <summary>
    /// �� ���� �̺�Ʈ ȣ��
    /// </summary>
    private static void NotifyChanged()
    {
        OnGemChanged?.Invoke(_currentGem);
    }
}
