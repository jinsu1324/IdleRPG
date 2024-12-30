using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 젬 관리자
/// </summary>
public class GemManager : MonoBehaviour
{
    private static int _currentGem = 100;                   // 현재 젬
    public static event Action<int> OnGemChanged;           // 젬 변경 되었을 때 이벤트

    /// <summary>
    /// 젬 추가
    /// </summary>
    public static void AddGem(int amount)
    {
        _currentGem += amount;
        NotifyChanged();
    }

    /// <summary>
    /// 젬 감소
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
            Debug.LogWarning("감소할 젬이 부족합니다!");
        }
    }

    /// <summary>
    /// 젬 가져오기
    /// </summary>
    public static int GetGem()
    {
        return _currentGem;
    }

    /// <summary>
    /// 젬 충분한지 체크
    /// </summary>
    public static bool HasEnoughGem(int cost)
    {
        return _currentGem >= cost;
    }

    /// <summary>
    /// 젬 변경 이벤트 호출
    /// </summary>
    private static void NotifyChanged()
    {
        OnGemChanged?.Invoke(_currentGem);
    }
}
