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
        if (item is IEnhanceableItem enhanceableItem)
        {
            // ������ ���� ����
            enhanceableItem.RemoveCountByEnhance();    
           
            // ������ ������
            enhanceableItem.ItemLevelUp();             

            // ������ ��ȭ �̺�Ʈ ��Ƽ
            OnItemEnhance?.Invoke(item);    

            // ����϶���
            if (item is GearItem gearItem)
            {
                // �ش� ��� �����Ǿ� ��������
                if (EquipGearManager.IsEquipped(gearItem))
                {
                    // �÷��̾� ���� ������Ʈ
                    PlayerStats.UpdateStatModifier(gearItem.GetAttributeDict(), item);
                }
            }
        }
    }
}
