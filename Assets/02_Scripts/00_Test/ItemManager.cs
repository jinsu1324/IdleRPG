using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonBase<ItemManager>
{
    [SerializeField] private List<ItemDataSO> _itemDataSOList;  // 아이템 데이터들 리스트
    private static Dictionary<string, ItemDataSO> _itemDataSODict; // 아이템데이터 스크립터블 딕셔너리

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Set_ItemDataSODict();
    }

    /// <summary>
    /// 아이템 데이터 딕셔너리 셋팅
    /// </summary>
    private void Set_ItemDataSODict()
    {
        _itemDataSODict = new Dictionary<string, ItemDataSO>();

        foreach (ItemDataSO itemDataSO in _itemDataSOList)
        {
            if (_itemDataSODict.ContainsKey(itemDataSO.ID) == false)
                _itemDataSODict[itemDataSO.ID] = itemDataSO;
        }
    }

    /// <summary>
    /// 특정 아이템타입에 맞는 모든 ItemDataSO 가져오기
    /// </summary>
    public static List<ItemDataSO> GetItemDataSOList_ByType(ItemType itemType)
    {
        List<ItemDataSO> result = new List<ItemDataSO>();

        foreach (ItemDataSO itemDataSO in _itemDataSODict.Values)
        {
            if (itemDataSO.ItemType == itemType.ToString())
                result.Add(itemDataSO);
        }

        return result;
    }

    /// <summary>
    /// ID 에맞는 ItemDataSO 가져오기
    /// </summary>
    public static ItemDataSO GetItemDataSO(string id)
    {
        if (_itemDataSODict.TryGetValue(id, out ItemDataSO itemDataSO))
            return itemDataSO;

        Debug.Log($"{id} 에 맞는 ItemDataSO를 찾을 수 없습니다.");
        return null;
    }

    /// <summary>
    /// 해당 아이템이 강화가능한지?
    /// </summary>
    public static bool CanEnhance(Item item)
    {
        ItemDataSO itemDataSO = GetItemDataSO(item.ID);
        return item.Count >= itemDataSO.GetEnhanceCount(item.Level);
    }
}
