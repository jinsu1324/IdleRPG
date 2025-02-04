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
    public static event Action<bool> OnDimdUpdate;  // 딤드 업데이트할때 사용할 이벤트

    [Title("드롭 갯수", bold: false)]
    [SerializeField] 
    [Range(1, 10)] 
    private int _maxDropCount;

    [Title("드롭할 아이템 타입", bold: false)]
    [SerializeField] 
    private ItemType _itemType;

    [Title("드롭 비용", bold: false)]
    [SerializeField]
    private int _dropCost;

    [Title("드롭 버튼", bold: false)]
    [SerializeField]
    private ItemDropButton _dropButton;




    // 불가시 토스트메시지




    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _dropButton.ButtonAddListener(OnClickDropItem); // 버튼에 아이템 드롭함수 연결
        _dropButton.UpdateDimd(GemManager.HasEnoughGem(_dropCost)); // 버튼 딤드 업데이트
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _dropButton.ButtonRemoveListener(); // 버튼 리스너 제거
    }

    /// <summary>
    /// 아이템 드롭 (버튼연결)
    /// </summary>
    public void OnClickDropItem()
    {
        // 젬 부족하면 그냥 리턴
        if (GemManager.HasEnoughGem(_dropCost) == false)
        {
            ToastManager.Instance.StartShow_ToastCommon("보석이 부족합니다."); // 토스트 메시지
            return;
        }

        GemManager.ReduceGem(_dropCost); // 젬 감소

        List<Item> dropItemList = new List<Item>(); 
        for (int i = 0; i < _maxDropCount; i++)
        {
            Item item = CreateItem(); 
            ItemInven.AddItem(item);
            dropItemList.Add(item);
        }

        OnDroppedItem?.Invoke(dropItemList);

        OnDimdUpdate?.Invoke(GemManager.HasEnoughGem(_dropCost)); // 버튼 딤드 업데이트
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
