using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장비 장착 컴포넌트
/// </summary>
public class EquipGearComponent : MonoBehaviour
{
    [Title("장비 슬롯", bold : false)]
    [SerializeField] private Transform _helmetSlot;    // 헬멧슬롯
    [SerializeField] private Transform _armorSlot;     // 갑옷슬롯
    [SerializeField] private Transform _weaponSlot;    // 무기슬롯

    [Title("기본 손", bold: false)]
    [SerializeField] private GameObject _basicHand;    // 기본 손

    private GameObject _currentHelmet;                 // 장착된 헬멧
    private GameObject _currentArmor;                  // 장착된 갑옷
    private GameObject _currentWeapon;                 // 장착된 무기

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipGearManager.OnEquipGear += EquipPlayer; // 장비 장착할때 -> 플레이어도 그 장비 장착
        EquipGearManager.OnUnEquipGear += UnEquipPlayer; // 장비 해제할때 -> 플레이어도 그 장비 해제
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
    /// 플레이어에 장착
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
                Hide_BasicHand();   // 손 켜기
                AttackAnimType attackAnimType = (AttackAnimType)Enum.Parse(typeof(AttackAnimType), gearDataSO.AttackAnimType);
                GetComponent<AnimComponent>().Change_AttackAnimType(attackAnimType);    // 애니메이션 타입 변경
                break;
        }
    }

    /// <summary>
    /// 플레이어에서 장착 해제
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
                Show_BasicHand();   // 손 끄기
                GetComponent<AnimComponent>().Change_AttackAnimType(AttackAnimType.Hand);   // 애니메이션 타입 손으로
                break;
        }
    }

    /// <summary>
    /// 장비 장착
    /// </summary>
    private void EquipGear(GameObject prefab, Transform slot, ref GameObject currentGear)
    {
        // 장착 아이템이 있으면 삭제
        if (currentGear != null)
            Destroy(currentGear);

        // 슬롯에 장비 프리팹 생성
        currentGear = Instantiate(prefab, slot);
    }

    /// <summary>
    /// 장비 장착해제
    /// </summary>
    private void UnEquipGear(ref GameObject currentGear)
    {
        // 장착 아이템이 있으면 삭제
        if (currentGear != null)
            Destroy(currentGear);
    }

    /// <summary>
    /// 손 켜기
    /// </summary>
    private void Show_BasicHand()
    {
        _basicHand.gameObject.SetActive(true);
    }

    /// <summary>
    /// 손 끄기
    /// </summary>
    private void Hide_BasicHand()
    {
        _basicHand.gameObject.SetActive(false);
    }
}
