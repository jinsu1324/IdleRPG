using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ��ȭ ������
/// </summary>
public class ItemEnhanceManager
{
    /// <summary>
    /// ��ȭ
    /// </summary>
    public static void Enhance(Item item)
    {
        item.RemoveCountByEnhance();    // ������ ���� ����
        item.ItemLevelUp();             // ������ ������

        // �ش� �������� �����Ǿ� ��������
        if (EquipItemManager.IsEquipped(item))
        {
            // �÷��̾� ���ȿ� ������ ���ȵ� ���� �߰�
            PlayerStats.Instance.UpdateModifier(item.GetStatDict(), item);
        }
    }
}
