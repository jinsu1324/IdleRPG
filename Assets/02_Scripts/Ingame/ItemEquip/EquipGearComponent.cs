using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� ���� ������Ʈ
/// </summary>
public class EquipGearComponent : MonoBehaviour
{
    [Title("��� ����", bold : false)]
    [SerializeField] private Transform _helmetSlot;    // ��佽��
    [SerializeField] private Transform _armorSlot;     // ���ʽ���
    [SerializeField] private Transform _weaponSlot;    // ���⽽��

    [Title("�⺻ ��", bold: false)]
    [SerializeField] private GameObject _basicHand;    // �⺻ ��

    private GameObject _currentHelmet;                 // ������ ���
    private GameObject _currentArmor;                  // ������ ����
    private GameObject _currentWeapon;                 // ������ ����

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipGearManager.OnEquipGear += EquipPlayer; // ��� �����Ҷ� -> �÷��̾ �� ��� ����
        EquipGearManager.OnUnEquipGear += UnEquipPlayer; // ��� �����Ҷ� -> �÷��̾ �� ��� ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipGearManager.OnEquipGear -= EquipPlayer;
        EquipGearManager.OnUnEquipGear -= UnEquipPlayer;
    }

    /// <summary>
    /// �÷��̾ ����
    /// </summary>
    public void EquipPlayer(Item item)
    {
        GearDataSO gearDataSO = DataManager.Instance.GetGearDataSO(item.ID);
        GameObject prefab = gearDataSO.Prefab;

        switch (item.ItemType)
        {
            case ItemType.Helmet:
                EquipGear(prefab, _helmetSlot, ref _currentHelmet);
                break;

            case ItemType.Armor:
                EquipGear(prefab, _armorSlot, ref _currentArmor);
                break;

            case ItemType.Weapon:
                EquipGear(prefab, _weaponSlot, ref _currentWeapon);
                Hide_BasicHand();   // �� �ѱ�
                AttackAnimType attackAnimType = (AttackAnimType)Enum.Parse(typeof(AttackAnimType), gearDataSO.AttackAnimType);
                GetComponent<AnimComponent>().Change_AttackAnimType(attackAnimType);    // �ִϸ��̼� Ÿ�� ����
                break;
        }
    }

    /// <summary>
    /// �÷��̾�� ���� ����
    /// </summary>
    public void UnEquipPlayer(Item item)
    {
        GearDataSO gearDataSO = DataManager.Instance.GetGearDataSO(item.ID);
        GameObject prefab = gearDataSO.Prefab;

        switch (item.ItemType)
        {
            case ItemType.Helmet:
                UnEquipGear(ref _currentHelmet);
                break;

            case ItemType.Armor:
                UnEquipGear(ref _currentArmor);
                break;

            case ItemType.Weapon:
                UnEquipGear(ref _currentWeapon);
                Show_BasicHand();   // �� ����
                GetComponent<AnimComponent>().Change_AttackAnimType(AttackAnimType.Hand);   // �ִϸ��̼� Ÿ�� ������
                break;
        }
    }

    /// <summary>
    /// ��� ����
    /// </summary>
    private void EquipGear(GameObject prefab, Transform slot, ref GameObject currentGear)
    {
        // ���� �������� ������ ����
        if (currentGear != null)
            Destroy(currentGear);

        // ���Կ� ��� ������ ����
        currentGear = Instantiate(prefab, slot);
    }

    /// <summary>
    /// ��� ��������
    /// </summary>
    private void UnEquipGear(ref GameObject currentGear)
    {
        // ���� �������� ������ ����
        if (currentGear != null)
            Destroy(currentGear);
    }

    /// <summary>
    /// �� �ѱ�
    /// </summary>
    private void Show_BasicHand()
    {
        _basicHand.gameObject.SetActive(true);
    }

    /// <summary>
    /// �� ����
    /// </summary>
    private void Hide_BasicHand()
    {
        _basicHand.gameObject.SetActive(false);
    }
}
