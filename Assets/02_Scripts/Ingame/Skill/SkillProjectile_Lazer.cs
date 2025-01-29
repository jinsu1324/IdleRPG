using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile_Lazer : MonoBehaviour
{
    private float _finalDamage;     // ���� ������
    private bool _isCritical;       // ũ��Ƽ�� ����
    private float _moveSpeed = 10;  // ����ü �ӵ�
    private float _time = 0;        // ����Ÿ�� ����� �ð�
    private float _lifeTime = 5;    // ����ü ����ִ� �ð�
    private float _hitCount = 0;    // ���� �� ��
    private float _maxHitCount = 3; // �ִ� Ÿ�� ��
    private BoxCollider2D _col;     // �ݶ��̴�

    /// <summary>
    /// �ʱ�ȭ
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
    /// ���� �浹�ϸ� ���� ó�� (Ʈ����)
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (_hitCount >= _maxHitCount) return; // �̹� �ִ�Ÿ�� �� �Ѿ����� ����

            TakeDamageArgs args = new TakeDamageArgs()
            {
                Damage = _finalDamage,
                IsCritical = _isCritical
            };
            collision.gameObject.GetComponent<HPComponent>().TakeDamage(args);

            _hitCount++; // Ÿ���� �� ī��Ʈ ����

            if (_hitCount >= _maxHitCount)
                _col.enabled = false; // �ִ� ���� �ʰ��ϸ� �浹 ��Ȱ��ȭ
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public void DestroyFX()
    {
        Destroy(gameObject);
    }
}
