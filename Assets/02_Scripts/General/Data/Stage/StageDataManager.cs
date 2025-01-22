using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지 데이터 관리
/// </summary>
public class StageDataManager : SingletonBase<StageDataManager>
{
    [SerializeField] private StageDatasSO _stageDatasSO;        // 스테이지 데이터들 스크립터블 오브젝트
    private static Dictionary<int, StageData> _stageDataDict;   // 스테이지 데이터 딕셔너리

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Set_StageDataDict();
    }

    /// <summary>
    /// 스테이지 데이터 딕셔너리 셋팅
    /// </summary>
    private void Set_StageDataDict()
    {
        _stageDataDict = new Dictionary<int, StageData>();

        foreach (StageData stageData in _stageDatasSO.StageDataList)
        {
            if (_stageDataDict.ContainsKey(stageData.Stage) == false)
                _stageDataDict[stageData.Stage] = stageData;
        }
    }

    /// <summary>
    /// 해당 스테이지에 맞는 스테이지 데이터 가져오기
    /// </summary>
    public static StageData GetStageData(int stage)
    {
        if (_stageDataDict.TryGetValue(stage, out StageData stageData))
            return stageData;
        else
        {
            Debug.Log($"{stage}에 맞는 스테이지 데이터를 찾을 수 없습니다.");
            return null;
        }
    }


}
