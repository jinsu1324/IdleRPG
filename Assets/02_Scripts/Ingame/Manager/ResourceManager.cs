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
    // ������ ��ųʸ�
    [SerializeField]
    private Dictionary<UpgradeID, Sprite> _statIconDict = new Dictionary<UpgradeID, Sprite>();    

    // ������ ��޺� ������ ��ųʸ�
    [SerializeField]
    private Dictionary<ItemGrade, ItemGradeInfo> _itemGradeFrameDict = new Dictionary<ItemGrade, ItemGradeInfo>(); 

    /// <summary>
    /// ������ ��������
    /// </summary>
    public Sprite GetIcon(string id)
    {
        UpgradeID statID = (UpgradeID)Enum.Parse(typeof(UpgradeID), id);

        if (_statIconDict.TryGetValue(statID, out Sprite icon))
        {
            return icon;
        }
        else
        {
            Debug.Log($"{id}�� �ش��ϴ� �������� ã�� �� �����ϴ�.");
            return null;
        }
    }

    /// <summary>
    /// ������ ��޺� ������ ��������
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
            Debug.Log($"{grade}�� �ش��ϴ� �������� ã�� �� �����ϴ�.");
            return null;
        }
    }

    /// <summary>
    /// ������ ��޺� �÷� ��������
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
            Debug.Log($"{grade}�� �ش��ϴ� �������� ã�� �� �����ϴ�.");
            return Color.white;
        }
    }
}
