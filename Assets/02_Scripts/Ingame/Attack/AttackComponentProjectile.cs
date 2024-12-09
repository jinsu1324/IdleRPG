using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponentProjectile : AttackComponentBase
{
    [SerializeField] private Projectile _projectilePrefab;   // ������Ÿ�� ������
    [SerializeField] private Transform _spawnPoint;          // ���� ��ġ

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (IsAttackCoolTime()) // ��Ÿ�Ӹ��� ���� ���μ��� ����
            StartAttackProcess();
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    protected override void Attack()
    {
        int finalDamage = CalculateDamage();
        SpawnProjectile(finalDamage);  // ������Ÿ�� ����
    }

    /// <summary>
    /// ������Ÿ�� ����
    /// </summary>
    private void SpawnProjectile(int attackPower)
    {
        Projectile projectile = GameObject.Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
        projectile.Init(attackPower, _spawnPoint.position, _isCritical);
    }
}
