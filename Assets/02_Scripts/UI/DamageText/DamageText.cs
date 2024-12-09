using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text; // �ؽ�Ʈ
    private ObjectPool<DamageText> _pool;           // ��ȯ�� Ǯ

    /// <summary>
    /// �ؽ�Ʈ ����
    /// </summary>
    public void SetText(string text, ObjectPool<DamageText> pool)
    {
        _text.text = text;
        _pool = pool;

        Invoke("ReturnPool", 0.5f);
    }

    /// <summary>
    /// Ǯ�� ��ȯ
    /// </summary>
    private void ReturnPool()
    {
        _pool.ReturnObject(this);
    }
}
