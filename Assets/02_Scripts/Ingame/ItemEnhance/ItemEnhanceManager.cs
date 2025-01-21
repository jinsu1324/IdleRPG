using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 강화 관리
/// </summary>
public class ItemEnhanceManager
{
    public static event Action<Item> OnItemEnhance;   // 아이템 강화할때 이벤트

    /// <summary>
    /// 아이템 강화
    /// </summary>
    public static void Enhance(Item item)
    {
        ItemDataSO itemDataSO = ItemManager.GetItemDataSO(item.ID);
        int enhanceCount = itemDataSO.GetEnhanceCount(item.Level);

        item.ReduceCount(enhanceCount);
        item.LevelUp();
        
        OnItemEnhance?.Invoke(item);

        // 장비이고 장착했을때만 플레이어 스탯에 적용
        if (itemDataSO is GearDataSO gearDataSO)
        {
            if (EquipGearManager.IsEquipped(item))
            {
                PlayerStatUpdateArgs args = new PlayerStatUpdateArgs()
                {
                    DetailStatDict = gearDataSO.GetGearStats(item.Level),
                    Source = item
                };

                PlayerStats.UpdateStatModifier(args);
            }
        }
    }
}
