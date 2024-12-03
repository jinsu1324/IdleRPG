using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoldManager : MonoBehaviour, ICurrencyManager
{
    #region Singleton
    public static GoldManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private int _currentGold = 100000;              // ���� ���
    public event Action<int> OnCurrencyChanged;     // ��� ���� �Ǿ��� �� �̺�Ʈ

    /// <summary>
    /// ��� �߰�
    /// </summary>
    public void AddCurrency(int amount)
    {
        _currentGold += amount;
        NotifyChanged();
    }

    /// <summary>
    /// ��� ����
    /// </summary>
    public void ReduceCurrency(int amount)
    {
        if (_currentGold >= amount)
        {
            _currentGold -= amount;
            NotifyChanged();
        }
        else
        {
            Debug.LogWarning("��尡 �����մϴ�!");
        }
    }

    /// <summary>
    /// ��� ��������
    /// </summary>
    public int GetCurrencyCount()
    {
        return _currentGold;
    }

    /// <summary>
    /// ��� ������� üũ
    /// </summary>
    public bool HasEnoughCurrency(int cost)
    {
        return _currentGold >= cost;
    }

    /// <summary>
    /// ��� ���� �̺�Ʈ ȣ��
    /// </summary>
    private void NotifyChanged()
    {
        OnCurrencyChanged?.Invoke(_currentGold);
        Debug.Log($"���� ��� : {_currentGold}");
    }
}
