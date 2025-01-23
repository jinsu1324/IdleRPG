using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 골드 관리자
/// </summary>
public class GoldManager// : ISavable
{
    public static event Action<int> OnGoldChange;       // 골드 변경 되었을 때 이벤트
    public string Key => nameof(GoldManager);           // Firebase 데이터 저장용 고유 키 설정
    [SaveField] private static int _currentGold;        // 현재 골드
    public static int CurrentGold                           
    {
        get => _currentGold;
        set
        {
            _currentGold = value;
            NotifyChanged(); // 값이 변경될 때 이벤트 호출
        }
    }

    /// <summary>
    /// 골드 추가
    /// </summary>
    public static void AddGold(int amount)
    {
        CurrentGold += amount;
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
            QuestManager.Instance.UpdateQuestProgress(QuestType.CollectGold, -amount);
        }
        else
            Debug.Log($"보유한 골드({CurrentGold})가 감소할 골드({amount})보다 적어서 골드감소가 불가능합니다!");
    }

    /// <summary>
    /// 골드 가져오기
    /// </summary>
    public static int GetGold() => CurrentGold; 

    /// <summary>
    /// 골드 충분한지 체크
    /// </summary>
    public static bool HasEnoughGold(int cost) => CurrentGold >= cost;

    /// <summary>
    /// 골드 변경 이벤트 호출
    /// </summary>
    private static void NotifyChanged() => OnGoldChange?.Invoke(CurrentGold);
}
