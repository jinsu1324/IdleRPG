using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 강화 관리자
/// </summary>
public class ItemEnhanceManager
{
    /// <summary>
    /// 강화
    /// </summary>
    public static void Enhance(Item item)
    {
        item.RemoveCountByEnhance();
        item.ItemLevelUp();

        // 장착했을때만
        if (EquipItemManager.IsEquipped(item))
        {
            // 플레이어 스탯에 아이템 스탯들 전부 추가
            foreach (var kvp in item.GetStatDict())
                PlayerStats.Instance.UpdateModifier(kvp.Key, kvp.Value, item);


            // 스탯 보여주는 UI 업데이트
            PlayerStats.Instance.AllStatUIUpdate();
        }
    }
}
