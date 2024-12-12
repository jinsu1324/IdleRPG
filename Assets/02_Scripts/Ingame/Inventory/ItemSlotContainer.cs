using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 슬롯들을 보관하고 있을 컨테이너
/// </summary>
public class ItemSlotContainer : SingletonBase<ItemSlotContainer>
{
    [SerializeField]
    private List<ItemSlot> _ItemSlotList;                     // 아이템 슬롯 리스트
    private ItemSlot _selectedItemSlot;                       // 선택된 아이템 슬롯



    [SerializeField]
    private SelectItemInfoPanel _selectedItemInfoPanel;                 // 선택된 아이템 정보 패널





    public List<ItemSlot> GetItemSlotList() => _ItemSlotList;    // 아이템 슬롯 리스트  가져오기



    /// <summary>
    /// 선택된 슬롯 하이라이팅
    /// </summary>
    public void HighlightingSelectdSlot(ItemSlot newSelectedSlot)
    {
        // 이미 선택된 슬롯을 다시 클릭했을 경우 무시
        if (_selectedItemSlot == newSelectedSlot)
            return;

        // 이전에 선택된 슬롯이 있다면, 그 슬롯의 하이라이트 OFF
        if (_selectedItemSlot != null)
            _selectedItemSlot.SelectFrameOFF();

        // 새 슬롯으로 교체 후 하이라이트 ON
        _selectedItemSlot = newSelectedSlot;
        _selectedItemSlot.SelectFrameON();

        // 선택된 아이템 정보 패널 켜기
        SelectedItemInfoPanel_ON();
    }



    /// <summary>
    /// 선택된 아이템 정보 패널 켜기
    /// </summary>
    public void SelectedItemInfoPanel_ON()
    {
        _selectedItemInfoPanel.OpenAndInit(_selectedItemSlot);
    }

}
