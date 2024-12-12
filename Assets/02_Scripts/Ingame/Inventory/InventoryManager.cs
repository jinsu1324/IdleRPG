using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonBase<InventoryManager>
{
    [Title("무기 인벤토리 관련", Bold = false)]
    [SerializeField] private ItemInventory _weaponItemInventory;
    [SerializeField] private ItemSlotManager _weaponItemSlotManager;
    [SerializeField] private ItemEquipManager _weaponItemEquipManager;

    [Title("갑옷 인벤토리 관련", Bold = false)]
    [SerializeField] private ItemInventory _armorItemInventory;
    [SerializeField] private ItemSlotManager _armorItemSlotManager;
    [SerializeField] private ItemEquipManager _armorItemEquipManager;


    private Dictionary<ItemType, ItemInventory> _itemInventoryDict = new Dictionary<ItemType, ItemInventory>();
    private Dictionary<ItemType, ItemSlotManager> _itemSlotManagerDict = new Dictionary<ItemType, ItemSlotManager>();
    private Dictionary<ItemType, ItemEquipManager> _itemEquipManagerDict = new Dictionary<ItemType, ItemEquipManager>();


    protected override void Awake()
    {
        base.Awake();

        _itemInventoryDict.Add(ItemType.Weapon, _weaponItemInventory);
        _itemInventoryDict.Add(ItemType.Armor, _armorItemInventory);

        _itemSlotManagerDict.Add(ItemType.Weapon, _weaponItemSlotManager);
        _itemSlotManagerDict.Add(ItemType.Armor, _armorItemSlotManager);

        _itemEquipManagerDict.Add(ItemType.Weapon, _weaponItemEquipManager);
        _itemEquipManagerDict.Add(ItemType.Armor, _armorItemEquipManager);
    }

    public ItemInventory GetItemInventory(ItemType itemType)
    {
        return _itemInventoryDict[itemType];
    }

    public ItemSlotManager GetItemSlotManager(ItemType itemType)
    {
        return _itemSlotManagerDict[itemType];
    }

    public ItemEquipManager GetItemEquipManager(ItemType itemType)
    {
        return _itemEquipManagerDict[itemType];
    }
}
