using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� Ÿ��
/// </summary>
public enum StageType
{
    Normal,  // �Ϲ� ��������
    Infinite // ���� ��������
}

/// <summary>
/// ���� �������� ������
/// </summary>
public class CurrentStageData : ISavable
{
    public string Key => nameof(CurrentStageData);      // Firebase ������ ����� ���� Ű ����
    [SaveField] private static StageType _stageType;    // ���� �������� Ÿ��
    [SaveField] private static int _stage = 1;          // ���� �������� (�⺻�� 1)
    public static StageType StageType { get { return _stageType; } private set { _stageType = value; } }     
    public static int Stage { get { return _stage; } set { _stage = value; } }                   

    /// <summary>
    /// ������ �ҷ������Ҷ� �½�ũ��
    /// </summary>
    public void DataLoadTask()
    {
        // not yet
    }

    /// <summary>
    /// �������� ������
    /// </summary>
    public static void StageLevelUp()
    {
        Stage++;
        QuestManager.UpdateQuestStack(QuestType.ReachStage, 1);
    }

    /// <summary>
    /// �������� ���� �ٿ�
    /// </summary>
    public static void StageLevelDown()
    {
        Stage--;

        if (Stage == 0)
            Stage = 1;
    }

    /// <summary>
    /// �������� Ÿ�� �Ϲݸ��� ����
    /// </summary>
    public static void SetStageType_Normal() => StageType = StageType.Normal;

    /// <summary>
    /// �������� Ÿ�� ���Ѹ��� ����
    /// </summary>
    public static void SetStageType_Infinite() => StageType = StageType.Infinite;
}
