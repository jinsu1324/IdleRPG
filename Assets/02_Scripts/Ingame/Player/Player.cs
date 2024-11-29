using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SerializedMonoBehaviour
{
    private int _attack;         // 공격력
    private int _attackDelay;    // 공격속도
    private int _maxHp;          // 최대체력
    private int _critical;       // 크리티컬 확률
    private int _currentHp;      // 현재체력

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize(PlayerData playerData)
    {
        UpdateStat(playerData);
    }

    /// <summary>
    /// 스탯 업데이트
    /// </summary>
    public void UpdateStat(PlayerData playerData)
    {
        _attack = playerData.GetStat(StatID.Attack.ToString()).Value;
        _attackDelay = playerData.GetStat(StatID.AttackDelay.ToString()).Value;
        _maxHp = playerData.GetStat(StatID.MaxHp.ToString()).Value;
        _critical = playerData.GetStat(StatID.Critical.ToString()).Value;
        _currentHp = _maxHp;

        Debug.Log($"_attack : {_attack}");
        Debug.Log($"_attackDelay : {_attackDelay}");
        Debug.Log($"_maxHp : {_maxHp}");
        Debug.Log($"_critical : {_critical}");
        Debug.Log($"_currentHp : {_currentHp}");
    }

    private void Attack()
    {

    }

    private void Damaged(int atk)
    {
        _currentHp -= atk;
        
        if (_currentHp <= 0)
            Died();
    }

    private void Died()
    {

    }


}
