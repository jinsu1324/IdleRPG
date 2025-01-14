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
        PlayerStats.OnPlayerStatChanged += Update_AttackPower;   // �÷��̾�� �ٲ� -> ���ݷ� ������Ʈ
        PlayerStats.OnPlayerStatChanged += Update_AttackSpeed;   // �÷��̾�� �ٲ� -> ���ݼӵ� ������Ʈ
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= Update_AttackPower;
        PlayerStats.OnPlayerStatChanged -= Update_AttackSpeed;
    }


    public override void Init(int attackPower, int attackSpeed)
    {
        base.Init(attackPower, attackSpeed);

        _criticalRate = ((float)PlayerStats.GetStatValue(StatType.CriticalRate) / 100.0f);
        _criticalMultiple = ((float)PlayerStats.GetStatValue(StatType.CriticalMultiple) / 100.0f);
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

    /// <summary>
    /// ���ݷ� ����
    /// </summary>
    public void Update_AttackPower(PlayerStatArgs args)
    {
        _attackPower = args.AttackPower;
    }

    /// <summary>
    /// ���ݼӵ� ����
    /// </summary>
    public void Update_AttackSpeed(PlayerStatArgs args)
    {
        _attackSpeed = args.AttackSpeed;
        _attackCooldown = 1f / _attackSpeed;
    }

    /// <summary>
    /// ġ��Ÿ�� ����Ͽ� ���� �������� ��ȯ
    /// </summary>
    protected int CalculateDamage()
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
