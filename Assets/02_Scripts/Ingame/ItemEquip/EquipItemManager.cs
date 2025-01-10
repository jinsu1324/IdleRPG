using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public struct OnEquipGearChangedArgs
{
    public ItemType ItemType;
    public GameObject Prefab;
    public string AttackAnimType;
    public bool IsTryEquip;
}

/// <summary>
/// 장착한 아이템 관리
/// </summary>
public class EquipItemManager
{
    public static event Action<OnEquipGearChangedArgs> OnEquipGearChanged;  // 장착 아이템이 변경되었을 때 이벤트
    
    private static Dictionary<ItemType, Item[]> _equipItemDict;             // 장착한 아이템 딕셔너리

    private static int _weaponMaxCount = 1;                                 // 무기 최대 장착가능 갯수
    private static int _armorMaxCount = 1;                                  // 갑옷 최대 장착가능 갯수
    private static int _HelmetMaxCount = 1;                                 // 헬멧 최대 장착가능 갯수
    private static int _SkillMaxCount = 3;                                  // 스킬 최대 장착가능 갯수

    /// <summary>
    /// 정적 생성자 (클래스가 처음 참조될 때 한 번만 호출)
    /// </summary>
    static EquipItemManager()
    {
        _equipItemDict = new Dictionary<ItemType, Item[]>
        {
            { ItemType.Weapon, new Item[_weaponMaxCount]},
            { ItemType.Armor, new Item[_armorMaxCount]},
            { ItemType.Helmet, new Item[_HelmetMaxCount]},
            { ItemType.Skill, new Item[_SkillMaxCount]}
        };
    }

    /// <summary>
    /// 장비 장착
    /// </summary>
    public static void Equip(Item item, int slotIndex)
    {
        // 현재 아이템을 장착중이면 그냥 리턴
        if (IsEquipped(item))
            return;

        // 다른 아이템이 장착되어 있다면, 먼저 장착 해제
        Item oldItem = GetEquippedItem(item.ItemType, slotIndex);
        if (oldItem != null)
            UnEquip(oldItem);

        // 새로운 스킬 장착
        _equipItemDict[item.ItemType][slotIndex] = item;

        Debug.Log($"장착된 아이템은! {GetEquippedItem(item.ItemType, slotIndex).Name}");
    }

    /// <summary>
    /// 장비 장착 해제
    /// </summary>
    public static void UnEquip(Item item)
    {
        // 해당 아이템이 어느 인덱스에 장착되어있는지 찾음
        int equippedIndex = FindEquippedSlotIndex(item);

        // 인덱스를 찾을수 없었다면(-1) 그냥리턴
        if (IsIndexFound(equippedIndex) == false)
        {
            Debug.Log($"{item.Name} 아이템이 장착슬롯 안에 없어서 장착해제가 불가능합니다.");
            return;
        }

        // 장착해제
        Debug.Log($"해제될 아이템은! {_equipItemDict[item.ItemType][equippedIndex].Name}");
        _equipItemDict[item.ItemType][equippedIndex] = null;


    }

    /// <summary>
    /// 해당 슬롯에 장착되어있는 아이템 가져오기
    /// </summary>
    public static Item GetEquippedItem(ItemType itemType, int slotIndex)
    {
        return _equipItemDict[itemType][slotIndex];
    }

    /// <summary>
    /// 장착한 아이템인지?
    /// </summary>
    public static bool IsEquipped(Item item)
    {
        return _equipItemDict[item.ItemType].Contains(item);
    }

    /// <summary>
    /// 해당 아이템이 어느 슬롯에 장착되어있는지 찾기
    /// </summary>
    private static int FindEquippedSlotIndex(Item item)
    {
        return Array.FindIndex(_equipItemDict[item.ItemType], x => x == item);
    }

    /// <summary>
    /// 인덱스를 찾을 수 있었는지?
    /// </summary>
    private static bool IsIndexFound(int equippedIndex)
    {
        if (equippedIndex >= 0)
            return true;
        else
            return false;
    }





















    /// <summary>
    /// 최대로 착용했는지?
    /// </summary>
    private static bool IsEquipMax(ItemType itemType)
    {
        return GetEmptySlotIndex(itemType) == -1;
    }

    /// <summary>
    /// 비어있는 슬롯 인덱스 가져오기
    /// </summary>
    private static int GetEmptySlotIndex(ItemType itemType)
    {
        return Array.FindIndex(_equipItemDict[itemType], s => s == null);
    }
}
