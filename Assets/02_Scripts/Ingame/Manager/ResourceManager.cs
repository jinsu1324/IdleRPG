using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemGradeInfo
{
    public Sprite ItemGradeFrame;
    public Color ItemGradeColor;
}

public class ResourceManager : SingletonBase<ResourceManager>
{
    // 아이콘 딕셔너리
    [SerializeField]
    private Dictionary<StatType, Sprite> _statIconDict = new Dictionary<StatType, Sprite>();    

    // 아이템 등급별 정보들 딕셔너리
    [SerializeField]
    private Dictionary<ItemGrade, ItemGradeInfo> _itemGradeFrameDict = new Dictionary<ItemGrade, ItemGradeInfo>(); 

    /// <summary>
    /// 아이콘 가져오기
    /// </summary>
    public Sprite GetIcon(StatType statType)
    {
        if (_statIconDict.TryGetValue(statType, out Sprite icon))
            return icon;
        else
        {
            Debug.Log($"{statType}에 해당하는 아이콘을 찾을 수 없습니다.");
            return null;
        }
    }

    /// <summary>
    /// 아이템 등급별 프레임 가져오기
    /// </summary>
    public Sprite GetItemGradeFrame(string grade)
    {
        ItemGrade itemGrade = (ItemGrade)Enum.Parse(typeof(ItemGrade), grade);

        if (_itemGradeFrameDict.TryGetValue(itemGrade, out ItemGradeInfo itemGradeInfo))
        {
            return itemGradeInfo.ItemGradeFrame;
        }
        else
        {
            Debug.Log($"{grade}에 해당하는 프레임을 찾을 수 없습니다.");
            return null;
        }
    }

    /// <summary>
    /// 아이템 등급별 컬러 가져오기
    /// </summary>
    public Color GetItemGradeColor(string grade)
    {
        ItemGrade itemGrade = (ItemGrade)Enum.Parse(typeof(ItemGrade), grade);

        if (_itemGradeFrameDict.TryGetValue(itemGrade, out ItemGradeInfo itemGradeInfo))
        {
            return itemGradeInfo.ItemGradeColor;
        }
        else
        {
            Debug.Log($"{grade}에 해당하는 프레임을 찾을 수 없습니다.");
            return Color.white;
        }
    }
}
