using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUpgradeDataManager : SingletonBase<DefaultUpgradeDataManager>
{
    [SerializeField] private DefaultUpgradeDatasSO _defaultUpgradeDatasSO;      // 디폴트 업그레이드 데이터들 스크립터블 오브젝트
    private static Dictionary<string, Upgrade> _defaultUpgradeDataDict;       // 디폴트 업그레이드 데이터 딕셔너리
   
    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Set_DefaultUpgradeDataDict();
    }

    /// <summary>
    /// 디폴트 업그레이드 데이터 딕셔너리 셋팅
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
    /// 특정 ID 에 맞는 디폴트 업그레이드 데이터 가져오기
    /// </summary>
    public static Upgrade Get_DefaultUpgradeData(string id)
    {
        if (_defaultUpgradeDataDict.TryGetValue(id, out Upgrade upgrade))
            return upgrade;
        else
        {
            Debug.Log($"{id}에 맞는 디폴트 업그레이드 데이터를 찾을 수 없습니다.");
            return null;
        }
    }
}
