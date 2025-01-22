using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������� ������ ����
/// </summary>
public class StageDataManager : SingletonBase<StageDataManager>
{
    [SerializeField] private StageDatasSO _stageDatasSO;        // �������� �����͵� ��ũ���ͺ� ������Ʈ
    private static Dictionary<int, StageData> _stageDataDict;   // �������� ������ ��ųʸ�

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Set_StageDataDict();
    }

    /// <summary>
    /// �������� ������ ��ųʸ� ����
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
    /// �ش� ���������� �´� �������� ������ ��������
    /// </summary>
    public static StageData GetStageData(int stage)
    {
        if (_stageDataDict.TryGetValue(stage, out StageData stageData))
            return stageData;
        else
        {
            Debug.Log($"{stage}�� �´� �������� �����͸� ã�� �� �����ϴ�.");
            return null;
        }
    }


}
