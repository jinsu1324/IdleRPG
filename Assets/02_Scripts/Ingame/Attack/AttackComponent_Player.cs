using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent_Player : AttackComponent
{
    [SerializeField] private Projectile _projectilePrefab;  // ������Ÿ�� ������
    [SerializeField] private Transform _spawnPoint;         // ���� ��ġ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += Update_AttackPower_And_AttackSpeed;   // �÷��̾�� �ٲ� -> ���ݷ°� ���ݼӵ� ������Ʈ
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

        // ������Ÿ�� ���� �� ����
        SpawnProjectile(finalDamage, isCritical);  
    }

    /// <summary>
    /// ������Ÿ�� ����
    /// </summary>
    private void SpawnProjectile(float finalDamage, bool isCritical)
    {
        Projectile projectile = GameObject.Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
        projectile.Init(finalDamage, isCritical, _spawnPoint.position); // ����������, ġ��Ÿ���� �� ����
    }

    /// <summary>
    /// ���ݷ°� ���ݼӵ� ������Ʈ
    /// </summary>
    public void Update_AttackPower_And_AttackSpeed(PlayerStatArgs args)
    {
        _attackPower = args.AttackPower;
        _attackSpeed = args.AttackSpeed;
        _attackCooldown = 1f / _attackSpeed;
    }
}
