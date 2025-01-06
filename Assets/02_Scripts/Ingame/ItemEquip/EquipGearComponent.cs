using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장착 아이템 컴포넌트
/// </summary>
public class EquipGearComponent : MonoBehaviour
{
    [Title("아이템 슬롯", bold : false)]
    [SerializeField] private Transform _helmetSlot;    // 헬멧슬롯
    [SerializeField] private Transform _armorSlot;     // 갑옷슬롯
    [SerializeField] private Transform _weaponSlot;    // 무기슬롯

    [Title("기본 손", bold: false)]
    [SerializeField] private GameObject _basicHand;    // 기본 손

    private GameObject _equipHelmet;                   // 장착된 헬멧
    private GameObject _equipArmor;                    // 장착된 갑옷
    private GameObject _equipWeapon;                   // 장착된 무기

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipGearManager.OnEquipGearChanged += ChangePlayerGear_ByItemType;   // 아이템이 장착되었을 때, 플레이어도 아이템 장착
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipGearManager.OnEquipGearChanged -= ChangePlayerGear_ByItemType;
    }

    /// <summary>
    /// 아이템 타입에 따라 플레이어 장비 변경
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
    /// 실제 장비 변경 로직
    /// </summary>
    private void ChangeGear(GameObject prefab, Transform slot, ref GameObject equipGear, bool tryEquip)
    {
        TryDestroy_EquipGear(equipGear); // 장착 아이템이 있으면 지우기

        if (tryEquip) // 장착이면 프리팹스폰하고 손끄기
            Spawn_GearPrefab(prefab, slot, ref equipGear);
    }

    /// <summary>
    /// 기본 손 켜기 / 끄기
    /// </summary>
    private void ChangeBasicHand(bool tryEquip)
    {
        _basicHand.gameObject.SetActive(!tryEquip);
    }

    /// <summary>
    /// 장착 장비가 있으면 지우기
    /// </summary>
    private void TryDestroy_EquipGear(GameObject equipItem)
    {
        if (equipItem != null)
            Destroy(equipItem);
    }

    /// <summary>
    /// 장비 프리팹 슬롯에 스폰
    /// </summary>
    private void Spawn_GearPrefab(GameObject prefab, Transform slot, ref GameObject equipGear)
    {
        equipGear = Instantiate(prefab, slot);
    }

    
}
