using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile_Meteor : MonoBehaviour
{
    private float _finalDamage; // 최종 데미지
    private bool _isCritical;   // 크리티컬 여부

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(float finalDamage, bool isCritical)
    {
        _finalDamage = finalDamage;
        _isCritical = isCritical;
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

            Destroy(gameObject); 
            // Todo 맞아야 게임오브젝트가 사라지게 되어있는데, Enemy가 사라질수있음 그때 처리 필요
        }
    }
}
