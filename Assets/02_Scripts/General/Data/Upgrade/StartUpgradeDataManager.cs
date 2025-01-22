using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpgradeDataManager : SingletonBase<StartUpgradeDataManager>
{
    [SerializeField] private StartUpgradeDatasSO _startUpgradeDatasSO;      // 스타트 업그레이드 데이터들 스크립터블 오브젝트
    private static Dictionary<string, Upgrade> _startUpgradeDataDict;       // 스타트 업그레이드 데이터 딕셔너리
   
    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Set_StartUpgradeDataDict();
    }

    /// <summary>
    /// 스타트 업그레이드 데이터 딕셔너리 셋팅
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
    /// 특정 ID 에 맞는 스타팅 업그레이드 데이터 가져오기
    /// </summary>
    public static Upgrade GetStartUpgradeData(string id)
    {
        if (_startUpgradeDataDict.TryGetValue(id, out Upgrade upgrade))
            return upgrade;
        else
        {
            Debug.Log($"{id}에 맞는 스타팅 업그레이드 데이터를 찾을 수 없습니다.");
            return null;
        }
    }
}
