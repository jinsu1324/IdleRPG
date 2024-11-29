using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataManager
{
    // é�� - �������� ������ Ʃ�� ��ųʸ�
    private Dictionary<(int Chapter, int Stage), StageData> _stageDataDict = new Dictionary<(int, int), StageData>();

    /// <summary>
    /// �������� ������ ��ųʸ� �ʱ�ȭ
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
    /// é�Ϳ� ���������� ���� �������������� ��������
    /// </summary>
    public StageData GetStageData(int chapter, int stage)
    {
        (int, int) key = (chapter, stage);
        return _stageDataDict.TryGetValue(key, out StageData data) ? data : null;
    }
}
