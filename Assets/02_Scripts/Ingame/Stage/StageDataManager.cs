using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataManager
{
    // 챕터 - 스테이지 데이터 튜플 딕셔너리
    private Dictionary<(int Chapter, int Stage), StageData> _stageDataDict = new Dictionary<(int, int), StageData>();

    /// <summary>
    /// 스테이지 데이터 딕셔너리 초기화
    /// </summary>
    public void Initialize()
    {
        foreach (StageData data in DataManager.Instance.StageDatasSO.DataList)
        {
            (int, int) key = (data.Chapter, data.Stage);
            _stageDataDict[key] = data;
        }
    }

    /// <summary>
    /// 챕터와 스테이지에 따라 스테이지데이터 가져오기
    /// </summary>
    public StageData GetStageData(int chapter, int stage)
    {
        (int, int) key = (chapter, stage);
        return _stageDataDict.TryGetValue(key, out StageData data) ? data : null;
    }
}
