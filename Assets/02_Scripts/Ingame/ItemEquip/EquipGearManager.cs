using System;
using System.Collections;
using System.Collections.Generic;
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
/// 장착한 장비 관리
/// </summary>
public class EquipGearManager
{
    // 장착 장비가 변경되었을 때 이벤트
    public static event Action<OnEquipGearChangedArgs> OnEquipGearChanged;

    // 장착한 장비 딕셔너리
    private static Dictionary<ItemType, Gear> _equipGearDict = new Dictionary<ItemType, Gear>
    { 
        { ItemType.Weapon, null},
        { ItemType.Armor, null},
        { ItemType.Helmet, null}
    };

    /// <summary>
    /// 장비 장착
    /// </summary>
    public static void EquipGear(Gear gear)
    {
        // 이미 장착된 아이템이면 그냥 리턴
        if (IsEquippedGear(gear))
            return;

        // 이미 슬롯에 다른 장착된 아이템이 있으면 먼저 장착해제
        if (_equipGearDict.TryGetValue(gear.ItemType, out Gear equippedItem))
        {
            if (equippedItem != null)
                UnEquipGear(equippedItem);
        }

        // 장착
        _equipGearDict[gear.ItemType] = gear;

        // 장착한 장비가 변경되었을 때 이벤트 실행
        OnEquipGearChangedArgs args = new OnEquipGearChangedArgs() 
        { 
            ItemType = gear.ItemType, 
            Prefab = gear.Prefab, 
            AttackAnimType = gear.AttackAnimType, 
            IsTryEquip = true 
        };
        OnEquipGearChanged?.Invoke(args);

        // 플레이어 스탯에 아이템 스탯들 전부 추가
        PlayerStats.UpdateStatModifier(gear.GetAbilityDict(), gear);

    }

    /// <summary>
    /// 장비 장착 해제
    /// </summary>
    public static void UnEquipGear(Gear gear)
    {
        // 장착된 장비가 없으면 그냥 리턴
        if (IsEquippedGear(gear) == false)
        {
            Debug.Log("장착된 장비가 없습니다.");
            return;
        }

        // 장착해제
        _equipGearDict[gear.ItemType] = null;
        
        // 장착한 장비가 변경되었을 때 이벤트 실행
        OnEquipGearChangedArgs args = new OnEquipGearChangedArgs() 
        { 
            ItemType = gear.ItemType, 
            Prefab = null, 
            AttackAnimType = AttackAnimType.Hand.ToString(), 
            IsTryEquip = false 
        };
        OnEquipGearChanged?.Invoke(args);

        // 플레이어 스탯에 아이템 스탯들 전부 제거
        PlayerStats.RemoveStatModifier(gear.GetAbilityDict(), gear);
    }

    /// <summary>
    /// 장착한 장비인지?
    /// </summary>
    public static bool IsEquippedGear(Gear gear)
    {
        return _equipGearDict.ContainsKey(gear.ItemType) && _equipGearDict[gear.ItemType] == gear;
    }

    /// <summary>
    /// 장착한 장비 가져오기
    /// </summary>
    public static Gear GetEquipGear(ItemType itemType)
    {
        if (_equipGearDict.TryGetValue(itemType, out Gear equipItem))
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
