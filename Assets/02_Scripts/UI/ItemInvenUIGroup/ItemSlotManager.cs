using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 아이템 슬롯들 관리
/// </summary>
public class ItemSlotManager : MonoBehaviour
{
    [Title("아이템 슬롯 리스트", bold: false)]
    [SerializeField] private List<ItemSlot> _ItemSlotList;              // 아이템 슬롯 리스트

    [Title("슬롯 하이라이트 이미지", bold: false)]
    [SerializeField] private RectTransform _highlightImage;             // 슬롯 하이라이트 이미지

    /// <summary>
    /// 아이템 슬롯들 초기화
    /// </summary>
    public void Init(ItemType itemType)
    {
        List<IItem> itemInven = ItemInven.GetItemInvenByItemType(itemType); // 아이템타입에 맞는 아이템 인벤토리 가져옴

        for (int i = 0; i < _ItemSlotList.Count; i++)
        {
            if (itemInven == null)  // 아이템 인벤 없으면 슬롯 전부 다 비우기만
            {
                _ItemSlotList[i].Clear();
                continue;
            }

            if (i < itemInven.Count)
                _ItemSlotList[i].Init(itemInven[i], SnapHighlightImage);
            else
                _ItemSlotList[i].Clear();
        }

        HideHighlightImage(); // 하이라이트 이미지 숨기기
    }

    /// <summary>
    /// 하이라이트 이미지 스냅
    /// </summary>
    public void SnapHighlightImage(RectTransform targetRect)
    {
        _highlightImage.SetParent(targetRect);
        _highlightImage.position = targetRect.position;
        _highlightImage.gameObject.SetActive(true);
    }

    /// <summary>
    /// 하이라이트 이미지 숨기기
    /// </summary>
    public void HideHighlightImage()
    {
        _highlightImage.gameObject.SetActive(false);
    }
}
