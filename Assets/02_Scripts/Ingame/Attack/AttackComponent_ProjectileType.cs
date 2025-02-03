using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent_ProjectileType : AttackComponent
{
    [SerializeField] private Projectile _projectilePrefab;  // ������Ÿ�� ������
    [SerializeField] private Transform _spawnPoint;         // ���� ��ġ
    private bool _isAttackPause;                            // �̵����϶� ���� �Ͻ����� ����

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (_isAttackPause)
            return;

        if (IsAttackCoolTime()) // ��Ÿ�Ӹ��� ���� ���μ��� ����
            StartAttackProcess();
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    protected override void Attack()
    {
        // ġ��Ÿ ���� �����ϰ�, ���������� ����ϰ�
        bool isCritical = CriticalManager.IsCritical();
        float finalDamage = CriticalManager.CalculateFinalDamage(_attackPower, isCritical);

        // ������Ÿ�� ����
        AttackArgs attackArgs = new AttackArgs()
        {
            AttackPower = finalDamage,
            IsCritical = isCritical,
            projectileSpawnPos = _spawnPoint.position,
        };
        SpawnProjectile(attackArgs);  
    }

    /// <summary>
    /// ������Ÿ�� ����
    /// </summary>
    private void SpawnProjectile(AttackArgs args)
    {
        Projectile projectile = GameObject.Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
        projectile.Init(args);
    }

    /// <summary>
    /// �����ߴ� true��
    /// </summary>
    public void IsAttackStop_True() => _isAttackPause = true;

    /// <summary>
    /// �����ߴ� false ��
    /// </summary>
    public void IsAttackStop_False(OnStageChangedArgs args) => _isAttackPause = false;
}
