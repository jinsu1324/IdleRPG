using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 장착한 장비 관리
/// </summary>
public class EquipGearManager// : ISavable
{
    public string Key => nameof(EquipGearManager);  // Firebase 데이터 저장용 고유 키 설정

    public static event Action<Item> OnEquipGear;   // 장비 장착할 때 이벤트
    public static event Action<Item> OnUnEquipGear; // 장비 해제할 때 이벤트

    [SaveField] private static Dictionary<ItemType, Item> _equipGearDict = new Dictionary<ItemType, Item>(); // 장착한 장비 딕셔너리

    /// <summary>
    /// 장착
    /// </summary>
    public static void Equip(Item item)
    {
        // 이미 그 아이템을 장착중이면 그냥 무시
        if (IsEquipped(item))
            return;

        // 다른 아이템이 먼저 장착되어 있다면, 장착 해제
        Item oldItem = GetEquippedItem(item.ItemType);
        if (oldItem != null)
            UnEquip(oldItem);

        // 새로운 아이템 장착
        _equipGearDict[item.ItemType] = item;
        
        // 장착 이벤트 노티
        OnEquipGear?.Invoke(item);

        // 플레이어 스탯 업데이트
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(item.ID);
        if (itemDataSO is GearDataSO gearDataSO)
        {
            PlayerStatUpdateArgs args = new PlayerStatUpdateArgs()
            {
                DetailStatDict = gearDataSO.GetGearStats(item.Level),
                Source = item
            };
            PlayerStats.UpdateStatModifier(args);
        }
    }

    /// <summary>
    /// 장착 해제
    /// </summary>
    public static void UnEquip(Item item)
    {
        // 해제
        _equipGearDict[item.ItemType] = null;

        // 해제 이벤트 노티
        OnUnEquipGear?.Invoke(item);

        // 플레이어 스탯 제거
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(item.ID);
        if (itemDataSO is GearDataSO gearDataSO)
        {
            PlayerStatUpdateArgs args = new PlayerStatUpdateArgs()
            {
                DetailStatDict = gearDataSO.GetGearStats(item.Level),
                Source = item
            };
            PlayerStats.RemoveStatModifier(args);
        }

    }

    /// <summary>
    /// 장착한 아이템인지?
    /// </summary>
    public static bool IsEquipped(Item item)
    {
        CheckAnd_SetDict(item.ItemType);
        return _equipGearDict[item.ItemType] == item;
    }

    /// <summary>
    /// 장착한 아이템 가져오기
    /// </summary>
    public static Item GetEquippedItem(ItemType itemType)
    {
        CheckAnd_SetDict(itemType);
        return _equipGearDict[itemType];
    }

    /// <summary>
    /// 딕셔너리에 키 체크해보고 없으면 딕셔너리 만들기
    /// </summary>
    private static void CheckAnd_SetDict(ItemType itemType)
    {
        if (_equipGearDict.ContainsKey(itemType) == false)
            _equipGearDict[itemType] = null;
    }
}
