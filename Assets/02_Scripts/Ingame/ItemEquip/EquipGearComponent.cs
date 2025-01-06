using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������ ������Ʈ
/// </summary>
public class EquipGearComponent : MonoBehaviour
{
    [Title("������ ����", bold : false)]
    [SerializeField] private Transform _helmetSlot;    // ��佽��
    [SerializeField] private Transform _armorSlot;     // ���ʽ���
    [SerializeField] private Transform _weaponSlot;    // ���⽽��

    [Title("�⺻ ��", bold: false)]
    [SerializeField] private GameObject _basicHand;    // �⺻ ��

    private GameObject _equipHelmet;                   // ������ ���
    private GameObject _equipArmor;                    // ������ ����
    private GameObject _equipWeapon;                   // ������ ����

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipGearManager.OnEquipGearChanged += ChangePlayerGear_ByItemType;   // �������� �����Ǿ��� ��, �÷��̾ ������ ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipGearManager.OnEquipGearChanged -= ChangePlayerGear_ByItemType;
    }

    /// <summary>
    /// ������ Ÿ�Կ� ���� �÷��̾� ��� ����
    /// </summary>
    public void ChangePlayerGear_ByItemType(OnEquipGearChangedArgs args)
    {
        switch (args.ItemType)
        {
            case ItemType.Helmet:
                ChangeGear(args.Prefab, _helmetSlot, ref _equipHelmet, args.IsTryEquip);
                break;

            case ItemType.Armor:
                ChangeGear(args.Prefab, _armorSlot, ref _equipArmor, args.IsTryEquip);
                break;

            case ItemType.Weapon:
                ChangeGear(args.Prefab, _weaponSlot, ref _equipWeapon, args.IsTryEquip);
                ChangeBasicHand(args.IsTryEquip);
                GetComponent<AnimComponent>().Change_AttackAnimType((AttackAnimType)Enum.Parse(typeof(AttackAnimType), args.AttackAnimType));
                break;
        }
    }

    /// <summary>
    /// ���� ��� ���� ����
    /// </summary>
    private void ChangeGear(GameObject prefab, Transform slot, ref GameObject equipGear, bool tryEquip)
    {
        TryDestroy_EquipGear(equipGear); // ���� �������� ������ �����

        if (tryEquip) // �����̸� �����ս����ϰ� �ղ���
            Spawn_GearPrefab(prefab, slot, ref equipGear);
    }

    /// <summary>
    /// �⺻ �� �ѱ� / ����
    /// </summary>
    private void ChangeBasicHand(bool tryEquip)
    {
        _basicHand.gameObject.SetActive(!tryEquip);
    }

    /// <summary>
    /// ���� ��� ������ �����
    /// </summary>
    private void TryDestroy_EquipGear(GameObject equipItem)
    {
        if (equipItem != null)
            Destroy(equipItem);
    }

    /// <summary>
    /// ��� ������ ���Կ� ����
    /// </summary>
    private void Spawn_GearPrefab(GameObject prefab, Transform slot, ref GameObject equipGear)
    {
        equipGear = Instantiate(prefab, slot);
    }

    
}
