using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct OnEquipItemChangedArgs
{
    public ItemType ItemType;
    public GameObject Prefab;
    public string AttackAnimType;
}

/// <summary>
/// 장착한 아이템 관리
/// </summary>
public class EquipItemManager
{
    // 장착 아이템이 변경되었을 때 이벤트
    public static event Action<OnEquipItemChangedArgs, bool> OnEquipItemChanged;      

    // 장착한 아이템 딕셔너리
    private static Dictionary<ItemType, Item> _equipItemDict = new Dictionary<ItemType, Item>
    { 
        { ItemType.Weapon, null},
        { ItemType.Armor, null},
        { ItemType.Helmet, null}
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

        // 이미 슬롯에 다른 장착된 아이템이 있으면 먼저 장착해제
        if (_equipItemDict.TryGetValue(item.ItemType, out Item equippedItem))
        {
            if (equippedItem != null)
                UnEquip(equippedItem);
        }

        // 장착
        _equipItemDict[item.ItemType] = item;

        // 장착 아이템이 변경되었을 때 이벤트 실행
        OnEquipItemChangedArgs args = new OnEquipItemChangedArgs() { ItemType = item.ItemType, Prefab = item.Prefab, AttackAnimType = item.AttackAnimType };
        OnEquipItemChanged?.Invoke(args, true);

        // 플레이어 스탯에 아이템 스탯들 전부 추가
        PlayerStats.UpdateStatModifier(item.GetStatDict(), item);
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

        // 장착 아이템이 변경되었을 때 이벤트 실행
        OnEquipItemChangedArgs args = new OnEquipItemChangedArgs() { ItemType = item.ItemType, Prefab = null, AttackAnimType = AttackAnimType.Hand.ToString() };
        OnEquipItemChanged?.Invoke(args, false);

        // 플레이어 스탯에 아이템 스탯들 전부 제거
        PlayerStats.RemoveStatModifier(item.GetStatDict(), item);
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
