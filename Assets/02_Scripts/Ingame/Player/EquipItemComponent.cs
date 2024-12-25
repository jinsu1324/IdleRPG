using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItemComponent : MonoBehaviour
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
    /// Start
    /// </summary>
    private void Start()
    {
        EquipItemManager.OnEquipItemChanged += ChangePlayerItem_ByItemType;   // 아이템이 장착되었을 때, 플레이어도 아이템 장착
    }

    /// <summary>
    /// 아이템 타입에 따라 플레이어 아이템 변경
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
    /// 실제 아이템 변경 로직
    /// </summary>
    private void ChangeItem(GameObject prefab, Transform slot, ref GameObject equipItem, bool tryEquip)
    {
        TryDestroy_EquipItem(equipItem); // 장착 아이템이 있으면 지우기

        if (tryEquip) // 장착이면 프리팹스폰하고 손끄기
            Spawn_ItemPrefab(prefab, slot, ref equipItem);
    }

    /// <summary>
    /// 기본 손 켜기 / 끄기
    /// </summary>
    private void ChangeBasicHand(bool tryEquip)
    {
        _basicHand.gameObject.SetActive(!tryEquip);
    }

    /// <summary>
    /// 장착 아이템이 있으면 지우기
    /// </summary>
    private void TryDestroy_EquipItem(GameObject equipItem)
    {
        if (equipItem != null)
            Destroy(equipItem);
    }

    /// <summary>
    /// 아이템 프리팹 슬롯에 스폰
    /// </summary>
    private void Spawn_ItemPrefab(GameObject prefab, Transform slot, ref GameObject equipItem)
    {
        equipItem = Instantiate(prefab, slot);
    }

    /// <summary>
    /// 손 켜기
    /// </summary>
    private void BasicHandOn()
    {
        _basicHand.gameObject.SetActive(true);
    }

    /// <summary>
    /// 손 끄기
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
