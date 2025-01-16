using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile_Meteor : MonoBehaviour
{
    private float _finalDamage; // ���� ������
    private bool _isCritical;   // ũ��Ƽ�� ����

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(float finalDamage, bool isCritical)
    {
        _finalDamage = finalDamage;
        _isCritical = isCritical;
    }

    /// <summary>
    /// ���� �浹�ϸ� ���� ó�� (Ʈ����)
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<HPComponent>().TakeDamage(_finalDamage, _isCritical);
            Destroy(gameObject);
        }
    }
}
