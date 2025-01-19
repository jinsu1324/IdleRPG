using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���
/// </summary>
public enum ItemGrade
{
    Normal,
    Rare,
    Legendary
}

/// <summary>
/// ������ Ÿ��
/// </summary>
public enum ItemType
{
    Weapon,
    Armor,
    Helmet,
    Skill
}

/// <summary>
/// ��ȭ ���� ����
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
    /// ��ȭ���� ���� ��������
    /// </summary>
    public int GetEnhanceCount(int level)
    {
        EnhanceCountInfo enhanceCountInfo = EnhanceCountInfoList.Find(x => x.Level == level);
        
        if (enhanceCountInfo == null)
        {
            Debug.Log($"{level} �� �ش��ϴ� ��ȭ ���� ������ ã�� �� �����ϴ�.");
            return 0;
        }

        return enhanceCountInfo.EnhanceCount;
    }
}