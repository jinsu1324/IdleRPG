using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingletonBase<ResourceManager>
{
    [SerializeField]
    private Dictionary<StatID, Sprite> _statIconDict = new Dictionary<StatID, Sprite>();    // 아이콘 딕셔너리

    /// <summary>
    /// 아이콘 가져오기
    /// </summary>
    public Sprite GetIcon(string id)
    {
        StatID statID = (StatID)Enum.Parse(typeof(StatID), id);

        if (_statIconDict.TryGetValue(statID, out Sprite icon))
        {
            return icon;
        }
        else
        {
            Debug.Log($"{id}에 해당하는 아이콘을 찾을 수 없습니다.");
            return null;
        }
    }
}
