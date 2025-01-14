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
    public override void Init(int attackPower, int attackSpeed)
    {
        base.Init(attackPower, attackSpeed);

        _criticalRate = (float)PlayerStats.GetStatValue(StatType.CriticalRate) / 100.0f;
        _criticalMultiple = (float)PlayerStats.GetStatValue(StatType.CriticalMultiple) / 100.0f;
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
    private void SpawnProjectile(int attackPower)
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
        _criticalRate = (float)args.CriticalRate / 100.0f;
        _criticalMultiple = (float)args.CriticalMultiple / 100.0f;
    }

    /// <summary>
    /// ġ��Ÿ�� ����Ͽ� ���� �������� ��ȯ
    /// </summary>
    protected int CalculateFinalDamage()
    {
        bool isCritical = Random.value <= _criticalRate;    // ġ��Ÿ ���� ����

        if (isCritical)
        {
            _isCritical = true;
            return (int)(_attackPower * _criticalMultiple); // ġ��Ÿ ���� ����
        }
        else
        {
            _isCritical = false;
            return _attackPower;
        }
    }
}
