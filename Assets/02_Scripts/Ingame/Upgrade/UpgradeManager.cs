using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���׷��̵� �Ŵ���
/// </summary>
public class UpgradeManager
{
    public static event Action OnUpgradeChanged; // ���׷��̵尡 ����Ǿ��� �� �̺�Ʈ
    public static int TotalPower { get; private set; }              // ���� ������
    public static int BeforeTotalPower { get; private set; }        // ���� ���� ������

    private static Dictionary<string, Upgrade> _upgradeDict;        // ���׷��̵�� ��ųʸ�

    /// <summary>
    /// ���׷��̵� ��ųʸ� ����
    /// </summary>
    public void Init(List<Upgrade> startingStatDataList)
    {
        // ��ųʸ� �ʱ�ȭ
        _upgradeDict = new Dictionary<string, Upgrade>();

        // ���׷��̵� ID�� �迭
        UpgradeID[] upgradeIDArr = (UpgradeID[])Enum.GetValues(typeof(UpgradeID));

        // ���׷��̵� ID ��ŭ �ݺ�
        foreach (UpgradeID upgradeID in upgradeIDArr)
        {
            // ���׷��̵� ID ���ڿ��� ��ȯ
            string id = upgradeID.ToString();

            // �ʱ���׷��̵� ����Ʈ�߿��� ID ��Ī�Ǵ°� ã��
            Upgrade findStatData = startingStatDataList.FirstOrDefault(x => x.ID == id);

            // null üũ
            if (findStatData == null)
            {
                Debug.Log($"�ʱ⽺�� ��ũ���ͺ� ������Ʈ���� {upgradeID}�� �ش��ϴ� �����͸� ã�� �� �����ϴ�.");
                return;
            }

            // ã�� �ʱ���׷��̵� �����ͷ� ���� ���� 
            Upgrade upgrade = new Upgrade();
            upgrade.Init(
                findStatData.ID,
                findStatData.Name,
                findStatData.Level,
                findStatData.Value,
                findStatData.ValueIncrease,
                findStatData.Cost,
                findStatData.CostIncrease
                );

            // ��ųʸ� ID �ߺ�üũ
            if (_upgradeDict.ContainsKey(id) == true)
            {
                Debug.LogWarning($"{id} �� �̹� ��ųʸ� �� �ߺ��� ���� ID �Դϴ�");
                return;
            }

            // ��ųʸ��� �߰�
            _upgradeDict.Add(id, upgrade);
        }

        UpdateTotalPower();  // ���� ������ ������Ʈ
    }

    /// <summary>
    /// Ư�� ���׷��̵� ��������
    /// </summary>
    public static Upgrade GetUpgrade(string id)
    {
        if (_upgradeDict.TryGetValue(id, out var stat))
        {
            return stat;
        }
        else
        {
            Debug.Log($"{id} ���׷��̵尡 �������� �ʽ��ϴ�.");
            return null;
        }
    }

    /// <summary>
    /// ��� ���׷��̵� ��������
    /// </summary>
    public static List<Upgrade> GetAllUpgrades()
    {
        List<Upgrade> upgradeList = new List<Upgrade>();
        upgradeList = _upgradeDict.Values.ToList();

        return upgradeList;
    }

    /// <summary>
    /// Ư�� ���׷��̵� ������ �õ�
    /// </summary>
    public static bool TryUpgradeLevelUp(string id)
    {
        Upgrade upgrade = GetUpgrade(id); // id �� �´� ���׷��̵� ��������

        if (upgrade != null && GoldManager.HasEnoughCurrency(upgrade.Cost)) // ���׷��̵尡 �ְ� + �ڱ��� �ȴٸ�
        {
            UpgradeLevelUp(id); // �� ���׷��̵� ������
            return true;
        }

        return false;
    }

    /// <summary>
    /// Ư�� ���׷��̵� ������
    /// </summary>
    public static void UpgradeLevelUp(string id)
    {
        if (_upgradeDict.TryGetValue(id.ToString(), out var upgrade))
        {
            // ��� ����
            GoldManager.ReduceCurrency(upgrade.Cost);

            // ���׷��̵� ������
            upgrade.LevelUp();
            
            // ���� ������ ������Ʈ
            UpdateTotalPower();

            // ���׷��̵� ���� �̺�Ʈ ȣ��
            OnUpgradeChanged?.Invoke();
        }
        else
        {
            Debug.Log($"{id} ���׷��̵尡 �������� �ʽ��ϴ�.");
        }
    }

    /// <summary>
    /// ���� ������ ������Ʈ
    /// </summary>
    public static void UpdateTotalPower()
    {
        BeforeTotalPower = TotalPower;

        List<int> statValueList = GetAllUpgrades().Select(stat => stat.Value).ToList();

        TotalPower = 0;
        foreach (int value in statValueList)
            TotalPower += value;
    }
}
