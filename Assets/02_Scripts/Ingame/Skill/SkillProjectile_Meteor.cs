using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile_Meteor : MonoBehaviour
{
    private int _attackPower;

    public void SetAtk(float attackPercentage)
    {
        float calculateAttackPercentage = attackPercentage / 100.0f;
        _attackPower = (int)Mathf.Floor(PlayerStats.GetStatValue(StatType.AttackPower) * calculateAttackPercentage);
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
