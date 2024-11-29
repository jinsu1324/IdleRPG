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
            Destroy(gameObject); // Ÿ���� ������� ����ü ����
            return;
        }

        // Ÿ�� �������� �̵�
        Vector3 direction = (_targetEnemy.transform.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);

        // Ÿ�ٿ� �����ߴ��� Ȯ��
        if (Vector3.Distance(transform.position, _targetEnemy.transform.position) < 0.1f)
        {
            AttackTarget();
        }

    }

    private void AttackTarget()
    {
        Debug.Log($"Ÿ�� ����: {_targetEnemy.name}");
        _targetEnemy.TakeDamage(_attackPower);

        // ����ü ����
        Destroy(gameObject);
    }
}
