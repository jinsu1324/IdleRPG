using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent_ProjectileType : AttackComponent
{
    [SerializeField] private Projectile _projectilePrefab;  // 프로젝타일 프리팹
    [SerializeField] private Transform _spawnPoint;         // 스폰 위치
    private bool _isAttackPause;                            // 이동중일때 공격 일시정지 여부
    private IDamagable _target;                             // 타겟

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        // 이동중일때 공격 일시정지
        if (_isAttackPause)
            return;

        // 쿨타임마다 공격 프로세스 시작
        if (IsAttackCoolTime()) 
        {
            // 타겟없으면 그냥 리턴
            _target = FieldTargetManager.GetClosestLivingTarget(transform.position);
            if (_target == null)
                return;

            StartAttackProcess();
        }
    }

    /// <summary>
    /// 실제 공격
    /// </summary>
    protected override void Attack()
    {
        // 치명타 여부 결정하고, 최종데미지 계산하고
        bool isCritical = CriticalManager.IsCritical();
        float finalDamage = CriticalManager.CalculateFinalDamage(_attackPower, isCritical);

        // 프로젝타일 생성
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
    /// 프로젝타일 생성
    /// </summary>
    private void SpawnProjectile(AttackArgs args)
    {
        Projectile projectile = GameObject.Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
        projectile.Init(args);
    }

    /// <summary>
    /// 공격일시중단
    /// </summary>
    public void AttackPause(StageBuildArgs args) => _isAttackPause = true;

    /// <summary>
    /// 공격재개
    /// </summary>
    public void AttackResume(StageBuildArgs args) => _isAttackPause = false;
}
