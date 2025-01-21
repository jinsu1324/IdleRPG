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
/// 강화 갯수 정보
/// </summary>
[System.Serializable]
public class EnhanceCountInfo
{
    public int Level;
    public int EnhanceCount;
}

[System.Serializable]
public class ItemDataSO : ScriptableObject
{
    public string ID;
    public string ItemType;
    public string Name;
    public string Grade;
    public string Desc;
    public Sprite Icon;
    public GameObject Prefab;
    public List<EnhanceCountInfo> EnhanceCountInfoList;

    /// <summary>
    /// 강화가능 갯수 가져오기
    /// </summary>
    public int GetEnhanceCount(int level)
    {
        EnhanceCountInfo enhanceCountInfo = EnhanceCountInfoList.Find(x => x.Level == level);
        
        if (enhanceCountInfo == null)
        {
            Debug.Log($"{level} 에 해당하는 강화 갯수 정보를 찾을 수 없습니다.");
            return 0;
        }

        return enhanceCountInfo.EnhanceCount;
    }
}