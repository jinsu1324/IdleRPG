using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 슬롯들을 관리해줄 매니저
/// </summary>
public class ItemSlotManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemSlot> _ItemSlotList;                   // 아이템 슬롯 리스트
    private ItemSlot _selectItemSlot;                       // 선택된 아이템 슬롯

    [SerializeField]
    private SelectItemInfoPanel _selectItemInfoPanel;       // 선택된 아이템 정보 패널

    /// <summary>
    /// 아이템 슬롯 리스트 가져오기
    /// </summary>
    public List<ItemSlot> GetItemSlotList()
    {
        return _ItemSlotList;
    }

    /// <summary>
    /// 선택된 슬롯 하이라이팅
    /// </summary>
    public void HighlightingSelectdSlot(ItemSlot newSelectedSlot)
    {
        // 이미 선택된 슬롯을 다시 클릭했을 경우 무시
        if (_selectItemSlot == newSelectedSlot)
            return;

        // 이전에 선택된 슬롯이 있다면, 그 슬롯의 하이라이트 OFF
        if (_selectItemSlot != null)
            _selectItemSlot.SelectFrameOFF();

        // 새 슬롯으로 교체 후 하이라이트 ON
        _selectItemSlot = newSelectedSlot;
        _selectItemSlot.SelectFrameON();

        // 선택된 아이템 정보 패널 켜기
        SelectItemInfoPanel_ON();
    }



    /// <summary>
    /// 선택된 아이템 정보 패널 켜기
    /// </summary>
    public void SelectItemInfoPanel_ON()
    {
        _selectItemInfoPanel.OpenAndInit(_selectItemSlot);
    }

}
