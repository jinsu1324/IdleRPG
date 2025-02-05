using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent_ProjectileType : AttackComponent
{
    [SerializeField] private Projectile _projectilePrefab;  // ������Ÿ�� ������
    [SerializeField] private Transform _spawnPoint;         // ���� ��ġ
    private bool _isAttackPause;                            // �̵����϶� ���� �Ͻ����� ����
    private IDamagable _target;                             // Ÿ��

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        // �̵����϶� ���� �Ͻ�����
        if (_isAttackPause)
            return;

        // ��Ÿ�Ӹ��� ���� ���μ��� ����
        if (IsAttackCoolTime()) 
        {
            // Ÿ�پ����� �׳� ����
            _target = FieldTargetManager.GetClosestLivingTarget(transform.position);
            if (_target == null)
                return;

            StartAttackProcess();
        }
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
            Target = _target,
        };
        SpawnProjectile(attackArgs);

        SoundManager.Instance.PlaySFX(SFXType.SFX_PlayerAttack);
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
    /// �����Ͻ��ߴ�
    /// </summary>
    public void AttackPause(StageBuildArgs args) => _isAttackPause = true;

    /// <summary>
    /// �����簳
    /// </summary>
    public void AttackResume(StageBuildArgs args) => _isAttackPause = false;
}
