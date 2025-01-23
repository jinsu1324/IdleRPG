using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���׷��̵� �Ŵ���
/// </summary>
public class UpgradeManager : ISavable
{
    public string Key => nameof(UpgradeManager); // Firebase ������ ����� ���� Ű ����

    [SaveField]
    private static Dictionary<StatType, Upgrade> _currentUpgradeDict = new Dictionary<StatType, Upgrade>() // ���� �� ���׷��̵��      
    {
        { StatType.AttackPower, new Upgrade()},
        { StatType.AttackSpeed, new Upgrade()},
        { StatType.MaxHp, new Upgrade()},
        { StatType.CriticalRate, new Upgrade()},
        { StatType.CriticalMultiple, new Upgrade()},
    };

    /// <summary>
    /// ����Ÿ�Կ� �´� ���� ���׷��̵� ��������
    /// </summary>
    public static Upgrade GetCurrentUpgrade(StatType statType)
    {
        if (_currentUpgradeDict.TryGetValue(statType, out Upgrade upgrade))
            return upgrade;
        else
        {
            Debug.Log($"{statType} �� �´� ���׷��̵带 ã�� �� �����ϴ�.");
            return null;
        }
    }

    /// <summary>
    /// ���׷��̵� ������ �õ�
    /// </summary>
    public static bool Try_UpgradeLevelUp(StatType statType)
    {
        Upgrade upgrade = GetCurrentUpgrade(statType);

        if (upgrade == null)
            return false;

        if (GoldManager.HasEnoughGold(upgrade.Cost))
        {
            UpgradeLevelUp(upgrade);
            return true;
        }

        return false;
    }

    /// <summary>
    /// ���׷��̵� ������
    /// </summary>
    public static void UpgradeLevelUp(Upgrade upgrade)
    {
        GoldManager.ReduceGold(upgrade.Cost);   // ��� ����
        upgrade.LevelUp();                      // ���׷��̵� ������
        FXManager.Instance.SpawnFX(FXName.FX_Player_Upgrade, PlayerManager.GetPlayerInstancePos()); // �ʵ忡 �ִ� �÷��̾� ��ġ�� ����Ʈ ���
    }

    /// <summary>
    /// ���� ���׷��̵�� �ʱⰪ���� ����
    /// </summary>
    public static void SetUpgrades_ByDefualt()
    {
        foreach (var kvp in _currentUpgradeDict)
        {
            StatType statType = kvp.Key;

            Upgrade startData = DefaultUpgradeDataManager.Get_DefaultUpgradeData(statType.ToString());

            if (startData == null)
                Debug.Log($"{statType}�� �´� ����Ʈ �����͸� ã�� �� �����ϴ�.");

            if (_currentUpgradeDict.TryGetValue(statType, out Upgrade upgrade))
            {
                upgrade.Init(startData.UpgradeStatType,
                         startData.Name,
                         startData.Level,
                         startData.Value,
                         startData.ValueIncrease,
                         startData.Cost,
                         startData.CostIncrease);
            }
            else
                Debug.Log($"{statType} ��ųʸ��� ã�� �� �����ϴ�.");
        }
    }
}
