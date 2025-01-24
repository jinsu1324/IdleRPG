using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    [SerializeField] private Button _saveButton;        // �����ư
    [SerializeField] private Button _loadButton;        // �ҷ����� ��ư
    [SerializeField] private Button _exitButton;        // ������ ��ư
    [SerializeField] private Button _dimdButton;        // ���� ������ ��ư

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _saveButton.onClick.AddListener(Save_And_HidePopup); // �����ư �̺�Ʈ �Ҵ�
        _loadButton.onClick.AddListener(Load_And_HidePopup); // �ε��ư �̺�Ʈ �Ҵ�   
        _exitButton.onClick.AddListener(Hide); // ������ ��ư �Ҵ�
        _dimdButton.onClick.AddListener(Hide); // ������ ��ư �Ҵ�
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
    /// �ʱ�ȭ (ID ���� Ȯ�� async)
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
    /// �����
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �����ư �ڵ鷯 (���� + �˾�����)
    /// </summary>
    private void Save_And_HidePopup()
    {
        SaveLoadManager.Instance.SaveAllButton();
        Hide();
    }

    /// <summary>
    /// �ε��ư �ڵ鷯 (�ε� + �˾�����)
    /// </summary>
    private void Load_And_HidePopup()
    {
        GameManager.Instance.TryLoadDataButton();
        Hide();
    }
}

