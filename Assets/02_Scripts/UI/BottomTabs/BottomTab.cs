using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomTab : MonoBehaviour
{
    public static event Action<BottomTab> OnOpenButtonClicked;   // ¿­±â¹öÆ° ´­·¶À» ¶§ ÀÌº¥Æ®
    public static event Action OnCloseButtonClicked;             // ´Ý±â¹öÆ° ´­·¶À» ¶§ ÀÌº¥Æ®

    [SerializeField] private GameObject _tabPopup;  // ÀÌ ÅÇ ´©¸£¸é ¿­¸± ÆË¾÷
    [SerializeField] private Button _openButton;    // ¿­±â ¹öÆ°
    [SerializeField] private Button _closeButton;   // ´Ý±â ¹öÆ°

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _openButton.onClick.AddListener(OpenTabPopup);
        _closeButton.onClick.AddListener(CloseTabPopup);
    }

    /// <summary>
    /// ÆË¾÷ ¿­±â
    /// </summary>
    public void OpenTabPopup()
    {
        _tabPopup.SetActive(true);  // ÆË¾÷ ¿­±â
        _openButton.gameObject.SetActive(false);  // ¿­±â ¹öÆ°Àº ²û
        _closeButton.gameObject.SetActive(true);  // ´Ý±â ¹öÆ°Àº ÄÔ

        OnOpenButtonClicked?.Invoke(this);
    }

    /// <summary>
    /// ÆË¾÷ ´Ý±â
    /// </summary>
    public void CloseTabPopup()
    {
        _tabPopup.SetActive(false); // ÆË¾÷ ´Ý±â
        _openButton.gameObject.SetActive(true);   // ¿­±â ¹öÆ°Àº ÄÔ
        _closeButton.gameObject.SetActive(false); // ´Ý±â ¹öÆ°Àº ²û
    }
}
