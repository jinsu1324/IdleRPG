using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 젬 관리자
/// </summary>
[System.Serializable]
public class GemManager : ISavable
{
    [SaveField] private static int _currentGem;                 // 현재 젬
    public static int CurrentGem
    { 
        get => _currentGem;
        set
        {
            _currentGem = value;
            NotifyChanged(); // 값이 변경될때 이벤트 호출
        }
    }

    public static event Action<int> OnGemChanged;   // 젬 변경 되었을 때 이벤트
    public string Key => nameof(GemManager);   // 고유 키 설정

    /// <summary>
    /// 젬 추가
    /// </summary>
    public static void AddGem(int amount)
    {
        CurrentGem += amount;
        Debug.Log($"현재 gem {CurrentGem}");
        //NotifyChanged();
    }

    /// <summary>
    /// 젬 감소
    /// </summary>
    public static void ReduceGem(int amount)
    {
        if (CurrentGem >= amount)
        {
            CurrentGem -= amount;
            //NotifyChanged();
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
        return CurrentGem;
    }

    /// <summary>
    /// 젬 충분한지 체크
    /// </summary>
    public static bool HasEnoughGem(int cost)
    {
        return CurrentGem >= cost;
    }

    /// <summary>
    /// 젬 변경 이벤트 호출
    /// </summary>
    private static void NotifyChanged()
    {
        Debug.Log("젬 변경 이벤트 호출!!~~~~~~~~~~!!!!!");

        OnGemChanged?.Invoke(_currentGem);
    }

    public void NotifyLoaded()
    {
        Debug.Log("젬 변경 이벤트 호출!!~~~~~~~~~~!!!!!");

        OnGemChanged?.Invoke(_currentGem);
    }
}
