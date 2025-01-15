using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile_Meteor : MonoBehaviour
{
    private float _attackPower;

    public void SetAtk(float attackPercentage)
    {
        _attackPower = PlayerStats.GetStatValue(StatType.AttackPower) * attackPercentage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<HPComponent>().TakeDamage(_attackPower, false);
            
            Debug.Log($"Enemy 에게 {_attackPower} 데미지를 줌!");

            Destroy(gameObject);
        }
    }

    


}
