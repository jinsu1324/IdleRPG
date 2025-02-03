using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToastCommon : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    /// <summary>
    /// 보여주기
    /// </summary>
    public void Show(string text)
    {
        _text.text = text;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 감추기
    /// </summary>
    public void Hide()
    {
        _text.text = null;
        gameObject.SetActive(false);
    }
}
