using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ��ȭ ����
/// </summary>
public class ItemEnhanceManager
{
    public static event Action<Item> OnItemEnhance;   // ������ ��ȭ�Ҷ� �̺�Ʈ (��� + ��ų ����)
    public static event Action<Item> OnSkillEnhance;  // ��ų ��ȭ�Ҷ� �̺�Ʈ (��ų�� ���� �÷�����)  

    /// <summary>
    /// ������ ��ȭ
    /// </summary>
    public static void Enhance(Item item)
    {
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(item.ID);
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

        if (item.ItemType == ItemType.Skill)
        {
            OnSkillEnhance?.Invoke(item);
        }
    }

    /// <summary>
    /// �ش� �������� ��ȭ��������?
    /// </summary>
    public static bool CanEnhance(Item item)
    {
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(item.ID);
        return item.Count >= itemDataSO.GetEnhanceCount(item.Level);
    }
}
