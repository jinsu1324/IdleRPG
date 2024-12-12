using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquipManager : MonoBehaviour
{
    [SerializeField] private List<Image> _equipItemIconList;        // ������ ������ ������ ����Ʈ (�ִ� 3��)

    private List<Item> _equipItemList = new List<Item>();           // ������ ������ ����Ʈ
    private int _maxCount = 3;                                      // �ִ� ���� ������ ������ ����


    /// <summary>
    /// ����
    /// </summary>
    public void Equip(Item item)
    {
        // �ִ� ���������� �ʰ������� �׳� ����
        if (CanEquipMore() == false)
        {
            Debug.Log("���� ������ �ִ� ������ ������ �ʰ��߽��ϴ�!");
            return;
        }

        // �̹� ������ �������̸� �׳� ����
        if (IsEquipped(item))
        {
            Debug.Log("�̹� ������ �������Դϴ�!");
            return;
        }

        // ���������� ����Ʈ�� �߰�
        AddEquipItemList(item);

        // �÷��̾� ���ȿ� ������ ���ȵ� �߰�
        PlayerStats.Instance.AddItemStatsToPlayer(item.GetStatDictionaryByLevel(), item);

        // ���Կ� ���� ������ ON
        ItemSlotFinder.FindSlot_ContainItem(item, InventoryManager.Instance.GetItemSlotManager(item.ItemType).GetItemSlotList()).EquippedIconON();

        

        // ���� ������ ����Ʈ UI ������Ʈ
        Update_EquippedItemIconListUI();

        // ���� �����ִ� UI ������Ʈ
        PlayerStats.Instance.AllStatUIUpdate();
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void UnEquip(Item item)
    {
        // �����Ϸ��� �������� �������� �ʾ����� �׳� ����
        if (IsEquipped(item) == false)
        {
            Debug.Log("�����Ϸ��� �������� �������� �ʾҽ��ϴ�!");
            return;
        }

        // ���� ������ ����Ʈ���� ����
        RemoveEquipItemList(item);

        // �÷��̾� ���ȿ��� ������ ���ȵ� ����
        PlayerStats.Instance.RemoveItemStatsToPlayer(item.GetStatDictionaryByLevel(), item);

        // ���Կ� ���� ������ OFF
        ItemSlotFinder.FindSlot_ContainItem(item, InventoryManager.Instance.GetItemSlotManager(item.ItemType).GetItemSlotList()).EquippedIconOFF();

        // ���� ������ ����Ʈ UI ������Ʈ
        Update_EquippedItemIconListUI();

        // ���� �����ִ� UI ������Ʈ
        PlayerStats.Instance.AllStatUIUpdate();
    }

    /// <summary>
    /// �� ������ �� �ִ���?
    /// </summary>
    public bool CanEquipMore()
    {
        return _equipItemList.Count < _maxCount;
    }

    /// <summary>
    /// �ش� �������� ������ ����������?
    /// </summary>
    public bool IsEquipped(Item item)
    {
        return _equipItemList.Contains(item);
    }

    /// <summary>
    /// ���� ������ ����Ʈ�� �߰�
    /// </summary>
    public void AddEquipItemList(Item item)
    {
        _equipItemList.Add(item);
    }

    /// <summary>
    /// ���� ������ ����Ʈ���� ����
    /// </summary>
    public void RemoveEquipItemList(Item item)
    {
        _equipItemList.Remove(item);
    }

    /// <summary>
    /// ������ ������ ������ ����Ʈ UI ������Ʈ
    /// </summary>
    public void Update_EquippedItemIconListUI()
    {
        for (int i = 0; i < _equipItemIconList.Count; i++)   // 3����ŭ �ݺ� (������ ���� �ִ� ����)
        {
            if (i < _equipItemList.Count) // ���� ������ ���������� ������ ǥ��
            {
                _equipItemIconList[i].sprite = _equipItemList[i].Icon;
                _equipItemIconList[i].gameObject.SetActive(true);
            }
            else // �������� ��Ȱ��ȭ
            {
                _equipItemIconList[i].sprite = null;
                _equipItemIconList[i].gameObject.SetActive(false);
            }
        }
    }
}
