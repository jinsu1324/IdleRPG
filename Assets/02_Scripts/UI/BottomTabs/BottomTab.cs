using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BottomTabType
{
    Gear,
    Shop,
    Skill
}

[RequireComponent(typeof(ReddotComponent))]
public class BottomTab : MonoBehaviour
{
    public static event Action<BottomTab> OnOpenButtonClicked;   // �����ư ������ �� �̺�Ʈ
    public static event Action OnCloseButtonClicked;             // �ݱ��ư ������ �� �̺�Ʈ

    [SerializeField] private BottomTabType _bottomTabType;       // �ϴ��� Ÿ��
    [SerializeField] private BottomTabPopupBase _tabPopup;       // �� �� ������ ���� �˾�
    [SerializeField] private Button _openButton;                 // ���� ��ư
    [SerializeField] private Button _closeButton;                // �ݱ� ��ư

    [Title("����� ������Ʈ", bold: false)]
    [SerializeField] private ReddotComponent _reddotComponent;  // ����� ������Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemInven.OnAddItem += Update_ReddotComponent; // ������ �߰��Ǿ��� �� -> �ϴ��� ����� ������Ʈ ������Ʈ
        ItemEnhanceManager.OnItemEnhance += Update_ReddotComponent; // ������ ��ȭ�Ҷ� -> �ϴ��� ����� ������Ʈ ������Ʈ

        _openButton.onClick.AddListener(OpenTabPopup);
        _closeButton.onClick.AddListener(CloseTabPopup);

        Update_ReddotComponent(); // ����� ������Ʈ ������Ʈ
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemInven.OnAddItem -= Update_ReddotComponent;
        ItemEnhanceManager.OnItemEnhance -= Update_ReddotComponent;

        _openButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// �˾� ����
    /// </summary>
    public void OpenTabPopup()
    {
        _tabPopup.Show();  // �˾� ����
        _openButton.gameObject.SetActive(false);  // ���� ��ư�� ��
        _closeButton.gameObject.SetActive(true);  // �ݱ� ��ư�� ��

        OnOpenButtonClicked?.Invoke(this);
    }

    /// <summary>
    /// �˾� �ݱ�
    /// </summary>
    public void CloseTabPopup()
    {
        _tabPopup.Hide(); // �˾� �ݱ�
        _openButton.gameObject.SetActive(true);   // ���� ��ư�� ��
        _closeButton.gameObject.SetActive(false); // �ݱ� ��ư�� ��

        OnCloseButtonClicked?.Invoke();
    }

    /// <summary>
    /// ����� ������Ʈ ������Ʈ
    /// </summary>
    public void Update_ReddotComponent(Item item = null)
    {
        switch (_bottomTabType)
        {
            case BottomTabType.Gear: 
                _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableGear()); // ��ȭ������ ��� �ִ��� Ȯ��
                break;
            case BottomTabType.Skill:
                _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableSkill()); // ��ȭ������ ��ų �ִ��� Ȯ��
                break;
            case BottomTabType.Shop:
                _reddotComponent.Hide();
                break;
        }
    }
}
