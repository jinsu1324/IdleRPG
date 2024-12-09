using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 10f;         // ����ü �ӵ�
    private int _attackPower;           // ���ݷ�
    private IDamagable _target;         // Ÿ��
    private Vector3 _spawnPos;          // ����ü ���� ��ġ
    private bool _isCritical;

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(int attackPower, Vector3 spawnPos, bool isCritical)
    {
        _attackPower = attackPower;
        _spawnPos = spawnPos;
        _isCritical = isCritical;

        FindTarget(); // Ÿ�� �˻�
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (_target == null || _target.IsDead)
        {
            //Debug.Log("Ÿ�پ���!!");
            Destroy(gameObject); // Ÿ���� ������� ����ü ����
            return;
        }
        else
        {
            // Ÿ�� �������� �̵�
            Vector3 direction = ((_target as Component).transform.position - transform.position).normalized;
            transform.Translate(direction * _speed * Time.deltaTime);

            // Ÿ�ٿ� �����ߴ��� Ȯ�� �� ����
            if (Vector3.Distance(transform.position, (_target as Component).transform.position) < 0.1f)
                AttackTargetEnemy();
        }
    }

    /// <summary>
    /// Ÿ�� ã��
    /// </summary>
    private void FindTarget()
    {
        _target = FieldTargetManager.Instance.GetClosestLivingTarget(_spawnPos);
    }

    /// <summary>
    /// Ÿ�� ����
    /// </summary>
    private void AttackTargetEnemy()
    {
        _target.TakeDamage(_attackPower, _isCritical);
        Destroy(gameObject);
    }
}
