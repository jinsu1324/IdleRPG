using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpgradeDataManager : SingletonBase<StartUpgradeDataManager>
{
    [SerializeField] private StartUpgradeDatasSO _startUpgradeDatasSO;      // ��ŸƮ ���׷��̵� �����͵� ��ũ���ͺ� ������Ʈ
    private static Dictionary<string, Upgrade> _startUpgradeDataDict;       // ��ŸƮ ���׷��̵� ������ ��ųʸ�
   
    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Set_StartUpgradeDataDict();
    }

    /// <summary>
    /// ��ŸƮ ���׷��̵� ������ ��ųʸ� ����
    /// </summary>
    public void Set_StartUpgradeDataDict()
    {
        _startUpgradeDataDict = new Dictionary<string, Upgrade>();

        foreach (Upgrade upgrade in _startUpgradeDatasSO.StartUpgradeDataList)
        {
            if (_startUpgradeDataDict.ContainsKey(upgrade.ID) == false)
                _startUpgradeDataDict[upgrade.ID] = upgrade;
        }
    }

    /// <summary>
    /// Ư�� ID �� �´� ��Ÿ�� ���׷��̵� ������ ��������
    /// </summary>
    public static Upgrade GetStartUpgradeData(string id)
    {
        if (_startUpgradeDataDict.TryGetValue(id, out Upgrade upgrade))
            return upgrade;
        else
        {
            Debug.Log($"{id}�� �´� ��Ÿ�� ���׷��̵� �����͸� ã�� �� �����ϴ�.");
            return null;
        }
    }
}
