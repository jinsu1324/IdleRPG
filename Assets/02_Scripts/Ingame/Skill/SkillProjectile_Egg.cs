using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile_Egg : MonoBehaviour
{
    private float _finalDamage;         // ���� ������
    private bool _isCritical;           // ũ��Ƽ�� ����
    private float _speed = 12f;         // ����ü �ӵ�
    private IDamagable _target;         // Ÿ��

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(float finalDamage, bool isCritical, IDamagable target)
    {
        _finalDamage = finalDamage;
        _isCritical = isCritical;
        _target = target;
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

        // Ÿ�� �������� �̵�
        Vector3 direction = ((_target as Component).transform.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);

        // Ÿ�ٿ� �����ߴ��� Ȯ�� �� ����
        if (Vector3.Distance(transform.position, (_target as Component).transform.position) < 0.1f)
            AttackTargetEnemy();
    }

    /// <summary>
    /// Ÿ�� ����
    /// </summary>
    private void AttackTargetEnemy()
    {
        if (_target == null || _target.IsDead)
        {
            //Debug.Log("Ÿ�پ���!!");
            Destroy(gameObject); // Ÿ���� ������� ����ü ����
            return;
        }

        TakeDamageArgs takeDamageArgs = new TakeDamageArgs()
        {
            Damage = _finalDamage,
            IsCritical = _isCritical,
        };
        _target.TakeDamage(takeDamageArgs);

        Destroy(gameObject);
    }
}
