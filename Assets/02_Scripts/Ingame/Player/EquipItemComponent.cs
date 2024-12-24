using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _helmet;
    [SerializeField] private SpriteRenderer _armor;
    [SerializeField] private SpriteRenderer _weapon;

    private void Start()
    {
        EquipItemManager.OnEquipItemChanged += ChangePlayerEquipItem;
    }


    public void ChangePlayerEquipItem(ItemType itemType, Sprite icon)
    {
        switch (itemType)
        {
            case ItemType.Helmet:
                _helmet.sprite = icon;
                break;
            case ItemType.Armor:
                _armor.sprite = icon;
                break;
            case ItemType.Weapon:
                _weapon.sprite = icon;
                break;
        }

    }


    private void OnDestroy()
    {
        EquipItemManager.OnEquipItemChanged -= ChangePlayerEquipItem;

    }
}
