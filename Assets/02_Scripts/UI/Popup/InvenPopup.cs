using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenPopup : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> _ItemSlotList;              // 아이템 슬롯 리스트
    [SerializeField] private SelectItemInfo _selectItemInfo;            // 선택된 아이템 정보 UI
    [SerializeField] private Button _exitButton;                        // 나가기 버튼
    private ItemSlot _selectItemSlot;                                   // 선택된 아이템 슬롯

    /// <summary>
    /// 팝업 켜기
    /// </summary>
    public void Show(ItemType itemType)
    {
        if (_selectItemSlot)
        {
            Debug.Log("선택된 아이템이 있어요!");
        }
        else
        {
            Debug.Log("선택된 아이템이 없!!!!!!!!어요!");

        }


        ItemSlot.OnSlotClickedAction += TurnON_SelectSlotHighlight; // 슬롯이 클릭되었을 때, 선택된 슬롯 하이라이트 켜기
        ItemSlot.OnSlotClickedAction += Show_SelectItemInfo; // 슬롯이 클릭되었을 때, 선택된 아이템 정보 보여주기
        
        Init_ItemSlotList(itemType);    // 아이템 슬롯들 초기화
        
        _exitButton.onClick.AddListener(Hide);  // 나가기 버튼 누르면 팝업끄기

        gameObject.SetActive(true);
    }


    /// <summary>
    /// 아이템 슬롯들 초기화
    /// </summary>
    private void Init_ItemSlotList(ItemType itemType)
    {
        // 이미 선택된 슬롯이 있으면
        if (_selectItemSlot != null)
        {
            // 하이라이트 끄기
            TurnOFF_SelectSlotHighlight();

            // 선택한 아이템 정보UI 끄기
            Hide_SelectItemInfo();

            Debug.Log("그래서 없앴어요!");

            // 선택한 슬롯 null으로 초기화
            _selectItemSlot = null;
        }

        // 아이템타입에 맞는 아이템 인벤토리 가져옴
        List<Item> itemInven = ItemInven.GetItemInvenByItemType(itemType);

        // 아이템인벤이 없으면, 다 빈슬롯으로 만들고 리턴
        if (itemInven == null)
        {
            for (int i = 0; i < _ItemSlotList.Count; i++)
            {
                _ItemSlotList[i].OFF_ItemSlotInfo();   // 나머지는 빈슬롯으로 두기
            }

            return;
        }


        for (int i = 0; i < _ItemSlotList.Count; i++)
        {
            if (i < itemInven.Count)
            {
                _ItemSlotList[i].Init_ItemSlotInfo(itemInven[i]); // 아이템 가진 갯수만큼 아이템슬롯 초기화
            }
            else
            {
                _ItemSlotList[i].OFF_ItemSlotInfo();   // 나머지는 끄기
            }
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
    /// 선택된 슬롯 하이라이트 끄기
    /// </summary>
    public void TurnOFF_SelectSlotHighlight()
    {
        _selectItemSlot.Highlight_OFF();
    }

    /// <summary>
    /// 선택된 아이템 정보 켜기
    /// </summary>
    private void Show_SelectItemInfo(ItemSlot newSelectSlot)
    {
        _selectItemInfo.Init(newSelectSlot);
    }

    /// <summary>
    /// 선택된 아이템 정보 끄기
    /// </summary>
    private void Hide_SelectItemInfo()
    {
        _selectItemInfo.HideUI();
    }

    /// <summary>
    /// 팝업 끄기
    /// </summary>
    public void Hide()
    {
        ItemSlot.OnSlotClickedAction -= TurnON_SelectSlotHighlight;
        ItemSlot.OnSlotClickedAction -= Show_SelectItemInfo;

        gameObject.SetActive(false);
    }
}
