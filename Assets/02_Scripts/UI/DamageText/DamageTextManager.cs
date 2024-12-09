using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextManager : SingletonBase<DamageTextManager>
{
    [SerializeField] private DamageText _damageTextPrefab;          // ������ �ؽ�Ʈ ������
    private ObjectPool<DamageText> _damageTextPool;                 // ������ �ؽ�Ʈ Ǯ

    [SerializeField] private DamageText _criticalDamageTextPrefab;  // ũ��Ƽ�� ������ �ؽ�Ʈ ������
    private ObjectPool<DamageText> _criticalDamageTextPool;         // ũ��Ƽ�� ������ �ؽ�Ʈ Ǯ

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake(); // �̱��� ����

        _damageTextPool = new ObjectPool<DamageText>(_damageTextPrefab, 10, transform); // Ǯ ����
        _criticalDamageTextPool = new ObjectPool<DamageText>(_criticalDamageTextPrefab, 10, transform); // ũ�� Ǯ ����
    }

    /// <summary>
    /// ������ �׽�Ʈ ǥ��
    /// </summary>
    public void ShowDamageText(float damage, Vector3 position, bool isCritical)
    {
        DamageText damageText = null;

        if (isCritical) // ũ�� ���ο� ���� �ٸ� �ؽ�Ʈ������ Ǯ���� ��������
            damageText = _criticalDamageTextPool.GetObject();
        else
            damageText = _damageTextPool.GetObject();
        
        damageText.transform.position = position; // ��ǥ ����
        damageText.SetText(damage.ToString(), _damageTextPool); // ������ �ؽ�Ʈ ����
    }
}
