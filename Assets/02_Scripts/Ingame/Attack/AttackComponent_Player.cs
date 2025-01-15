using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent_Player : AttackComponent
{
    [SerializeField] private Projectile _projectilePrefab;  // ������Ÿ�� ������
    [SerializeField] private Transform _spawnPoint;         // ���� ��ġ
    protected float _criticalRate;                          // ġ��Ÿ Ȯ��
    protected float _criticalMultiple;                      // ġ��Ÿ ���� ����
    protected bool _isCritical;                             // ġ��Ÿ ��������

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += Update_AttackGroup;   // �÷��̾�� �ٲ� -> ���ݷ� ���� ��ġ�� ������Ʈ
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= Update_AttackGroup;
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public override void Init(float attackPower, float attackSpeed)
    {
        base.Init(attackPower, attackSpeed);

        _criticalRate = PlayerStats.GetStatValue(StatType.CriticalRate);
        _criticalMultiple = PlayerStats.GetStatValue(StatType.CriticalMultiple);
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
        SpawnProjectile(CalculateFinalDamage());  // ������Ÿ�� ����
    }

    /// <summary>
    /// ������Ÿ�� ����
    /// </summary>
    private void SpawnProjectile(float attackPower)
    {
        Projectile projectile = GameObject.Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
        projectile.Init(attackPower, _spawnPoint.position, _isCritical);
    }

    /// <summary>
    /// ���ݷ� ���� ��ġ�� ������Ʈ
    /// </summary>
    public void Update_AttackGroup(PlayerStatArgs args)
    {
        _attackPower = args.AttackPower;
        _attackSpeed = args.AttackSpeed;
        _attackCooldown = 1f / _attackSpeed;
        _criticalRate = args.CriticalRate;
        _criticalMultiple = args.CriticalMultiple;
    }

    /// <summary>
    /// ġ��Ÿ�� ����Ͽ� ���� �������� ��ȯ
    /// </summary>
    protected float CalculateFinalDamage()
    {
        bool isCritical = Random.value <= _criticalRate;    // ġ��Ÿ ���� ����

        if (isCritical)
        {
            _isCritical = true;
            return _attackPower * _criticalMultiple; // ġ��Ÿ ���� ����
        }
        else
        {
            _isCritical = false;
            return _attackPower;
        }
    }
}
