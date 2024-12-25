using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemComponent : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _helmet;    // ���
    [SerializeField] private SpriteRenderer _armor;     // ����
    [SerializeField] private SpriteRenderer _weapon;    // ����

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        EquipItemManager.OnEquipItemChanged += ChangePlayerEquipItem;   // ������ �������� ����Ǿ��� ��, �÷��̾� ��������� ����
    }

    /// <summary>
    /// �÷��̾� ���� ������ ����
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
