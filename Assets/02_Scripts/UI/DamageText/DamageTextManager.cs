using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextManager : SingletonBase<DamageTextManager>
{
    [SerializeField] private ObjectPool _damageTextPool;         // ������ �ؽ�Ʈ Ǯ
    [SerializeField] private ObjectPool _criticalDamageTextPool; // ũ��Ƽ�� ������ �ؽ�Ʈ Ǯ

    /// <summary>
    /// ������ �׽�Ʈ ǥ��
    /// </summary>
    public void ShowDamageText(float damage, Vector3 position, bool isCritical)
    {
        DamageText damageText = null;

        if (isCritical) // ũ�� ���ο� ���� �ٸ� �ؽ�Ʈ������ Ǯ���� ��������
            damageText = _criticalDamageTextPool.GetObj().GetComponent<DamageText>();
        else
            damageText = _damageTextPool.GetObj().GetComponent<DamageText>();

        damageText.transform.position = position; // ��ǥ ����
        damageText.SetText(damage.ToString()); // ������ �ؽ�Ʈ ����
    }
}
