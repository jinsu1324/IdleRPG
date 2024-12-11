using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private List<Equipment> _haveItemList = new List<Equipment>();      // ������ �ִ� ������ ����Ʈ

    [SerializeField]
    private List<Image> _equippedItemIconList;                          // ������ ������ ������ ����Ʈ (�ִ� 3��)
    private List<Equipment> _equippedItemList = new List<Equipment>();  // ������ ������ ����Ʈ
    private int _maxEquipableItemCount = 3;                             // �ִ� ���� ������ ������ ����

    [SerializeField]
    private List<InventorySlot> _inventorySlotList;                     // �κ��丮 ���� ����Ʈ
    private InventorySlot _selectedSlot;                                // ���õ� ����

    [SerializeField]
    private SelectItemInfoPanel _selectedItemInfoPanel;                 // ���õ� ������ ���� �г�

    /// <summary>
    /// ���õ� ���� ���̶�����
    /// </summary>
    public void HighlightingSelectdSlot(InventorySlot newSelectedSlot)
    {
        // �̹� ���õ� ������ �ٽ� Ŭ������ ��� ����
        if (_selectedSlot == newSelectedSlot)
            return;

        // ������ ���õ� ������ �ִٸ�, �� ������ ���̶���Ʈ OFF
        if (_selectedSlot != null)
            _selectedSlot.SelectFrameOFF();

        // �� �������� ��ü �� ���̶���Ʈ ON
        _selectedSlot = newSelectedSlot;
        _selectedSlot.SelectFrameON();

        // ���õ� ������ ���� �г� �ѱ�
        SelectedItemInfoPanel_ON();
    }

    /// <summary>
    /// ����
    /// </summary>
    public void Equip(Equipment item)
    {
        // �̹� ������ ��������, �ִ� ���������� �ʰ������� �׳� ����
        if (_equippedItemList.Count >= _maxEquipableItemCount)
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

        // ����
        _equippedItemList.Add(item);

        // �÷��̾� ���ȿ� ������ ���ȵ� �߰�
        AddItemStatsToPlayer(item.GetStatDictionaryByLevel(), item);

        // ���Կ� ���� ������ ON
        FindSlotByItem(item).EquippedIconON();

        // ���� ������ ����Ʈ UI ������Ʈ
        Update_EquippedItemIconListUI();

        // ���� �����ִ� UI ������Ʈ
        PlayerStats.Instance.AllStatUIUpdate(); 
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void UnEquip(Equipment item)
    {
        // �����Ϸ��� �������� �������� �ʾ����� �׳� ����
        if (_equippedItemList.Contains(item) == false)
        {
            Debug.Log("�����Ϸ��� �������� �������� �ʾҽ��ϴ�!");
            return;
        }

        // ����
        _equippedItemList.Remove(item);

        // �÷��̾� ���ȿ��� ������ ���ȵ� ����
        RemoveItemStatsToPlayer(item.GetStatDictionaryByLevel(), item);

        // ���Կ� ���� ������ OFF
        FindSlotByItem(item).EqiuppedIconOFF();

        // ���� ������ ����Ʈ UI ������Ʈ
        Update_EquippedItemIconListUI();

        // ���� �����ִ� UI ������Ʈ
        PlayerStats.Instance.AllStatUIUpdate();
    }

    /// <summary>
    /// �÷��̾� ���ȿ� ������ ���ȵ� �߰�
    /// </summary>
    private void AddItemStatsToPlayer(Dictionary<StatType, int> statDict, Equipment selfItem)
    {
        foreach (var kvp in statDict)
            PlayerStats.Instance.AddModifier(kvp.Key, kvp.Value, selfItem);
    }

    /// <summary>
    /// �÷��̾� ���ȿ��� ������ ���ȵ� ����
    /// </summary>
    private void RemoveItemStatsToPlayer(Dictionary<StatType, int> statDict, Equipment selfItem)
    {
        foreach (var kvp in statDict)
            PlayerStats.Instance.RemoveModifier(kvp.Key, selfItem);
    }

    /// <summary>
    /// �ش� �������� ����ִ� ���� ã��
    /// </summary>
    private InventorySlot FindSlotByItem(Equipment item)
    {
        foreach (InventorySlot slot in _inventorySlotList)
        {
            if (slot.CurrentItem == item)
                return slot; 
        }

        return null;
    }

    /// <summary>
    /// ������ ������ ������ ����Ʈ UI ������Ʈ
    /// </summary>
    private void Update_EquippedItemIconListUI()
    {
        for (int i = 0; i < _equippedItemIconList.Count; i++)   // 3����ŭ �ݺ� (������ ���� �ִ� ����)
        {
            if (i < _equippedItemList.Count) // ���� ������ ���������� ������ ǥ��
            {
                _equippedItemIconList[i].sprite = _equippedItemList[i].Icon;
                _equippedItemIconList[i].gameObject.SetActive(true);
            }
            else // �������� ��Ȱ��ȭ
            {
                _equippedItemIconList[i].sprite = null;
                _equippedItemIconList[i].gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// �ش� �������� ������ ���������� Ȯ��
    /// </summary>
    public bool IsEquipped(Equipment item)
    {
        return _equippedItemList.Contains(item);
    }

    /// <summary>
    /// ���õ� ������ ���� �г� �ѱ�
    /// </summary>
    private void SelectedItemInfoPanel_ON()
    {
        _selectedItemInfoPanel.OpenAndInit(_selectedSlot);
    }







    //--------------- �Ʒ����ʹ� �ӽ� ������ ���� ȹ�� ���� �ڵ�


    /// <summary>
    /// ���� ������ �߰� ��ư
    /// </summary>
    public void OnClickAddRandomItem()
    {
        // ������ ������� ��������
        EquipmentDataSO equipmentDataSO 
            = DataManager.Instance.GetEquipmentDataSOByID(GetRandomEquipmentID().ToString());

        //EquipmentDataSO equipmentDataSO
        //   = DataManager.Instance.GetEquipmentDataSOByID(EquipmentID.Armor_ForestArmor.ToString());


        // ���� ��� �ϳ� ����
        //Equipment equipment = new Equipment(equipmentDataSO);
        Equipment equipment = new Equipment(equipmentDataSO, 1);

        // �߰�
        if (AddItem(equipment))
            Debug.Log($"{equipment.Name} ---- ȹ��!");
        else
            Debug.Log("�߰� ����!");

    }
    
    /// <summary>
    /// ������ �߰�
    /// </summary>
    public bool AddItem(Equipment item)
    {
        // ����ִ� ������ ã�Ƽ�, ���Կ� ������ �߰� + ���� �����۸���Ʈ���� �߰�
        foreach (InventorySlot slot in _inventorySlotList)
        {
            if (slot.IsSlotEmpty)
            {
                slot.AddItem(item);
                _haveItemList.Add(item);

                return true;
            }
        }

        return false; // ����
    }

    /// <summary>
    /// ���� ������ ���� ��ư
    /// </summary>
    public void OnClickRemoveRandomItem()
    {
        // ���� ������ �ƹ��͵� ������ �׳� ����
        if (_haveItemList.Count <= 0)
            return;

        // ���� �����۵� �߿� �ƹ��ų� ������
        int randomIndex = Random.Range(0, _haveItemList.Count);
        Equipment randomPickedEquipment = _haveItemList[randomIndex];

        // ����
        if (RemoveItem(randomPickedEquipment))
            Debug.Log($"{randomPickedEquipment.Name} ---- ����!");
        else
            Debug.Log($"���� ����!");
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public bool RemoveItem(Equipment item)
    {
        // �ش� �������� �ִ� ������ ã�Ƽ� ������ ���� + ���� �����ۿ����� ����
        foreach (InventorySlot slot in _inventorySlotList)
        {
            if (slot.CurrentItem == item)
            {
                slot.ClearItem();
                _haveItemList.Remove(item);

                return true;
            }
        }

        return false; // ����
    }

    /// <summary>
    /// ���� EquipmentID ��ȯ
    /// </summary>
    public EquipmentID GetRandomEquipmentID()
    {
        // Enum ������ �迭�� ������
        Array values = Enum.GetValues(typeof(EquipmentID));

        // ���� �ε��� ����
        int randomIndex = Random.Range(0, values.Length);

        // ���� EquipmentID ��ȯ
        return (EquipmentID)values.GetValue(randomIndex);
    }







    //private void Start()
    //{
    //    // PlayerStats �ʱ�ȭ
    //    PlayerStats playerStats = new PlayerStats();

    //    // ��� ����
    //    Equipment sword = new Equipment("������ ��", new Dictionary<StatType, float>
    //    {
    //        { StatType.AttackPower, 50 },
    //        { StatType.AttackSpeed, 10 }
    //    });

    //    // ��� ����
    //    sword.Equip(playerStats);
    //    Debug.Log("���� �� ���ݷ�: " + playerStats.GetFinalStat(StatType.AttackPower)); // +50

    //    // ��� ����
    //    sword.Unequip(playerStats);
    //    Debug.Log("���� �� ���ݷ�: " + playerStats.GetFinalStat(StatType.AttackPower)); // 0
    //}
}
