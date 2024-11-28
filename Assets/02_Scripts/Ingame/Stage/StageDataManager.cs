using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataManager
{
    private Dictionary<(int Chapter, int Stage), StageData> _stageDataDict;     // é�� - �������� ������ Ʃ�� ��ųʸ�

    /// <summary>
    /// �������� ������ ��ųʸ� �ʱ�ȭ
    /// </summary>
    public void Initialize(StageDatasSO stageDatasSO)
    {
        _stageDataDict = new Dictionary<(int, int), StageData>();
        foreach (StageData data in stageDatasSO.DataList)
        {
            (int, int) key = (data.Chapter, data.Stage);
            _stageDataDict[key] = data;
        }
    }

    /// <summary>
    /// é�Ϳ� ���������� ���� �������������� ��������
    /// </summary>
    public StageData GetStageData(int chapter, int stage)
    {
        (int, int) key = (chapter, stage);
        return _stageDataDict.TryGetValue(key, out StageData data) ? data : null;
    }
}
