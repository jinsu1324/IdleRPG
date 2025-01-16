using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

/// <summary>
/// 플레이어 코어
/// </summary>
public class Player : SerializedMonoBehaviour
{
    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        GetComponent<HPComponent_Player>().OnDeadPlayer += PlayerDeadTask;  // 플레이어 죽었을때, 플레이어 죽음에 필요한 태스크들 처리
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        GetComponent<HPComponent_Player>().OnDeadPlayer -= PlayerDeadTask;
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        PlayerStatArgs args = PlayerStats.GetCurrentPlayerStatArgs(); // 현재 스탯들 가져오기
        GetComponent<HPComponent_Player>().Init(args.MaxHp); // HP 컴포넌트 초기값 설정
        GetComponent<AttackComponent_Player>().Init(args.AttackPower, args.AttackSpeed); // 공격 컴포넌트 초기값 설정
    }

    /// <summary>
    /// 플레이어 죽었을 때 처리할 것들
    /// </summary>
    private void PlayerDeadTask()
    {
        StageManager.Instance.DefeatRestartGame(); // 스테이지 패배로 재시작
    }
}
