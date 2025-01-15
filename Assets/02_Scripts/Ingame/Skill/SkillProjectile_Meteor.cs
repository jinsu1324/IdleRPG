using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile_Meteor : MonoBehaviour
{
    private float _finalDamage;
    private bool _isCritical;

    public void Init(float finalDamage, bool isCritical)
    {
        _finalDamage = finalDamage;
        _isCritical = isCritical;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<HPComponent>().TakeDamage(_finalDamage, _isCritical);
            
            Debug.Log($"Enemy 에게 {_finalDamage} 데미지를 줌!");

            Destroy(gameObject);
        }
    }
}
