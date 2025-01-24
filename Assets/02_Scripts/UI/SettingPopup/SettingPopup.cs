using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    [SerializeField] private Button _saveButton;        // 저장버튼
    [SerializeField] private Button _loadButton;        // 불러오기 버튼
    [SerializeField] private Button _exitButton;        // 나가기 버튼
    [SerializeField] private Button _dimdButton;        // 딤드 나가기 버튼

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _saveButton.onClick.AddListener(Save_And_HidePopup); // 저장버튼 이벤트 할당
        _loadButton.onClick.AddListener(Load_And_HidePopup); // 로드버튼 이벤트 할당   
        _exitButton.onClick.AddListener(Hide); // 나가기 버튼 할당
        _dimdButton.onClick.AddListener(Hide); // 나가기 버튼 할당
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _saveButton.onClick.RemoveAllListeners();
        _loadButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
        _dimdButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 초기화 (ID 여부 확인 async)
    /// </summary>
    public async Task InitAsync()
    {
        bool existUserID = await SaveLoadManager.ExistUserID();

        if (existUserID)
            _loadButton.gameObject.SetActive(true);
        else
            _loadButton.gameObject.SetActive(false);

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 숨기기
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 저장버튼 핸들러 (저장 + 팝업끄기)
    /// </summary>
    private void Save_And_HidePopup()
    {
        SaveLoadManager.Instance.SaveAllButton();
        Hide();
    }

    /// <summary>
    /// 로드버튼 핸들러 (로드 + 팝업끄기)
    /// </summary>
    private void Load_And_HidePopup()
    {
        GameManager.Instance.TryLoadDataButton();
        Hide();
    }
}

