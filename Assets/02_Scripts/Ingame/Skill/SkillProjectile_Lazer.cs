using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile_Lazer : MonoBehaviour
{
    private float _finalDamage;     // 최종 데미지
    private bool _isCritical;       // 크리티컬 여부
    private float _moveSpeed = 10;  // 투사체 속도
    private float _time = 0;        // 삭제타임 계산할 시간
    private float _lifeTime = 5;    // 투사체 살아있는 시간
    private float _hitCount = 0;    // 맞은 적 수
    private float _maxHitCount = 3; // 최대 타격 수
    private BoxCollider2D _col;     // 콜라이더

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(float finalDamage, bool isCritical)
    {
        _finalDamage = finalDamage;
        _isCritical = isCritical;
        _time = 0;
        _hitCount = 0;
        _col = GetComponent<BoxCollider2D>();
        _col.enabled = true;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        _time += Time.deltaTime;
        if (_time > _lifeTime)
            DestroyFX();

        transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
    }

    /// <summary>
    /// 적과 충돌하면 공격 처리 (트리거)
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (_hitCount >= _maxHitCount) return; // 이미 최대타격 수 넘었으면 무시

            TakeDamageArgs args = new TakeDamageArgs()
            {
                Damage = _finalDamage,
                IsCritical = _isCritical
            };
            collision.gameObject.GetComponent<HPComponent>().TakeDamage(args);

            _hitCount++; // 타격한 적 카운트 증가

            if (_hitCount >= _maxHitCount)
                _col.enabled = false; // 최대 개수 초과하면 충돌 비활성화
        }
    }

    /// <summary>
    /// 삭제
    /// </summary>
    public void DestroyFX()
    {
        Destroy(gameObject);
    }
}
