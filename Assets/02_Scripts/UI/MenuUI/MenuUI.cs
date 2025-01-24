using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button _settingButton;         // 설정버튼
    [SerializeField] private SettingPopup _settingPopup;    // 설정 팝업

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _settingButton.onClick.AddListener(() => _ = _settingPopup.InitAsync());
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _settingButton.onClick.RemoveAllListeners();
    }
}
