using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUpgradeDataManager : SingletonBase<DefaultUpgradeDataManager>
{
    [SerializeField] private DefaultUpgradeDatasSO _defaultUpgradeDatasSO;      // ����Ʈ ���׷��̵� �����͵� ��ũ���ͺ� ������Ʈ
    private static Dictionary<string, Upgrade> _defaultUpgradeDataDict;       // ����Ʈ ���׷��̵� ������ ��ųʸ�
   
    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Set_DefaultUpgradeDataDict();
    }

    /// <summary>
    /// ����Ʈ ���׷��̵� ������ ��ųʸ� ����
    /// </summary>
    public void Set_DefaultUpgradeDataDict()
    {
        _defaultUpgradeDataDict = new Dictionary<string, Upgrade>();

        foreach (Upgrade upgrade in _defaultUpgradeDatasSO.DefaultUpgradeDataList)
        {
            if (_defaultUpgradeDataDict.ContainsKey(upgrade.UpgradeStatType) == false)
                _defaultUpgradeDataDict[upgrade.UpgradeStatType] = upgrade;
        }
    }

    /// <summary>
    /// Ư�� ID �� �´� ����Ʈ ���׷��̵� ������ ��������
    /// </summary>
    public static Upgrade Get_DefaultUpgradeData(string id)
    {
        if (_defaultUpgradeDataDict.TryGetValue(id, out Upgrade upgrade))
            return upgrade;
        else
        {
            Debug.Log($"{id}�� �´� ����Ʈ ���׷��̵� �����͸� ã�� �� �����ϴ�.");
            return null;
        }
    }
}
