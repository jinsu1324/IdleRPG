using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text; // 텍스트
    private ObjectPool<DamageText> _pool;           // 반환할 풀

    /// <summary>
    /// 텍스트 설정
    /// </summary>
    public void SetText(string text, ObjectPool<DamageText> pool)
    {
        _text.text = text;
        _pool = pool;

        Invoke("ReturnPool", 0.5f);
    }

    /// <summary>
    /// 풀로 반환
    /// </summary>
    private void ReturnPool()
    {
        _pool.ReturnObject(this);
    }
}
