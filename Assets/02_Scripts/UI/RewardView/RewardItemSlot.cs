using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemSlot : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;       // ������ ������
    [SerializeField] private Image _gradeFrame;     // ������ ��� ������

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(Item item)
    {
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(item.ID);

        _itemIcon.sprite = itemDataSO.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(itemDataSO.Grade);

        gameObject.SetActive(true);
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
