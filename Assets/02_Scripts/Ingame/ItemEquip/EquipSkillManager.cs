using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 장착한 스킬 관리
/// </summary>
public class EquipSkillManager
{
    public static event Action<Item> OnEquipSkillChanged;   // 장착스킬 바뀌었을때 이벤트
    public static event Action OnSkillSwapStarted;          // 장착스킬교체 시작할 때 이벤트
    public static event Action OnSkillSwapFinished;         // 장착스킬교체 끝났을 때 이벤트

    private static Item[] _equipSkillArr;                   // 장착한 스킬 배열
    private static int _maxCount = 3;                       // 스킬 장착 최대 갯수

    private static Item _swapTargetItem;                    // 스왑 타겟 아이템

    /// <summary>
    /// 정적 생성자 (클래스가 처음 참조될 때 한 번만 호출)
    /// </summary>
    static EquipSkillManager()
    {
        _equipSkillArr = new Item[_maxCount];
    }
    
    /// <summary>
    /// 스킬 장착
    /// </summary>
    public static void Equip(Item item, int slotIndex = -1)
    {
        // 이미 그 아이템을 장착중이면 그냥 무시
        if (IsEquipped(item))
            return;

        // 이미 최대로 장착했으면 교체시작
        if (IsEquipMax())
        {
            _swapTargetItem = item;
            OnSkillSwapStarted?.Invoke(); // 스왑 시작 알림

            return;
        }

        // 슬롯 지정 안했다면, 비어있는 슬롯 인덱스 아무거나 가져오기
        if (slotIndex == -1)
            slotIndex = GetEmptySlotIndex();

        // 장착
        _equipSkillArr[slotIndex] = item;

        // 장착스킬 바뀌었을때 이벤트 노티
        OnEquipSkillChanged?.Invoke(item);
    }

    /// <summary>
    /// 스킬 장착 해제
    /// </summary>
    public static void UnEquip(Item item)
    {
        // 해당 아이템이 장착되어있는 슬롯인덱스 가져오기
        int index = GetEquippedIndex(item); 

        // 인덱스 못찾았으면 그냥 리턴
        if (index < 0)
            return;
        
        // 장착 해제
        _equipSkillArr[index] = null;

        // 장착스킬 바뀌었을때 이벤트 노티
        OnEquipSkillChanged?.Invoke(item);
    }

    /// <summary>
    /// 스킬 교체
    /// </summary>
    public static void Swap(Item equipSlotItem)
    {
        UnEquip(equipSlotItem); // 장착슬롯꺼 먼저 해제
        Equip(_swapTargetItem); // 새로운 아이템 장착

        OnSkillSwapFinished?.Invoke();
    }

    /// <summary>
    /// 장착한 아이템인지?
    /// </summary>
    public static bool IsEquipped(Item item)
    {
        return _equipSkillArr.Contains(item);
    }

    /// <summary>
    /// 최대로 착용했는지?
    /// </summary>
    private static bool IsEquipMax()
    {
        return GetEmptySlotIndex() == -1;   // 비어있는 슬롯이 하나도 없으면 최대착용임
    }

    /// <summary>
    /// 해당 아이템이 장착되어있는 슬롯인덱스 가져오기
    /// </summary>
    private static int GetEquippedIndex(Item item)
    {
        return Array.FindIndex(_equipSkillArr, s => s == item);
    }

    /// <summary>
    /// 비어있는 슬롯 인덱스 가져오기
    /// </summary>
    private static int GetEmptySlotIndex()
    {
        return Array.FindIndex(_equipSkillArr, s => s == null);
    }

    /// <summary>
    /// 해당 슬롯에 장착되어있는 스킬아이템 가져오기
    /// </summary>
    public static Item GetEquipSkill(int slotIndex)
    {
        return _equipSkillArr[slotIndex];
    }

    /// <summary>
    /// 장착한 스킬아이템들 가져오기(아이템 타입)
    /// </summary>
    public static Item[] GetEquipSkills_ItemType()
    {
        if (_equipSkillArr.All(s => s == null)) // 모든 슬롯이 비어있다면(null) null 반환
            return null;

        return _equipSkillArr;
    }

    /// <summary>
    /// 장착한 스킬들 가져오기 (사용하기 위해 '스킬'형태로 변환해서)
    /// </summary>
    public static Skill[] GetEquipSkills_SkillType()
    {
        Skill[] castSkillArr = new Skill[_maxCount];

        for (int i = 0; i < _equipSkillArr.Length; i++)
        {
            Item equipItem = _equipSkillArr[i];

            if (equipItem == null)
            {
                castSkillArr[i] = null;
                continue;
            }

            castSkillArr[i] = SkillFactory.CreateSkill(equipItem.ID, equipItem.Level);
        }

        return castSkillArr;
    }
}
