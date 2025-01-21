using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 강화 관리
/// </summary>
public class ItemEnhanceManager
{
    public static event Action<Item> OnItemEnhance;   // 아이템 강화할때 이벤트 (장비 + 스킬 포함)
    public static event Action<Item> OnSkillEnhance;  // 스킬 강화할때 이벤트 (스킬만 따로 플러스로)  

    /// <summary>
    /// 아이템 강화
    /// </summary>
    public static void Enhance(Item item)
    {
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(item.ID);
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

        if (item.ItemType == ItemType.Skill)
        {
            OnSkillEnhance?.Invoke(item);
        }
    }

    /// <summary>
    /// 해당 아이템이 강화가능한지?
    /// </summary>
    public static bool CanEnhance(Item item)
    {
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(item.ID);
        return item.Count >= itemDataSO.GetEnhanceCount(item.Level);
    }
}
