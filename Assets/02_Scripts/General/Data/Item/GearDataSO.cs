using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공격 애니메이션 타입
/// </summary>
public enum AttackAnimType
{
    Hand = 0,
    Melee = 1,
    Magic = 2,
}

/// <summary>
/// 장비 아이템 스크립터블 오브젝트
/// </summary>
[System.Serializable]
public class GearDataSO : ItemDataSO
{
    public string AttackAnimType;
    public GameObject Prefab;

    /// <summary>
    /// 원하는 레벨에 맞는 어빌리티들 딕셔너리로 가져오기
    /// </summary>
    public Dictionary<StatType, int> GetAbilityDict_ByLevel(int level)
    {
        // 중복된 데이터를 get하지 않게 새 딕셔너리 생성
        Dictionary<StatType, int> abilityDict = new Dictionary<StatType, int>();

        // level에 맞는 itemLevelInfo 찾기
        ItemLevelInfo itemLevelInfo = ItemLevelInfoList.Find(x => x.Level == level.ToString());

        // 없으면 그냥 리턴
        if (itemLevelInfo == null)
        {
            Debug.Log($"{level}에 해당하는 ItemLevelInfo를 찾지 못했습니다.");
            return abilityDict;
        }

        // 해당 레벨의 itemAbility들을 딕셔너리에 넣기
        foreach (ItemAbility itemAbility in itemLevelInfo.ItemAbilityList)
        {
            StatType statType = (StatType)Enum.Parse(typeof(StatType), itemAbility.AbilityType);
            int value = int.Parse(itemAbility.AbilityValue);

            abilityDict.Add(statType, value);
        }

        return abilityDict;
    }
}
