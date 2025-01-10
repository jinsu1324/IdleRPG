using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 강화 관리
/// </summary>
public class ItemEnhanceManager
{
    /// <summary>
    /// 아이템 강화
    /// </summary>
    public static void Enhance(Item item)
    {
        if (item is IEnhanceableItem enhanceableItem)
        {
            enhanceableItem.RemoveCountByEnhance();    // 아이템 갯수 감소
            enhanceableItem.ItemLevelUp();             // 아이템 레벨업

            Debug.Log($"강회된 아이템 {item.Name} - {item.Count} / {enhanceableItem.EnhanceableCount}");
        }

        //// 장비일때만
        //if (item is GearItem gearItem)
        //{
        //    // 해당 아이템이 장착되어 있을때만
        //    if (EquipItemManager.IsEquipped(gearItem))
        //    {
        //        // 플레이어 스탯에 아이템 스탯들 전부 추가
        //        PlayerStats.UpdateStatModifier(gearItem.GetAbilityDict(), item);
        //    }
        //}

        //OnItemInvenChanged?.Invoke(); // 가지고 있는 아이템이 변경되었을 때 이벤트 호출
    }
}
