using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 등급
/// </summary>
public enum ItemGrade
{
    Normal,
    Rare,
    Legendary
}

/// <summary>
/// 아이템 타입
/// </summary>
public enum ItemType
{
    Weapon,
    Armor,
    Helmet,
    Skill
}

/// <summary>
/// 아이템들 부모 스크립터블 오브젝트
/// </summary>
[System.Serializable]
public class ItemDataSO : ScriptableObject
{
    public string ID;
    public string ItemType;
    public string Name;
    public string Grade;
    public List<ItemLevelInfo> ItemLevelInfoList;

    /// <summary>
    /// 원하는 레벨에 맞는 어빌리티들 딕셔너리로 가져오기
    /// </summary>
    public Dictionary<StatType, int> GetAbilityDict_ByLevel(int level)
    {
        // 중복된 데이터를 get하지 않게 새 딕셔너리 생성
        Dictionary<StatType, int> statDict = new Dictionary<StatType, int>();

        // level에 맞는 itemLevelInfo 찾기
        ItemLevelInfo itemLevelInfo = ItemLevelInfoList.Find(x => x.Level == level.ToString());

        // 없으면 그냥 리턴
        if (itemLevelInfo == null)
        {
            Debug.Log($"{level}에 해당하는 upgradeInfo를 찾지 못했습니다.");
            return statDict;
        }

        // 해당 레벨의 itemAbility들을 딕셔너리에 넣기
        foreach (ItemAbility itemAbility in itemLevelInfo.ItemAbilityList)
        {
            StatType statType = (StatType)Enum.Parse(typeof(StatType), itemAbility.AbilityType);
            int value = int.Parse(itemAbility.AbilityValue);

            statDict.Add(statType, value);
        }

        return statDict;
    }
}

/// <summary>
/// 아이템 레벨과 그에맞는 정보들(어빌리티 등)
/// </summary>
[System.Serializable]
public class ItemLevelInfo
{
    public string Level;                        // 레벨
    public List<ItemAbility> ItemAbilityList;   // 가지고 있는 어빌리티들 리스트
}

/// <summary>
/// 아이템 어빌리티
/// </summary>
[System.Serializable]
public class ItemAbility
{
    public string AbilityType;
    public string AbilityValue;
}