using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _helmet;    // 헬멧
    [SerializeField] private SpriteRenderer _armor;     // 갑옷
    [SerializeField] private SpriteRenderer _weapon;    // 무기

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        EquipItemManager.OnEquipItemChanged += ChangePlayerEquipItem;   // 장착된 아이템이 변경되었을 때, 플레이어 착용아이템 변경
    }

    /// <summary>
    /// 플레이어 착용 아이템 변경
    /// </summary>
    public void ChangePlayerEquipItem(ItemType itemType, Sprite itemSprite)
    {
        switch (itemType)
        {
            case ItemType.Helmet:
                _helmet.sprite = itemSprite;
                break;
            case ItemType.Armor:
                _armor.sprite = itemSprite;
                break;
            case ItemType.Weapon:
                _weapon.sprite = itemSprite;
                break;
        }

    }

    /// <summary>
    /// OnDestroy
    /// </summary>
    private void OnDestroy()
    {
        EquipItemManager.OnEquipItemChanged -= ChangePlayerEquipItem;
    }
}
