using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 아이템 획득 기계
/// </summary>
public class ItemDropMachine : MonoBehaviour
{
    public static event Action<List<Item>> OnDroppedItem;   // 아이템 드롭 완료되었을때 이벤트

    [Title("드롭 갯수", bold: false)]
    [SerializeField] 
    [Range(1, 10)] 
    private int _maxDropCount;

    [Title("드롭할 아이템 타입", bold: false)]
    [SerializeField] 
    private ItemType _itemType;

    /// <summary>
    /// 아이템 드롭 (버튼연결)
    /// </summary>
    public void OnClickDropItem()
    {
        List<Item> dropItemList = new List<Item>(); 

        for (int i = 0; i < _maxDropCount; i++)
        {
            Item item = CreateItem(); 
            ItemInven.AddItem(item);
            dropItemList.Add(item);
        }

        OnDroppedItem?.Invoke(dropItemList);
    }

    /// <summary>
    /// 아이템 생성
    /// </summary>
    private Item CreateItem()
    {
        // 해당 아이템타입의 모든 데이터 스크립터블 오브젝트들 중에서 1개만 고르기
        List<ItemDataSO> itemDataSOList = ItemDataManager.GetItemDataSOList_ByType(_itemType);
        ItemDataSO itemDataSO = itemDataSOList[RandomIndex(itemDataSOList.Count)];

        Item item = new Item(itemDataSO.ID, itemDataSO.ItemType, 1, 1);
        return item;
    }

    /// <summary>
    /// 0-maxCount 사이 랜덤한 숫자 반환
    /// </summary>
    private int RandomIndex(int maxCount)
    {
        int index = Random.Range(0, maxCount);
        return index;
    }
}
