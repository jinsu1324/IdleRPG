using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ��ȭ ����
/// </summary>
public class ItemEnhanceManager
{
    /// <summary>
    /// ������ ��ȭ
    /// </summary>
    public static void Enhance(Item item)
    {
        if (item is IEnhanceableItem enhanceableItem)
        {
            enhanceableItem.RemoveCountByEnhance();    // ������ ���� ����
            enhanceableItem.ItemLevelUp();             // ������ ������

            Debug.Log($"��ȸ�� ������ {item.Name} - {item.Count} / {enhanceableItem.EnhanceableCount}");
        }

        //// ����϶���
        //if (item is GearItem gearItem)
        //{
        //    // �ش� �������� �����Ǿ� ��������
        //    if (EquipItemManager.IsEquipped(gearItem))
        //    {
        //        // �÷��̾� ���ȿ� ������ ���ȵ� ���� �߰�
        //        PlayerStats.UpdateStatModifier(gearItem.GetAbilityDict(), item);
        //    }
        //}

        //OnItemInvenChanged?.Invoke(); // ������ �ִ� �������� ����Ǿ��� �� �̺�Ʈ ȣ��
    }
}
