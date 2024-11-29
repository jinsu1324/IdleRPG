using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SerializedMonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;  // 투사체 프리팹 

    private int _attackPower;               // 공격력
    private int _attackDelay;               // 공격속도
    private int _maxHp;                     // 최대체력
    private int _critical;                  // 크리티컬 확률
    private int _currentHp;                 // 현재체력

    private float _time;                    // 쿨타임 시간 계산용

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize(PlayerData playerData)
    {
        UpdateStat(playerData);
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (IsAttackCoolTime())
            Attack();   
    }

    /// <summary>
    /// 공격 쿨타임 계산
    /// </summary>
    private bool IsAttackCoolTime()
    {
        _time += Time.deltaTime;

        if (_time >= _attackDelay)
        {
            _time %= _attackDelay;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 공격
    /// </summary>
    private void Attack()
    {
        // 타겟 찾기
        Enemy targetEnemy = EnemyManager.Instance.Get_ClosestEnemy(transform.position);
        if (targetEnemy == null)
        {
            Debug.Log("타겟 없음!");
            return;
        }

        Debug.Log("Player Attack");
        // 투사체 생성 및 초기화
        Projectile projectile = Instantiate(_projectilePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        projectile.Initialize(targetEnemy, _attackPower);
    }

    /// <summary>
    /// 스탯 업데이트
    /// </summary>
    public void UpdateStat(PlayerData playerData)
    {
        Debug.Log("플레이어 프리팹 스탯 업데이트!");
        _attackPower = playerData.GetStat(StatID.AttackPower.ToString()).Value;
        _attackDelay = playerData.GetStat(StatID.AttackDelay.ToString()).Value;
        _maxHp = playerData.GetStat(StatID.MaxHp.ToString()).Value;
        _critical = playerData.GetStat(StatID.Critical.ToString()).Value;
        _currentHp = _maxHp;
    }

    
    /// <summary>
    /// 데미지 받음
    /// </summary>
    public void TakeDamage(int atk)
    {
        _currentHp -= atk;

        //Debug.Log($"{atk}의 데미지를 받음! 남은 체력 : {_currentHp}");
        //if (_currentHp <= 0)
        //    Die();
    }

    private void Die()
    {

    }


}
