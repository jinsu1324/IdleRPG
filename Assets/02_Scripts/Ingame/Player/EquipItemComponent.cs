using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemComponent : MonoBehaviour
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
    /// Start
    /// </summary>
    private void Start()
    {
        EquipItemManager.OnEquipItemChanged += ChangePlayerItem_ByItemType;   // �������� �����Ǿ��� ��, �÷��̾ ������ ����
    }

    /// <summary>
    /// ������ Ÿ�Կ� ���� �÷��̾� ������ ����
    /// </summary>
    public void ChangePlayerItem_ByItemType(OnEquipItemChangedArgs args, bool tryEquip)
    {
        switch (args.ItemType)
        {
            case ItemType.Helmet:
                ChangeItem(args.Prefab, _helmetSlot, ref _equipHelmet, tryEquip);
                break;

            case ItemType.Armor:
                ChangeItem(args.Prefab, _armorSlot, ref _equipArmor, tryEquip);
                break;

            case ItemType.Weapon:
                ChangeItem(args.Prefab, _weaponSlot, ref _equipWeapon, tryEquip);
                ChangeBasicHand(tryEquip);
                GetComponent<AnimComponent>().Change_AttackAnimType((AttackAnimType)Enum.Parse(typeof(AttackAnimType), args.AttackAnimType));
                break;
        }
    }

    /// <summary>
    /// ���� ������ ���� ����
    /// </summary>
    private void ChangeItem(GameObject prefab, Transform slot, ref GameObject equipItem, bool tryEquip)
    {
        TryDestroy_EquipItem(equipItem); // ���� �������� ������ �����

        if (tryEquip) // �����̸� �����ս����ϰ� �ղ���
            Spawn_ItemPrefab(prefab, slot, ref equipItem);
    }

    /// <summary>
    /// �⺻ �� �ѱ� / ����
    /// </summary>
    private void ChangeBasicHand(bool tryEquip)
    {
        _basicHand.gameObject.SetActive(!tryEquip);
    }

    /// <summary>
    /// ���� �������� ������ �����
    /// </summary>
    private void TryDestroy_EquipItem(GameObject equipItem)
    {
        if (equipItem != null)
            Destroy(equipItem);
    }

    /// <summary>
    /// ������ ������ ���Կ� ����
    /// </summary>
    private void Spawn_ItemPrefab(GameObject prefab, Transform slot, ref GameObject equipItem)
    {
        equipItem = Instantiate(prefab, slot);
    }

    /// <summary>
    /// �� �ѱ�
    /// </summary>
    private void BasicHandOn()
    {
        _basicHand.gameObject.SetActive(true);
    }

    /// <summary>
    /// �� ����
    /// </summary>
    private void BasicHandOFF()
    {
        _basicHand.gameObject.SetActive(false);
    }

    /// <summary>
    /// OnDestroy
    /// </summary>
    private void OnDestroy()
    {
        EquipItemManager.OnEquipItemChanged -= ChangePlayerItem_ByItemType;
    }
}
