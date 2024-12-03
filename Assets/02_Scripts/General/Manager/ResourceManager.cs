using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : SerializedMonoBehaviour
{
    public static ResourceManager Instance { get; private set; }                            // �̱��� �ν��Ͻ�

    [SerializeField]
    private Dictionary<StatID, Sprite> _statIconDict = new Dictionary<StatID, Sprite>();    // ������ ��ųʸ�

    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ������ ��������
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
            Debug.Log($"{id}�� �ش��ϴ� �������� ã�� �� �����ϴ�.");
            return null;
        }
    }
}
