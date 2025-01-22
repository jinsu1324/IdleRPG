using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDataManager : SingletonBase<QuestDataManager>
{
    [SerializeField] private QuestDatasSO _questDatasSO;        // 퀘스트 데이터들 스크립터블 오브젝트
    private static Dictionary<int, QuestData> _questDataDict;   // 퀘스트 데이터 딕셔너리

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Set_QuestDataDict();
    }

    /// <summary>
    /// 퀘스트 데이터 딕셔너리 셋팅
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
    /// 해당 인덱스에 맞는 퀘스트 데이터 가져오기
    /// </summary>
    public static QuestData GetQuestData(int index)
    {
        if (_questDataDict.TryGetValue(index, out QuestData questData))
            return questData;
        else
        {
            Debug.Log($"{index}에 맞는 퀘스트 데이터를 찾을 수 없습니다.");
            return null;
        }
    }
}
