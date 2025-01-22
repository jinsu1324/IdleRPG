using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDataManager : SingletonBase<QuestDataManager>
{
    [SerializeField] private QuestDatasSO _questDatasSO;        // ����Ʈ �����͵� ��ũ���ͺ� ������Ʈ
    private static Dictionary<int, QuestData> _questDataDict;   // ����Ʈ ������ ��ųʸ�

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Set_QuestDataDict();
    }

    /// <summary>
    /// ����Ʈ ������ ��ųʸ� ����
    /// </summary>
    public void Set_QuestDataDict()
    {
        _questDataDict = new Dictionary<int, QuestData>();

        foreach (QuestData questData in _questDatasSO.QuestDataList)
        {
            if (_questDataDict.ContainsKey(questData.Index) == false)
                _questDataDict[questData.Index] = questData;
        }
    }

    /// <summary>
    /// �ش� �ε����� �´� ����Ʈ ������ ��������
    /// </summary>
    public static QuestData GetQuestData(int index)
    {
        if (_questDataDict.TryGetValue(index, out QuestData questData))
            return questData;
        else
        {
            Debug.Log($"{index}�� �´� ����Ʈ �����͸� ã�� �� �����ϴ�.");
            return null;
        }
    }
}
