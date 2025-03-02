using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemSlot : MonoBehaviour
{
    [SerializeField] private Image _itemIcon;       // 아이템 아이콘
    [SerializeField] private Image _gradeFrame;     // 아이템 등급 프레임

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(Item item)
    {
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(item.ID);

        _itemIcon.sprite = itemDataSO.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(itemDataSO.Grade);

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 끄기
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
