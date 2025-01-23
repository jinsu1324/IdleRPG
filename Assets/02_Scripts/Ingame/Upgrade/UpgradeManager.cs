using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 업그레이드 매니저
/// </summary>
public class UpgradeManager : ISavable
{
    public string Key => nameof(UpgradeManager); // Firebase 데이터 저장용 고유 키 설정

    [SaveField]
    private static Dictionary<StatType, Upgrade> _currentUpgradeDict = new Dictionary<StatType, Upgrade>() // 현재 내 업그레이드들      
    {
        { StatType.AttackPower, new Upgrade()},
        { StatType.AttackSpeed, new Upgrade()},
        { StatType.MaxHp, new Upgrade()},
        { StatType.CriticalRate, new Upgrade()},
        { StatType.CriticalMultiple, new Upgrade()},
    };

    /// <summary>
    /// 스탯타입에 맞는 현재 업그레이드 가져오기
    /// </summary>
    public static Upgrade GetCurrentUpgrade(StatType statType)
    {
        if (_currentUpgradeDict.TryGetValue(statType, out Upgrade upgrade))
            return upgrade;
        else
        {
            Debug.Log($"{statType} 에 맞는 업그레이드를 찾을 수 없습니다.");
            return null;
        }
    }

    /// <summary>
    /// 업그레이드 레벨업 시도
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
    /// 업그레이드 레벨업
    /// </summary>
    public static void UpgradeLevelUp(Upgrade upgrade)
    {
        GoldManager.ReduceGold(upgrade.Cost);   // 골드 감소
        upgrade.LevelUp();                      // 업그레이드 레벨업
        FXManager.Instance.SpawnFX(FXName.FX_Player_Upgrade, PlayerManager.GetPlayerInstancePos()); // 필드에 있는 플레이어 위치에 이펙트 재생
    }

    /// <summary>
    /// 현재 업그레이드들 초기값으로 셋팅
    /// </summary>
    public static void SetUpgrades_ByDefualt()
    {
        foreach (var kvp in _currentUpgradeDict)
        {
            StatType statType = kvp.Key;

            Upgrade startData = DefaultUpgradeDataManager.Get_DefaultUpgradeData(statType.ToString());

            if (startData == null)
                Debug.Log($"{statType}에 맞는 디폴트 데이터를 찾을 수 없습니다.");

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
                Debug.Log($"{statType} 딕셔너리를 찾을 수 없습니다.");
        }
    }
}
