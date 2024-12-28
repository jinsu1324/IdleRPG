using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BottomTabType
{
    PlayerDetail,
    Shop
}

[RequireComponent(typeof(ReddotComponent))]
public class BottomTab : MonoBehaviour
{
    public static event Action<BottomTab> OnOpenButtonClicked;   // �����ư ������ �� �̺�Ʈ
    public static event Action OnCloseButtonClicked;             // �ݱ��ư ������ �� �̺�Ʈ

    [SerializeField] private BottomTabType _bottomTabType;       // �ϴ��� Ÿ��
    [SerializeField] private TabPopupBase _tabPopup;             // �� �� ������ ���� �˾�
    [SerializeField] private Button _openButton;                 // ���� ��ư
    [SerializeField] private Button _closeButton;                // �ݱ� ��ư

    [Title("����� ������Ʈ", bold: false)]
    [SerializeField] private ReddotComponent _reddotComponent;   // ����� ������Ʈ

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _openButton.onClick.AddListener(OpenTabPopup);
        _closeButton.onClick.AddListener(CloseTabPopup);
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
    /// �ϴ��� Ÿ�� ��������
    /// </summary>
    public BottomTabType GetBottomTabType()
    {
        return _bottomTabType;
    }

    /// <summary>
    /// ����� ���� ����
    /// </summary>
    public void SetReddotCondition(Func<bool> condition)
    {
        _reddotComponent.UpdateReddot(condition);
    }
}
