using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent_Player : AttackComponent
{
    [SerializeField] private Projectile _projectilePrefab;  // 프로젝타일 프리팹
    [SerializeField] private Transform _spawnPoint;         // 스폰 위치
    protected float _criticalRate;                          // 치명타 확률
    protected float _criticalMultiple;                      // 치명타 피해 배율
    protected bool _isCritical;                             // 치명타 공격인지

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += Update_AttackGroup;   // 플레이어스탯 바뀔때 -> 공격력 관련 수치들 업데이트
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= Update_AttackGroup;
    }

    /// <summary>
    /// 초기화
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
        if (IsAttackCoolTime()) // 쿨타임마다 공격 프로세스 시작
            StartAttackProcess();
    }

    /// <summary>
    /// 실제 공격
    /// </summary>
    protected override void Attack()
    {
        SpawnProjectile(CalculateFinalDamage());  // 프로젝타일 생성
    }

    /// <summary>
    /// 프로젝타일 생성
    /// </summary>
    private void SpawnProjectile(int attackPower)
    {
        Projectile projectile = GameObject.Instantiate(_projectilePrefab, _spawnPoint.position, Quaternion.identity);
        projectile.Init(attackPower, _spawnPoint.position, _isCritical);
    }

    /// <summary>
    /// 공격력 관련 수치들 업데이트
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
    /// 치명타를 계산하여 최종 데미지를 반환
    /// </summary>
    protected int CalculateFinalDamage()
    {
        bool isCritical = Random.value <= _criticalRate;    // 치명타 여부 결정

        if (isCritical)
        {
            _isCritical = true;
            return (int)(_attackPower * _criticalMultiple); // 치명타 피해 적용
        }
        else
        {
            _isCritical = false;
            return _attackPower;
        }
    }
}
