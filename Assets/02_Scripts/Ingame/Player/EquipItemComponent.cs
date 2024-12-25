using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemComponent : MonoBehaviour
{
    [SerializeField] private Transform _helmetSlot;    // ��佽��
    [SerializeField] private Transform _armorSlot;     // ���ʽ���
    [SerializeField] private Transform _weaponSlot;    // ���⽽��

    [SerializeField] private GameObject _basicHand;

    private GameObject _equipHelmetPrefab;
    private GameObject _equipArmorPrefab;
    private GameObject _equipWeaponPrefab;

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        EquipItemManager.OnItemEquipped += EquipPlayerItem;   // �������� �����Ǿ��� ��, �÷��̾� ������ ����
        EquipItemManager.OnItemUnEquipped += UnEquipPlayerItem;   // �������� ���������Ǿ��� ��, �÷��̾� ������ ��������
    }

    /// <summary>
    /// �÷��̾� ������ ����
    /// </summary>
    public void EquipPlayerItem(OnEquipItemChangedArgs args)
    {
        switch (args.ItemType)
        {
            case ItemType.Helmet:
                if (_equipHelmetPrefab != null)
                    Destroy(_equipHelmetPrefab);
                _equipHelmetPrefab = Instantiate(args.Prefab, _helmetSlot);
                break;

            case ItemType.Armor:
                if (_equipArmorPrefab != null)
                    Destroy(_equipArmorPrefab);
                _equipArmorPrefab = Instantiate(args.Prefab, _armorSlot);
                break;

            case ItemType.Weapon:
                if (_equipWeaponPrefab != null)
                    Destroy(_equipWeaponPrefab);
                _equipWeaponPrefab = Instantiate(args.Prefab, _weaponSlot);
                _basicHand.SetActive(false);
                GetComponent<AnimComponent>().
                    Change_AttackAnimType((AttackAnimType)Enum.Parse(typeof(AttackAnimType), args.AttackAnimType));
                break;
        }
    }

    /// <summary>
    /// �÷��̾� ������ ��������
    /// </summary>
    public void UnEquipPlayerItem(OnEquipItemChangedArgs args)
    {
        switch (args.ItemType)
        {
            case ItemType.Helmet:
                if (_equipHelmetPrefab != null)
                    Destroy(_equipHelmetPrefab);
                break;

            case ItemType.Armor:
                if (_equipArmorPrefab != null)
                    Destroy(_equipArmorPrefab);
                break;

            case ItemType.Weapon:
                if (_equipWeaponPrefab != null)
                    Destroy(_equipWeaponPrefab);
                _basicHand.SetActive(true);
                GetComponent<AnimComponent>().
                    Change_AttackAnimType((AttackAnimType)Enum.Parse(typeof(AttackAnimType), args.AttackAnimType));
                break;
        }
    }

    /// <summary>
    /// OnDestroy
    /// </summary>
    private void OnDestroy()
    {
        EquipItemManager.OnItemEquipped -= EquipPlayerItem;
        EquipItemManager.OnItemUnEquipped -= UnEquipPlayerItem;

    }
}
