using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile_Meteor : MonoBehaviour
{
    private float _finalDamage; // ���� ������
    private bool _isCritical;   // ũ��Ƽ�� ����
    private BoxCollider2D _col; // �ݶ��̴�

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(float finalDamage, bool isCritical)
    {
        _finalDamage = finalDamage;
        _isCritical = isCritical;
        _col = GetComponent<BoxCollider2D>();
        _col.enabled = false;
    }

    /// <summary>
    /// ���� �浹�ϸ� ���� ó�� (Ʈ����)
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
    /// �ݶ��̴� �ѱ� (�ִϸ��̼� Ű�����ӿ��� ����)
    /// </summary>
    public void Collider_ON()
    {
        _col.enabled = true;
    }

    /// <summary>
    /// ���� (�ִϸ��̼� Ű�����ӿ��� ����)
    /// </summary>
    public void DestroyFX()
    {
        Destroy(gameObject);
    }
}
