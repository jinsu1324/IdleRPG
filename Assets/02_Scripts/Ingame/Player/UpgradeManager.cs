using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 업그레이드 매니저
/// </summary>
public class UpgradeManager
{
    public static event Action OnUpgradeChanged; // 업그레이드가 변경되었을 때 이벤트
    public static int TotalPower { get; private set; }              // 총합 전투력
    public static int BeforeTotalPower { get; private set; }        // 이전 총합 전투력

    private static Dictionary<string, Upgrade> _upgradeDict;        // 업그레이드들 딕셔너리

    /// <summary>
    /// 업그레이드 딕셔너리 셋팅
    /// </summary>
    public void Init(List<Upgrade> startingStatDataList)
    {
        // 딕셔너리 초기화
        _upgradeDict = new Dictionary<string, Upgrade>();

        // 업그레이드 ID들 배열
        UpgradeID[] upgradeIDArr = (UpgradeID[])Enum.GetValues(typeof(UpgradeID));

        // 업그레이드 ID 만큼 반복
        foreach (UpgradeID upgradeID in upgradeIDArr)
        {
            // 업그레이드 ID 문자열로 변환
            string id = upgradeID.ToString();

            // 초기업그레이드 리스트중에서 ID 매칭되는것 찾기
            Upgrade findStatData = startingStatDataList.FirstOrDefault(x => x.ID == id);

            // null 체크
            if (findStatData == null)
            {
                Debug.Log($"초기스탯 스크립터블 오브젝트에서 {upgradeID}에 해당하는 데이터를 찾을 수 없습니다.");
                return;
            }

            // 찾은 초기업그레이드 데이터로 스탯 생성 
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

            // 딕셔너리 ID 중복체크
            if (_upgradeDict.ContainsKey(id) == true)
            {
                Debug.LogWarning($"{id} 는 이미 딕셔너리 속 중복된 스탯 ID 입니다");
                return;
            }

            // 딕셔너리에 추가
            _upgradeDict.Add(id, upgrade);
        }

        UpdateTotalPower();  // 총합 전투력 업데이트
    }

    /// <summary>
    /// 특정 업그레이드 가져오기
    /// </summary>
    public static Upgrade GetUpgrade(string id)
    {
        if (_upgradeDict.TryGetValue(id, out var stat))
        {
            return stat;
        }
        else
        {
            Debug.Log($"{id} 업그레이드가 존재하지 않습니다.");
            return null;
        }
    }

    /// <summary>
    /// 모든 업그레이드 가져오기
    /// </summary>
    public static List<Upgrade> GetAllUpgrades()
    {
        List<Upgrade> upgradeList = new List<Upgrade>();
        upgradeList = _upgradeDict.Values.ToList();

        return upgradeList;
    }

    /// <summary>
    /// 특정 업그레이드 레벨업 시도
    /// </summary>
    public static bool TryUpgradeLevelUp(string id)
    {
        Upgrade upgrade = GetUpgrade(id); // id 에 맞는 업그레이드 가져오기

        if (upgrade != null && GoldManager.HasEnoughCurrency(upgrade.Cost)) // 업그레이드가 있고 + 자금이 된다면
        {
            UpgradeLevelUp(id); // 그 업그레이드 레벨업
            return true;
        }

        return false;
    }

    /// <summary>
    /// 특정 업그레이드 레벨업
    /// </summary>
    public static void UpgradeLevelUp(string id)
    {
        if (_upgradeDict.TryGetValue(id.ToString(), out var upgrade))
        {
            // 골드 감소
            GoldManager.ReduceCurrency(upgrade.Cost);

            // 업그레이드 레벨업
            upgrade.LevelUp();
            
            // 총합 전투력 업데이트
            UpdateTotalPower();

            // 업그레이드 변경 이벤트 호출
            OnUpgradeChanged?.Invoke();
        }
        else
        {
            Debug.Log($"{id} 업그레이드가 존재하지 않습니다.");
        }
    }

    /// <summary>
    /// 총합 전투력 업데이트
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
