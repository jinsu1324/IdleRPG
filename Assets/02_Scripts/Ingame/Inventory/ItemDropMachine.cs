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
    [SerializeField] private ItemType _itemType;    // 획득할 아이템 타입

    /// <summary>
    /// 랜덤 아이템 획득 버튼
    /// </summary>
    public void AcquireRandomItemButton()
    {
        Item item = RandomPickItem(); // 아이템 랜덤으로 가져오기
        int dropCount = RandomPickDropCount(5); // 획득 수 랜덤으로 가져오기
        
        for (int i = 0; i < dropCount; i++) // 획득 수 만큼 반복 획득
            AcquireItem(item);

        Debug.Log("------------------------------");
        ItemInven.CheckCurrentItemInven(item);  // 일단 디버그로 체크용
    }

    /// <summary>
    /// 아이템 획득
    /// </summary>
    private void AcquireItem(Item item)
    {
        // 가지고 있는 아이템인지 체크
        Item existItem = ItemInven.HasItemInInven(item); 

        // 이미 있으면, 그 아이템의 갯수만 추가
        if (existItem != null)
        {
            existItem.AddCount();
            return;
        }
        
        // 없으면, 새로 아이템을 추가
        ItemInven.AddItem(item);
        return;
    }

    /// <summary>
    /// 장비 랜덤으로 픽
    /// </summary>
    private Item RandomPickItem()
    {
        // 같은 아이템타입의 아이템데이터는 모두 가져오기
        List<ItemDataSO> typeItemDataSOList =  DataManager.Instance.GetAllItemDataSOByItemType(_itemType);  

        // 그 아이템데이터들 중에서 하나 랜덤 픽
        ItemDataSO itemDataSO = typeItemDataSOList[RandomPickItemIndex(typeItemDataSOList.Count)];

        // 랜덤픽된 데이터로 아이템 만들기
        Item item = new Item(itemDataSO, 1);

        return item;
    }

    /// <summary>
    /// 아이템 갯수 중 랜덤으로 픽
    /// </summary>
    private int RandomPickItemIndex(int maxCount)
    {
        int index = Random.Range(0, maxCount);
        return index;
    }

    /// <summary>
    /// 획득 숫자 랜덤으로 픽
    /// </summary>
    private int RandomPickDropCount(int maxCount)
    {
        int dropCount = Random.Range(1, 5 + 1);
        return dropCount;
    }
}
