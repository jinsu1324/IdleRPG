using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : IAttackable
{
    private Projectile _projectilePrefab;   // ������Ÿ�� ������
    private Transform _spawnPoint;          // ���� ��ġ

    /// <summary>
    /// ������
    /// </summary>
    public ProjectileAttack(Projectile projectilePrefab, Transform spawnPoint)
    {
        _projectilePrefab = projectilePrefab;
        _spawnPoint = spawnPoint;
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void ExecuteAttack(int attackPower)
    {
        Projectile projectile = GameObject.Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
        projectile.Init(attackPower, _spawnPoint.position);
    }
}
