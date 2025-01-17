using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 10f;         // 투사체 속도
    private float _finalDamage;         // 최종 공격력
    private IDamagable _target;         // 타겟
    private Vector3 _spawnPos;          // 투사체 생성 위치
    private bool _isCritical;

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(AttackArgs args)
    {
        _finalDamage = args.AttackPower;
        _isCritical = args.IsCritical;
        _spawnPos = args.projectileSpawnPos;

        FindTarget(); // 타겟 검색
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (_target == null || _target.IsDead)
        {
            //Debug.Log("타겟없음!!");
            Destroy(gameObject); // 타겟이 사라지면 투사체 제거
            return;
        }
        else
        {
            // 타겟 방향으로 이동
            Vector3 direction = ((_target as Component).transform.position - transform.position).normalized;
            transform.Translate(direction * _speed * Time.deltaTime);

            // 타겟에 도달했는지 확인 및 공격
            if (Vector3.Distance(transform.position, (_target as Component).transform.position) < 0.1f)
                AttackTargetEnemy();
        }
    }

    /// <summary>
    /// 타겟 찾기
    /// </summary>
    private void FindTarget()
    {
        _target = FieldTargetManager.GetClosestLivingTarget(_spawnPos);
    }

    /// <summary>
    /// 타겟 공격
    /// </summary>
    private void AttackTargetEnemy()
    {
        TakeDamageArgs takeDamageArgs = new TakeDamageArgs()
        {
            Damage = _finalDamage,
            IsCritical = _isCritical,
        };
        _target.TakeDamage(takeDamageArgs);

        Destroy(gameObject);
    }
}
