using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// 플레이어 코어
/// </summary>
public class PlayerCore : SerializedMonoBehaviour
{
    [SerializeField] private HPComponent _hpComponent;                          // HP 컴포넌트
    [SerializeField] private AttackComponent_ProjectileType _attackComponent;   // 공격 컴포넌트 (프로젝타일타입)
    [SerializeField] private AnimComponent _animComponent;                      // 애니메이션 컴포넌트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _hpComponent.OnDie += PlayerDieTask;  // 플레이어 죽었을때, 플레이어 죽음에 필요한 태스크들 처리

        PlayerStats.OnPlayerStatChanged += _hpComponent.ChangeMaxHp; // 플레이어 스탯 변경되었을 때 -> 플레이어의 최대체력 변경
        PlayerStats.OnPlayerStatChanged += Update_PlayerAttackValues; // 플레이어 스탯 변경되었을 때 -> 플레이어의 공격관련 수치들 업데이트

        PlayerResetService.OnReset += _hpComponent.ResetHp;  // 플레이어 리셋할때 -> 플레이어의 HP 리셋
        PlayerResetService.OnReset += _hpComponent.ResetIsDead;  // 플레이어 리셋할때 -> 플레이어의 죽었는지 bool값도 리셋

        StageManager.OnStageBuilding += _attackComponent.IsAttackStop_True;    // 스테이지 변경중일때 -> 공격중단 true
        StageManager.OnStageBuilding += _animComponent.MoveAnimStart;   // 스테이지 변경중일 때 -> 움직임 애니메이션 시작

        StageManager.OnStageBuildFinish += _attackComponent.IsAttackStop_False; // 스테이지 변경 끝났을때 -> 공격중단 false
        StageManager.OnStageBuildFinish += _animComponent.MoveAnimStop; // 스테이지 변경 끝났을때 -> 움직임 애니메이션 종료
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _hpComponent.OnDie -= PlayerDieTask;

        PlayerStats.OnPlayerStatChanged -= _hpComponent.ChangeMaxHp; 
        PlayerStats.OnPlayerStatChanged -= Update_PlayerAttackValues;

        PlayerResetService.OnReset -= _hpComponent.ResetHp;
        PlayerResetService.OnReset -= _hpComponent.ResetIsDead;

        StageManager.OnStageBuilding -= _attackComponent.IsAttackStop_True;
        StageManager.OnStageBuilding -= _animComponent.MoveAnimStart;

        StageManager.OnStageBuildFinish -= _attackComponent.IsAttackStop_False;
        StageManager.OnStageBuildFinish -= _animComponent.MoveAnimStop;
    }

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        // HP 컴포넌트 초기화
        HPInitArgs hpInitArgs = new HPInitArgs() 
        { 
            MaxHp = PlayerStats.GetStatValue(StatType.MaxHp) 
        };
        _hpComponent.Init(hpInitArgs);


        // 공격 컴포넌트 초기화
        AttackInitArgs attackInitArgs = new AttackInitArgs()
        {
            AttackPower = PlayerStats.GetStatValue(StatType.AttackPower),
            AttackSpeed = PlayerStats.GetStatValue(StatType.AttackSpeed)
        };
        _attackComponent.Init(attackInitArgs);
    }

    /// <summary>
    /// 플레이어의 공격관련 수치들 업데이트
    /// </summary>
    private void Update_PlayerAttackValues(PlayerStatArgs args)
    {
        AttackUpdateArgs attackUpdateArgs = new AttackUpdateArgs()
        {
            AttackPower = args.AttackPower,
            AttackSpeed = args.AttackSpeed,
        };
        _attackComponent.Update_AttackPower(attackUpdateArgs);
        _attackComponent.Update_AttackSpeed(attackUpdateArgs);
    }

    /// <summary>
    /// 플레이어가 죽었을 때 처리할 것들
    /// </summary>
    private void PlayerDieTask()
    {
        StageManager.Instance.DefeatRestartGame(); // 스테이지 패배로 재시작
    }
}
