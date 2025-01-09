using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : ObjectPoolObj
{
    [SerializeField] private TextMeshProUGUI _text; // �ؽ�Ʈ

    /// <summary>
    /// �ؽ�Ʈ ����
    /// </summary>
    public void SetText(string text)
    {
        _text.text = text;

        Invoke("ReturnPool", 0.5f);
    }

    /// <summary>
    /// Ǯ�� ��ȯ
    /// </summary>
    private void ReturnPool()
    {
        BackTrans();
    }
}
