using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ��ȭ ����
/// </summary>
public class ItemEnhanceManager
{
    public static event Action<Item> OnItemEnhance;   // ������ ��ȭ�Ҷ� �̺�Ʈ

    /// <summary>
    /// ������ ��ȭ
    /// </summary>
    public static void Enhance(Item item)
    {
        ItemDataSO itemDataSO = ItemManager.GetItemDataSO(item.ID);
        int enhanceCount = itemDataSO.GetEnhanceCount(item.Level);

        item.ReduceCount(enhanceCount);
        item.LevelUp();
        
        OnItemEnhance?.Invoke(item);

        // ����̰� ������������ �÷��̾� ���ȿ� ����
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
