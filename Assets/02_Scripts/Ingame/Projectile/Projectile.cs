using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 10f;         // ����ü �ӵ�
    private int _attackPower;           // ���ݷ�
    private Enemy _targetEnemy;         // Ÿ�� ���ʹ�      --------------> Enemy�� �ƴ϶� Animal�̸� ��Ұǵ�? �̷�����
    private Vector3 _spawnPos;          // ����ü ���� ��ġ

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(int attackPower, Vector3 spawnPos)
    {
        _attackPower = attackPower;
        _spawnPos = spawnPos;

        FindTarget(); // Ÿ�� �˻�
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (_targetEnemy == null || _targetEnemy.IsDead)
        {
            Debug.Log("Ÿ�پ���!!");
            Destroy(gameObject); // Ÿ���� ������� ����ü ����
            return;
        }
        else
        {
            // Ÿ�� �������� �̵�
            Vector3 direction = (_targetEnemy.transform.position - transform.position).normalized;
            transform.Translate(direction * _speed * Time.deltaTime);

            // Ÿ�ٿ� �����ߴ��� Ȯ�� �� ����
            if (Vector3.Distance(transform.position, _targetEnemy.transform.position) < 0.1f)
                AttackTargetEnemy();
        }
    }

    /// <summary>
    /// Ÿ�� ã��
    /// </summary>
    private void FindTarget()
    {
        _targetEnemy = EnemyManager.Instance.GetClosestLivingEnemy(_spawnPos);
    }

    /// <summary>
    /// Ÿ�� ����
    /// </summary>
    private void AttackTargetEnemy()
    {
        _targetEnemy.GetComponent<HPComponent>().TakeDamage(_attackPower);
        Destroy(gameObject);
    }
}
