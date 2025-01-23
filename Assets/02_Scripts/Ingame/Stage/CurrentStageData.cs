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
public class CurrentStageData// : ISavable
{
    public string Key => nameof(CurrentStageData);                          // Firebase ������ ����� ���� Ű ����
    [SaveField] public static StageType StageType { get; private set; }     // ���� �������� Ÿ��
    [SaveField] private static int _stage;                                  // ���� �������� 
    public static int Stage
    {
        get
        {
            if (_stage == 0)
            {
                _stage = 1;
                return _stage;
            }
            else 
                return _stage;
        }
        private set 
        { 
            _stage = value; 
        }
    }

    /// <summary>
    /// �������� ������
    /// </summary>
    public static void StageLevelUp()
    {
        Stage++;
        QuestManager.Instance.UpdateQuestProgress(QuestType.ReachStage, 1);
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
