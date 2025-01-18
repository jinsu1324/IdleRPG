using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 골드 관리자
/// </summary>
[System.Serializable]
public class GoldManager : ISavable
{
    [SaveField] private static int _currentGold;                   // 현재 골드
    public static int CurrentGold
    {
        get => _currentGold;
        set
        {
            _currentGold = value;
            NotifyChanged(); // 값이 변경될 때 이벤트 호출
        }
    }


    public static event Action<int> OnGoldChanged;     // 골드 변경 되었을 때 이벤트
    public string Key => nameof(GoldManager);   // 고유 키 설정


    /// <summary>
    /// 골드 추가
    /// </summary>
    public static void AddGold(int amount)
    {
        CurrentGold += amount;
        //NotifyChanged();
        QuestManager.Instance.UpdateQuestProgress(QuestType.CollectGold, amount);
    }

    /// <summary>
    /// 골드 감소
    /// </summary>
    public static void ReduceGold(int amount)
    {
        if (CurrentGold >= amount)
        {
            CurrentGold -= amount;
            //NotifyChanged();
            QuestManager.Instance.UpdateQuestProgress(QuestType.CollectGold, -amount);
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
        return CurrentGold;
    }

    /// <summary>
    /// 골드 충분한지 체크
    /// </summary>
    public static bool HasEnoughGold(int cost)
    {
        return CurrentGold >= cost;
    }

    /// <summary>
    /// 골드 변경 이벤트 호출
    /// </summary>
    private static void NotifyChanged()
    {
        Debug.Log("골드 변경 이벤트 호출!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        OnGoldChanged?.Invoke(_currentGold);
    }

    public void NotifyLoaded()
    {
        Debug.Log("골드 변경 이벤트 호출!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        OnGoldChanged?.Invoke(_currentGold);
    }
}
