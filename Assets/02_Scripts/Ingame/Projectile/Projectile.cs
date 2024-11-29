using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 10f;         // ����ü �ӵ�
    private int _attackPower;           // ���ݷ�
    private Enemy _targetEnemy;         // Ÿ�� ���ʹ�

    /// <summary>
    /// �ʱ�ȭ
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
            Destroy(gameObject); // Ÿ���� ������� ����ü ����
            return;
        }

        // Ÿ�� �������� �̵�
        Vector3 direction = (_targetEnemy.transform.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);

        // Ÿ�ٿ� �����ߴ��� Ȯ�� �� ����
        if (Vector3.Distance(transform.position, _targetEnemy.transform.position) < 0.1f)
            AttackTargetEnemy();

    }

    /// <summary>
    /// Ÿ�� ����
    /// </summary>
    private void AttackTargetEnemy()
    {
        _targetEnemy.TakeDamage(_attackPower);
        Destroy(gameObject);
    }
}
