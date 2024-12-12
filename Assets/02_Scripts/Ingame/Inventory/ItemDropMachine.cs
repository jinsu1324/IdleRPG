using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 아이템 획득 기계
/// </summary>
public class ItemDropMachine : MonoBehaviour
{
    // 실제 에어컨이나 공기청정기 같은 기계를 만든다고 생각해보자.
    // 어디에 추가될지는 얘 자체가 가지고 있을 필요가 없어.
    // 연결해주는 통로는 필요해 어디에 추가할지는
    // 얘는 드롭머신일 뿐이지. 그 저장소가 아니란말이야
    // 저장소가 어딘지 연결할건 필요해
    // 그 저장소가 추상적인 곳이 아니라면, 어디서라도 반드시 참조는 해줘야해


    //[Title("획득한 아이템을 저장할 슬롯 컨테이너", Bold = false)]
    //[SerializeField] private ItemSlotContainer _itemSlotContainer;    // 아이템슬롯들이 들어있는 컨테이너


    /// <summary>
    /// 랜덤 아이템 획득 버튼
    /// </summary>
    public void AcquireRandomItemButton()
    {
        Item item = RandomPickItem(); // 아이템 랜덤으로 가져오기
        int dropCount = RandomPickDropCount(); // 획득 수 랜덤으로 가져오기
        
        for (int i = 0; i < dropCount; i++) // 획득 수 만큼 반복 획득
            AcquireItem(item);
    }

    /// <summary>
    /// 아이템 획득
    /// </summary>
    public bool AcquireItem(Item item)
    {
        
        // 가지고 있는 아이템인지 체크
        Item existItem = HaveItemContainer.HaveItemCheck(item); 

        // 이미 있으면, 그 아이템의 갯수를 추가해주자
        if (existItem != null)
        {
            // 갯수 추가
            existItem.AddCount();

            // 해당 아이템의 슬롯을 찾아 UI 갱신
            ItemSlot itemSlot = ItemSlotFinder.FindSlot_ContainItem(existItem, ItemSlotContainer.Instance.GetItemSlotList());
            itemSlot.UpdateItemInfoUI();

            return true;
        }

        // 없으면, 새로 아이템을 획득해주자
        else
        {
            ItemSlot emptySlot = ItemSlotFinder.FindSlot_Empty(ItemSlotContainer.Instance.GetItemSlotList());

            if (emptySlot != null)
            {
                emptySlot.AddItem(item);
                HaveItemContainer.AddHaveItemList(item);

                return true;
            }
        }

        Debug.Log("아이템을 추가하지 못했습니다.");
        return false; // 그 어떤것도 못했으면 실패
    }

    /// <summary>
    /// 장비 랜덤으로 픽
    /// </summary>
    private Item RandomPickItem()
    {
        ItemDataSO itemDataSO = DataManager.Instance.GetItemDataSOByID(GetRandomItemID().ToString()); // 랜덤한 장비데이터 가져오기
        Item equipment = new Item(itemDataSO, 1); // 랜덤 장비 하나 생성

        return equipment;
    }

    /// <summary>
    /// 획득 숫자 랜덤으로 픽
    /// </summary>
    private int RandomPickDropCount()
    {
        int dropCount = Random.Range(1, 5); // 랜덤 획득 숫자 1, 2, 3, 4

        return dropCount;
    }

    /// <summary>
    /// 랜덤 EquipmentID 반환
    /// </summary>
    public ItemID GetRandomItemID()
    {
        // Enum 값들을 배열로 가져옴
        Array values = Enum.GetValues(typeof(ItemID));

        // 랜덤 인덱스 선택
        int randomIndex = Random.Range(0, values.Length);

        // 랜덤 EquipmentID 반환
        return (ItemID)values.GetValue(randomIndex);
    }
}
