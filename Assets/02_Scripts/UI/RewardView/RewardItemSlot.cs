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
        //_itemIcon.sprite = item.Icon;
        //_gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(item.Grade);

        //gameObject.SetActive(true);
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
