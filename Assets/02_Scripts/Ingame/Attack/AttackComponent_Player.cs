using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent_Player : AttackComponent
{
    [SerializeField] private Projectile _projectilePrefab;  // 프로젝타일 프리팹
    [SerializeField] private Transform _spawnPoint;         // 스폰 위치

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += Update_AttackPower_And_AttackSpeed;   // 플레이어스탯 바뀔때 -> 공격력과 공격속도 업데이트
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= Update_AttackPower_And_AttackSpeed;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (IsAttackCoolTime()) // 쿨타임마다 공격 프로세스 시작
            StartAttackProcess();
    }

    /// <summary>
    /// 실제 공격
    /// </summary>
    protected override void Attack()
    {
        // 치명타 여부 결정하고, 최종데미지 계산하고
        bool isCritical = CriticalManager.IsCritical();
        float finalDamage = CriticalManager.CalculateFinalDamage(_attackPower, isCritical);

        // 프로젝타일 생성 및 주입
        SpawnProjectile(finalDamage, isCritical);  
    }

    /// <summary>
    /// 프로젝타일 생성
    /// </summary>
    private void SpawnProjectile(float finalDamage, bool isCritical)
    {
        Projectile projectile = GameObject.Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
        projectile.Init(finalDamage, isCritical, _spawnPoint.position); // 최종데미지, 치명타여부 등 주입
    }

    /// <summary>
    /// 공격력과 공격속도 업데이트
    /// </summary>
    public void Update_AttackPower_And_AttackSpeed(PlayerStatArgs args)
    {
        _attackPower = args.AttackPower;
        _attackSpeed = args.AttackSpeed;
        _attackCooldown = 1f / _attackSpeed;
    }
}
