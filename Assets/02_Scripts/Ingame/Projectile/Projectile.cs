using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 10f;         // 투사체 속도
    private int _attackPower;           // 공격력
    private Enemy _targetEnemy;         // 타겟 에너미

    /// <summary>
    /// 초기화
    /// </summary>
    public void Initialize(Enemy targetEnemy, int attackPower)
    {
        _targetEnemy = targetEnemy;
        _attackPower = attackPower;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (_targetEnemy == null || _targetEnemy.IsDead)
        {
            Destroy(gameObject); // 타겟이 사라지면 투사체 제거
            return;
        }

        // 타겟 방향으로 이동
        Vector3 direction = (_targetEnemy.transform.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);

        // 타겟에 도달했는지 확인 및 공격
        if (Vector3.Distance(transform.position, _targetEnemy.transform.position) < 0.1f)
            AttackTargetEnemy();

    }

    /// <summary>
    /// 타겟 공격
    /// </summary>
    private void AttackTargetEnemy()
    {
        _targetEnemy.TakeDamage(_attackPower);
        Destroy(gameObject);
    }
}
