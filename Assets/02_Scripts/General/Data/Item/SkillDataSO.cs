using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스킬에 있는 속성 타입
/// </summary>
public enum SkillAttributeType
{
    AttackPercentage,
    Delay,
    Range,
    SplashRadius,
    AddAttackSpeed,
    Duration,
    ProjectileCount
}

/// <summary>
/// 스킬 스크립터블 오브젝트 (+스킬아이템)
/// </summary>
[System.Serializable]
public class SkillDataSO : ItemDataSO
{
    /// <summary>
    /// 레벨에 맞는 속성들을 딕셔너리로 가져오기
    /// </summary>
    public Dictionary<SkillAttributeType, int> GetAttributeDict_ByLevel(int level)
    {
        // 중복된 데이터를 get하지 않게 새 딕셔너리 생성
        Dictionary<SkillAttributeType, int> attributeDict = new Dictionary<SkillAttributeType, int>();

        // level에 맞는 levelAttributes 찾기
        LevelAttributes levelAttributes = LevelAttributesList.Find(x => x.Level == level.ToString());

        // 없으면 그냥 리턴
        if (levelAttributes == null)
        {
            Debug.Log($"{level}이 맞는 levelAttributes를 찾지 못했습니다.");
            return attributeDict;
        }

        // 해당 레벨에 존재하는 속성(attribute)들을 딕셔너리에 넣고 반환
        foreach (Attribute attribute in levelAttributes.AttributeList)
        {
            SkillAttributeType statType = (SkillAttributeType)Enum.Parse(typeof(SkillAttributeType), attribute.Type);
            int value = int.Parse(attribute.Value);

            attributeDict.Add(statType, value);
        }

        return attributeDict;
    }

    /// <summary>
    /// 특정 속성의 값을 반환
    /// </summary>
    public string GetAttributeValue(SkillAttributeType skillAttributeType, int level)
    {
        // level에 맞는 levelAttributes 찾기
        LevelAttributes levelAttributes = LevelAttributesList.Find(x => x.Level == level.ToString());

        // 속성중에서 타입이름이 같은 속성을 찾기
        Attribute attribute = levelAttributes.AttributeList.Find(x => x.Type == skillAttributeType.ToString());

        // 그 속성의 값을 반환
        return attribute.Value;
    }
}
