using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 10f;
    private int _attackPower;
    private Enemy _targetEnemy;

    public void Initialize(Enemy targetEnemy, int attackPower)
    {
        _targetEnemy = targetEnemy;
        _attackPower = attackPower;
    }

    private void Update()
    {
        if (_targetEnemy == null)
        {
            Destroy(gameObject); // 타겟이 사라지면 투사체 제거
            return;
        }

        // 타겟 방향으로 이동
        Vector3 direction = (_targetEnemy.transform.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);

        // 타겟에 도달했는지 확인
        if (Vector3.Distance(transform.position, _targetEnemy.transform.position) < 0.1f)
        {
            AttackTarget();
        }

    }

    private void AttackTarget()
    {
        Debug.Log($"타겟 공격: {_targetEnemy.name}");
        _targetEnemy.TakeDamage(_attackPower);

        // 투사체 제거
        Destroy(gameObject);
    }
}
