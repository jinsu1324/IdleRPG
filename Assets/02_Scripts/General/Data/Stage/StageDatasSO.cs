using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDatasSO : BaseDatasSO<StageData>
{
    // é�� - �������� ������ Ʃ�� ��ųʸ�
    private Dictionary<(int Chapter, int Stage), StageData> _stageDataDict ;

    /// <summary>
    /// �������� ������ ��ųʸ� �ʱ�ȭ
    /// </summary>
    private void SettingStageDataDict()
    {
        _stageDataDict = new Dictionary<(int, int), StageData>();

        foreach (StageData data in DataList)
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
        if (_stageDataDict == null)
            SettingStageDataDict();

        (int, int) key = (chapter, stage);
        return _stageDataDict.TryGetValue(key, out StageData data) ? data : null;
    }
}
