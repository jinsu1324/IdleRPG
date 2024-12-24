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
        item.RemoveCountByEnhance();    // 아이템 갯수 감소
        item.ItemLevelUp();             // 아이템 레벨업

        // 해당 아이템이 장착되어 있을때만
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
