using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoldManager : SingletonBase<GoldManager>, ICurrencyManager
{
    private int _currentGold = 100000;                          // 현재 골드
    public static event Action<int> OnCurrencyChanged;          // 골드 변경 되었을 때 이벤트


    /// <summary>
    /// 골드 추가
    /// </summary>
    public void AddCurrency(int amount)
    {
        _currentGold += amount;
        NotifyChanged();
    }

    /// <summary>
    /// 골드 감소
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
            Debug.LogWarning("골드가 부족합니다!");
        }
    }

    /// <summary>
    /// 골드 가져오기
    /// </summary>
    public int GetCurrencyCount()
    {
        return _currentGold;
    }

    /// <summary>
    /// 골드 충분한지 체크
    /// </summary>
    public bool HasEnoughCurrency(int cost)
    {
        return _currentGold >= cost;
    }

    /// <summary>
    /// 골드 변경 이벤트 호출
    /// </summary>
    private void NotifyChanged()
    {
        OnCurrencyChanged?.Invoke(_currentGold);
    }
}
