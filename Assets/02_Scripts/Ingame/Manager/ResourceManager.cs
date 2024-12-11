using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SingletonBase<ResourceManager>
{
    [SerializeField]
    private Dictionary<UpgradeID, Sprite> _statIconDict = new Dictionary<UpgradeID, Sprite>();    // ������ ��ųʸ�

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
}
