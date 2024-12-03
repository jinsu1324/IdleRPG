using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : IAttackable
{
    private Projectile _projectilePrefab;   // 프로젝타일 프리팹
    private Transform _spawnPoint;          // 스폰 위치

    /// <summary>
    /// 생성자
    /// </summary>
    public ProjectileAttack(Projectile projectilePrefab, Transform spawnPoint)
    {
        _projectilePrefab = projectilePrefab;
        _spawnPoint = spawnPoint;
    }

    /// <summary>
    /// 공격 실행
    /// </summary>
    public void ExecuteAttack(int attackPower)
    {
        Projectile projectile = GameObject.Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
        projectile.Init(attackPower, _spawnPoint.position);
    }
}
