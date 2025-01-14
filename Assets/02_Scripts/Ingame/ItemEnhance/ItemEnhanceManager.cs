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
        if (item is IEnhanceableItem enhanceableItem)
        {
            // 아이템 갯수 감소
            enhanceableItem.RemoveCountByEnhance();    
           
            // 아이템 레벨업
            enhanceableItem.ItemLevelUp();             

            // 아이템 강화 이벤트 노티
            OnItemEnhance?.Invoke(item);    

            // 장비일때만
            if (item is GearItem gearItem)
            {
                // 해당 장비가 장착되어 있을때만
                if (EquipGearManager.IsEquipped(gearItem))
                {
                    // 플레이어 스탯 업데이트
                    PlayerStats.UpdateStatModifier(gearItem.GetAttributeDict(), item);
                }
            }
        }
    }
}
