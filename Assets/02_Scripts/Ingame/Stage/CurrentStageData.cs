using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지 타입
/// </summary>
public enum StageType
{
    Normal,  // 일반 스테이지
    Infinite // 무한 스테이지
}

/// <summary>
/// 현재 스테이지 데이터
/// </summary>
public class CurrentStageData : ISavable
{
    public string Key => nameof(CurrentStageData);      // Firebase 데이터 저장용 고유 키 설정
    [SaveField] private static StageType _stageType;    // 현재 스테이지 타입
    [SaveField] private static int _stage = 1;          // 현재 스테이지 (기본값 1)
    public static StageType StageType { get { return _stageType; } private set { _stageType = value; } }     
    public static int Stage { get { return _stage; } set { _stage = value; } }                   

    /// <summary>
    /// 데이터 불러오기할때 태스크들
    /// </summary>
    public void DataLoadTask()
    {
        // not yet
    }

    /// <summary>
    /// 스테이지 레벨업
    /// </summary>
    public static void StageLevelUp()
    {
        Stage++;
        QuestManager.UpdateQuestStack(QuestType.ReachStage, 1);
    }

    /// <summary>
    /// 스테이지 레벨 다운
    /// </summary>
    public static void StageLevelDown()
    {
        Stage--;

        if (Stage == 0)
            Stage = 1;
    }

    /// <summary>
    /// 스테이지 타입 일반모드로 변경
    /// </summary>
    public static void SetStageType_Normal() => StageType = StageType.Normal;

    /// <summary>
    /// 스테이지 타입 무한모드로 변경
    /// </summary>
    public static void SetStageType_Infinite() => StageType = StageType.Infinite;
}
