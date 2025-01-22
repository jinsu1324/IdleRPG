using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 젬 관리자
/// </summary>
public class GemManager : ISavable
{
    public static event Action<int> OnGemChange;    // 젬 변경 되었을 때 이벤트
    public string Key => nameof(GemManager);        // Firebase 데이터 저장용 고유 키 설정
    [SaveField] private static int _currentGem;     // 현재 젬
    public static int CurrentGem
    { 
        get => _currentGem;
        set
        {
            _currentGem = value;
            NotifyChanged(); // 값이 변경될때 이벤트 호출
        }
    }

    /// <summary>
    /// 젬 추가
    /// </summary>
    public static void AddGem(int amount)
    {
        CurrentGem += amount;
    }

    /// <summary>
    /// 젬 감소
    /// </summary>
    public static void ReduceGem(int amount)
    {
        if (CurrentGem >= amount)
        {
            CurrentGem -= amount;
        }
        else
            Debug.Log($"보유한 젬({CurrentGem})이 감소할 젬({amount})보다 적어서 젬감소가 불가능합니다!");
    }

    /// <summary>
    /// 젬 가져오기
    /// </summary>
    public static int GetGem() => CurrentGem;

    /// <summary>
    /// 젬 충분한지 체크
    /// </summary>
    public static bool HasEnoughGem(int cost) => CurrentGem >= cost;

    /// <summary>
    /// 젬 변경 이벤트 호출
    /// </summary>
    private static void NotifyChanged() => OnGemChange?.Invoke(CurrentGem);
}
