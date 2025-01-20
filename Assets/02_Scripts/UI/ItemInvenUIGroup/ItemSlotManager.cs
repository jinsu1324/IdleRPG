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
        List<Item> itemInven = ItemInven.GetItemInven(itemType);
        
        for (int i = 0; i < _ItemSlotList.Count; i++)
        {
            if (itemInven == null)
            {
                _ItemSlotList[i].Clear();
                continue;
            }

            if (i < itemInven.Count)  
                _ItemSlotList[i].Init(itemInven[i], SnapHighlightImage); // 인벤속 아이템 갯수만큼만 초기화, 나머지는 끄기
            else
                _ItemSlotList[i].Clear();
        }

        HideHighlightImage();
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
