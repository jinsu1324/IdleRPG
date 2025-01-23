using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 스킬 장착 슬롯
/// </summary>
public enum SkillSlot
{
    None,   // 슬롯 없음 표시용
    First,  // 첫번째 슬롯
    Second, // 두번째 슬롯
    Third,  // 세번째 슬롯
}

/// <summary>
/// 장착한 스킬 관리
/// </summary>
public class EquipSkillManager : ISavable
{
    public static event Action<Item> OnEquipSkillChanged;   // 장착스킬 바뀌었을때 이벤트
    public static event Action OnSkillSwapStarted;          // 장착스킬교체 시작할 때 이벤트
    public static event Action OnSkillSwapFinished;         // 장착스킬교체 끝났을 때 이벤트
    
    private static int _maxCount = 3;                       // 스킬 장착가능 갯수
    private static Item _swapTargetItem;                    // 스왑 타겟 아이템
    public string Key => nameof(EquipSkillManager);         // Firebase 데이터 저장용 고유 키 설정
    [SaveField] private static Dictionary<SkillSlot, Item> _equipSkillDict = new Dictionary<SkillSlot, Item>(); // 장착 스킬 딕셔너리

    /// <summary>
    /// 데이터 불러오기할때 태스크들
    /// </summary>
    public void DataLoadTask()
    {
        OnEquipSkillChanged?.Invoke(new Item("Weapon_Sword", "Weapon", 1, 1)); // 매개변수는 임시데이터입니다. args로 전체적으로 변경해야함
    }

    /// <summary>
    /// 스킬 장착
    /// </summary>
    public static void Equip(Item item)
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

        // 비어있는 슬롯 인덱스 아무거나 가져오기
        SkillSlot skillSlot = GetEmptySlot();
        
        // 장착
        _equipSkillDict[skillSlot] = item;

        // 장착스킬 바뀌었을때 이벤트 노티
        OnEquipSkillChanged?.Invoke(item);
    }

    /// <summary>
    /// 스킬 장착 해제
    /// </summary>
    public static void UnEquip(Item item)
    {
        // 해당 아이템이 장착되어있는 슬롯 가져오기
        SkillSlot skillSlot = GetEquippedIndex(item); 

        // 인덱스 못찾았으면 그냥 리턴
        if (skillSlot == SkillSlot.None)
            return;

        // 장착 해제
        _equipSkillDict[skillSlot] = null;

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
        return _equipSkillDict.Values.Any(x => x != null && x.ID == item.ID);
    }

    /// <summary>
    /// 최대로 착용했는지?
    /// </summary>
    private static bool IsEquipMax()
    {
        CheckAnd_SetDict();

        return _equipSkillDict.Values.All(x => x != null);
    }

    /// <summary>
    /// 해당 아이템이 장착되어있는 슬롯 가져오기
    /// </summary>
    private static SkillSlot GetEquippedIndex(Item item)
    {
        foreach (var kvp in _equipSkillDict)
        {
            SkillSlot keyIndex = kvp.Key;
            Item existItem = kvp.Value;

            if (existItem == null)
                continue;

            if (existItem.ID == item.ID)
                return keyIndex;
        }

        return SkillSlot.None;
    }

    /// <summary>
    /// 비어있는 슬롯 가져오기
    /// </summary>
    private static SkillSlot GetEmptySlot()
    {
        CheckAnd_SetDict();

        foreach (var kvp in _equipSkillDict)
        {
            SkillSlot skillSlot = kvp.Key;
            Item item = kvp.Value;

            if (item == null)
                return skillSlot;
        }

        return SkillSlot.None;
    }

    /// <summary>
    /// 해당 슬롯에 장착되어있는 스킬아이템 가져오기
    /// </summary>
    public static Item GetEquipSkill(SkillSlot skillSlot)
    {
        CheckAnd_SetDict();

        return _equipSkillDict[skillSlot];
    }

    /// <summary>
    /// 장착한 스킬들 가져오기 (사용하기 위해 '스킬'형태로 변환해서)
    /// </summary>
    public static Skill[] GetEquipSkills_SkillType()
    {
        CheckAnd_SetDict();

        Skill[] castSkillArr = new Skill[_maxCount];

        SkillSlot[] allSlots = (SkillSlot[])Enum.GetValues(typeof(SkillSlot));
        SkillSlot[] validSlots = allSlots.Where(s => s != SkillSlot.None).ToArray();

        for (int i = 0; i < _equipSkillDict.Values.Count; i++)
        {
            Item item = _equipSkillDict[validSlots[i]];

            if (item == null)
            {
                castSkillArr[i] = null;
                continue;
            }

            castSkillArr[i] = SkillFactory.CreateSkill(item.ID, item.Level);
        }

        return castSkillArr;
    }


    /// <summary>
    /// 딕셔너리에 키 체크해보고 없으면 딕셔너리 만들기
    /// </summary>
    private static void CheckAnd_SetDict()
    {
        SkillSlot[] allSlots = (SkillSlot[])Enum.GetValues(typeof(SkillSlot));
        SkillSlot[] validSlots = allSlots.Where(s => s != SkillSlot.None).ToArray();

        for (int i = 0; i < validSlots.Length; i++)
        {
            if (_equipSkillDict.ContainsKey(validSlots[i]) == false)
                _equipSkillDict[validSlots[i]] = null;
            else
                continue;
        }
    }
}
