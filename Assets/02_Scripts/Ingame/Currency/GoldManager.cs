using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 골드 관리자
/// </summary>
public class GoldManager
{
    private static int _currentGold = 100000;                   // 현재 골드
    public static event Action<int> OnGoldChanged;              // 골드 변경 되었을 때 이벤트

    /// <summary>
    /// 골드 추가
    /// </summary>
    public static void AddGold(int amount)
    {
        _currentGold += amount;
        NotifyChanged();
    }

    /// <summary>
    /// 골드 감소
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
            Debug.LogWarning("감소할 골드가 부족합니다!");
        }
    }

    /// <summary>
    /// 골드 가져오기
    /// </summary>
    public static int GetGold()
    {
        return _currentGold;
    }

    /// <summary>
    /// 골드 충분한지 체크
    /// </summary>
    public static bool HasEnoughGold(int cost)
    {
        return _currentGold >= cost;
    }

    /// <summary>
    /// 골드 변경 이벤트 호출
    /// </summary>
    private static void NotifyChanged()
    {
        OnGoldChanged?.Invoke(_currentGold);
    }
}
