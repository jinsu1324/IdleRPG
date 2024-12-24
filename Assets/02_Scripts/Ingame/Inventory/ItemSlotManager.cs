using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotManager : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> _ItemSlotList;              // 아이템 슬롯 리스트
    private ItemSlot _selectItemSlot;                                   // 선택된 아이템 슬롯

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        ItemSlot.OnSlotSelected += TurnON_SelectSlotHighlight; // 슬롯이 선택되었을 때, 선택된 슬롯 하이라이트 켜기
    }

    /// <summary>
    /// 아이템 슬롯들 초기화
    /// </summary>
    public void Init(ItemType itemType)
    {
        TryClear_SelectItemSlot();

        List<Item> itemInven = ItemInven.GetItemInvenByItemType(itemType); // 아이템타입에 맞는 아이템 인벤토리 가져옴

        for (int i = 0; i < _ItemSlotList.Count; i++)
        {
            if (itemInven == null)  // 아이템 인벤 없으면 슬롯 전부 다 비우기만
            {
                _ItemSlotList[i].Clear();
                continue;
            }

            if (i < itemInven.Count)
                _ItemSlotList[i].Init(itemInven[i]);
            else
                _ItemSlotList[i].Clear();
        }
    }

    /// <summary>
    /// 선택된 슬롯이 있으면 슬롯 비우기
    /// </summary>
    public void TryClear_SelectItemSlot()
    {
        if (_selectItemSlot != null)
        {
            _selectItemSlot.Highlight_OFF();
            _selectItemSlot = null;
        }
    }

    /// <summary>
    /// 선택된 슬롯 하이라이트 켜기
    /// </summary>
    public void TurnON_SelectSlotHighlight(ItemSlot newSelectSlot)
    {
        // 이미 선택된 슬롯을 다시 클릭했을 경우 무시
        if (_selectItemSlot == newSelectSlot)
            return;

        // 이전에 선택된 슬롯이 있다면, 그 슬롯의 하이라이트 OFF
        if (_selectItemSlot != null)
            _selectItemSlot.Highlight_OFF();

        // 새 슬롯으로 교체 후 하이라이트 ON
        _selectItemSlot = newSelectSlot;
        _selectItemSlot.Highlight_ON();
    }

    /// <summary>
    /// OnDestroy
    /// </summary>
    private void OnDestroy()
    {
        ItemSlot.OnSlotSelected -= TurnON_SelectSlotHighlight;
    }
}
