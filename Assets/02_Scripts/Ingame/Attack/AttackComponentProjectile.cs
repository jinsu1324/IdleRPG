using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponentProjectile : AttackComponentBase
{
    [SerializeField] private Projectile _projectilePrefab;   // 프로젝타일 프리팹
    [SerializeField] private Transform _spawnPoint;          // 스폰 위치

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
        int finalDamage = CalculateDamage();
        SpawnProjectile(finalDamage);  // 프로젝타일 생성
    }

    /// <summary>
    /// 프로젝타일 생성
    /// </summary>
    private void SpawnProjectile(int attackPower)
    {
        Projectile projectile = GameObject.Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
        projectile.Init(attackPower, _spawnPoint.position, _isCritical);
    }
}
