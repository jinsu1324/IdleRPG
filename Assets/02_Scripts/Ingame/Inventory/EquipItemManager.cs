using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipItemManager
{
    // 장착한 아이템 딕셔너리
    private static Dictionary<ItemType, Item> _equipItemDict = new Dictionary<ItemType, Item>
    { 
        { ItemType.Weapon, null},
        { ItemType.Armor, null}
    };

    /// <summary>
    /// 장착
    /// </summary>
    public static void Equip(Item item)
    {
        // 이미 장착된 아이템이면 그냥 리턴
        if (IsEquipped(item))
        {
            Debug.Log("이미 장착된 아이템입니다!");
            return;
        }

        // 장착
        _equipItemDict[item.ItemType] = item;
        Debug.Log($"{item.ItemType} 에 {item.Name} 장착완료!");


        // 플레이어 스탯에 아이템 스탯들 전부 추가
        foreach (var kvp in item.GetStatDict())
            PlayerStats.Instance.UpdateModifier(kvp.Key, kvp.Value, item);


        // 스탯 보여주는 UI 업데이트
        PlayerStats.Instance.AllStatUIUpdate();
    }

    /// <summary>
    /// 장착 해제
    /// </summary>
    public static void UnEquip(Item item)
    {
        // 장착된 아이템이 없으면 그냥 리턴
        if (IsEquipped(item) == false)
        {
            Debug.Log("장착된 아이템이 없습니다.");
            return;
        }

        // 장착해제
        _equipItemDict[item.ItemType] = null;
        Debug.Log($"{item.ItemType} 에서 {item.Name} 장착해제!");

        // 플레이어 스탯에 아이템 스탯들 전부 제거
        foreach (var kvp in item.GetStatDict())
            PlayerStats.Instance.RemoveModifier(kvp.Key, item);

        // 스탯 보여주는 UI 업데이트
        PlayerStats.Instance.AllStatUIUpdate();
    }

    /// <summary>
    /// 장착한 아이템인지?
    /// </summary>
    public static bool IsEquipped(Item item)
    {
        return _equipItemDict.ContainsKey(item.ItemType) && _equipItemDict[item.ItemType] == item;
    }

    /// <summary>
    /// 장착한 아이템 가져오기
    /// </summary>
    public static Item GetEquipItem(ItemType itemType)
    {
        if (_equipItemDict.TryGetValue(itemType, out Item equipItem))
        {
            return equipItem;
        }
        else
        {
            Debug.Log($"{itemType} 에 장착한 아이템이 없습니다.");
            return null;
        }
    }
}
