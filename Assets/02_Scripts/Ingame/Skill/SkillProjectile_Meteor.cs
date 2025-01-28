using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile_Meteor : MonoBehaviour
{
    private float _finalDamage; // 최종 데미지
    private bool _isCritical;   // 크리티컬 여부
    private BoxCollider2D _col; // 콜라이더

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(float finalDamage, bool isCritical)
    {
        _finalDamage = finalDamage;
        _isCritical = isCritical;
        _col = GetComponent<BoxCollider2D>();
        _col.enabled = false;
    }

    /// <summary>
    /// 적과 충돌하면 공격 처리 (트리거)
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            TakeDamageArgs args = new TakeDamageArgs()
            {
                Damage = _finalDamage,
                IsCritical = _isCritical
            };
            collision.gameObject.GetComponent<HPComponent>().TakeDamage(args);
            _col.enabled = false;
        }
    }

    /// <summary>
    /// 콜라이더 켜기 (애니메이션 키프레임에서 설정)
    /// </summary>
    public void Collider_ON()
    {
        _col.enabled = true;
    }

    /// <summary>
    /// 삭제 (애니메이션 키프레임에서 설정)
    /// </summary>
    public void DestroyFX()
    {
        Destroy(gameObject);
    }
}
