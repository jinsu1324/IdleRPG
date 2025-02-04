using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDropButton : MonoBehaviour
{
    [SerializeField] private Button _dropButton;        // ��� ��ư
    [SerializeField] private GameObject _dimd;          // ����
    [SerializeField] private TextMeshProUGUI _costText; // ��� �ؽ�Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemDropMachine.OnDimdUpdate += UpdateDimd; // ���� ������Ʈ
    }

    /// <summary>
    /// OnDiable
    /// </summary>
    private void OnDisable()
    {
        ItemDropMachine.OnDimdUpdate -= UpdateDimd;
    }

    /// <summary>
    /// ���� ������Ʈ
    /// </summary>
    public void UpdateDimd(bool canBuy)
    {
        _dimd.SetActive(!canBuy);

        if (canBuy)
            _costText.color = Color.white;
        else
            _costText.color = Color.red;
    }

    /// <summary>
    /// ��ư�� �����۵�� �Լ� ������ ����
    /// </summary>
    public void ButtonAddListener(UnityEngine.Events.UnityAction dropItem)
    {
        _dropButton.onClick.AddListener(dropItem);
    }

    /// <summary>
    /// ��ư�� ������ ����
    /// </summary>
    public void ButtonRemoveListener()
    {
        _dropButton.onClick.RemoveAllListeners();
    }
}
