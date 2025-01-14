using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 리셋 관련 일 처리
/// </summary>
public class PlayerResetService
{
    public static event Action OnReset; // 플레이어 컴포넌트들 등 리셋 이벤트

    /// <summary>
    /// 플레이어 컴포넌트들 등 리셋 (이벤트 알림)
    /// </summary>
    public static void PlayerReset()
    {
        OnReset?.Invoke();
    }
}
