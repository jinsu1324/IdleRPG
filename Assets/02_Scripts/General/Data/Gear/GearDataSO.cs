using System.Collections.Generic;

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
/// 장비 속성
/// </summary>
[System.Serializable]
public class GearAttribute
{
    public string Type;
    public string Value;
}

/// <summary>
/// 장비 속성들 정보
/// </summary>
[System.Serializable]
public class GearAttributesInfo
{
    public int Level;
    public List<GearAttribute> GearAttributeList;
}

/// <summary>
/// 장비 스크립터블 오브젝트
/// </summary>
[System.Serializable]
public class GearDataSO : ItemDataSO
{
    public string AttackAnimType;
    public List<GearAttributesInfo> GearAttributesInfoList;









    

    ///// <summary>
    ///// 레벨에 맞는 속성들을 딕셔너리로 가져오기
    ///// </summary>
    //public Dictionary<StatType, float> GetAttributeDict_ByLevel(int level)
    //{
    //    // 중복된 데이터를 get하지 않게 새 딕셔너리 생성
    //    Dictionary<StatType, float> attributeDict = new Dictionary<StatType, float>();

    //    // level에 맞는 levelAttributes 찾기
    //    LevelAttributes levelAttributes = LevelAttributesList.Find(x => x.Level == level.ToString());

    //    // 없으면 그냥 리턴
    //    if (levelAttributes == null)
    //    {
    //        Debug.Log($"{level}이 맞는 levelAttributes를 찾지 못했습니다.");
    //        return attributeDict;
    //    }

    //    // 해당 레벨에 존재하는 속성(attribute)들을 딕셔너리에 넣고 반환
    //    foreach (Attribute attribute in levelAttributes.AttributeList)
    //    {
    //        StatType statType = (StatType)Enum.Parse(typeof(StatType), attribute.Type);
    //        float value = float.Parse(attribute.Value);

    //        attributeDict.Add(statType, value);
    //    }

    //    return attributeDict;
    //}
}



