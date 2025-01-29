using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile_Egg : MonoBehaviour
{
    private float _finalDamage;         // 최종 데미지
    private bool _isCritical;           // 크리티컬 여부
    private float _speed = 12f;         // 투사체 속도
    private IDamagable _target;         // 타겟

    /// <summary>
    /// 초기화
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
            //Debug.Log("타겟없음!!");
            Destroy(gameObject); // 타겟이 사라지면 투사체 제거
            return;
        }

        // 타겟 방향으로 이동
        Vector3 direction = ((_target as Component).transform.position - transform.position).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);

        // 타겟에 도달했는지 확인 및 공격
        if (Vector3.Distance(transform.position, (_target as Component).transform.position) < 0.1f)
            AttackTargetEnemy();
    }

    /// <summary>
    /// 타겟 공격
    /// </summary>
    private void AttackTargetEnemy()
    {
        if (_target == null || _target.IsDead)
        {
            //Debug.Log("타겟없음!!");
            Destroy(gameObject); // 타겟이 사라지면 투사체 제거
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
