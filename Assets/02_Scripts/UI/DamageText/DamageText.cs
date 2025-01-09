using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : ObjectPoolObj
{
    [SerializeField] private TextMeshProUGUI _text; // 텍스트

    /// <summary>
    /// 텍스트 설정
    /// </summary>
    public void SetText(string text)
    {
        _text.text = text;

        Invoke("ReturnPool", 0.5f);
    }

    /// <summary>
    /// 풀로 반환
    /// </summary>
    private void ReturnPool()
    {
        BackTrans();
    }
}
