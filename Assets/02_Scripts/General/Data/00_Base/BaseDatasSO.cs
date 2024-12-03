using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseDatasSO<Data> : ScriptableObject where Data : BaseData
{
    public List<Data> DataList = new List<Data>();        // 데이터 리스트
    private Dictionary<string, Data> _dataDict;           // 리스트를 딕셔너리로 저장할 변수

    /// <summary>
    /// 딕셔너리 초기화
    /// </summary>
    public void InitDictionary()
    {
        _dataDict = new Dictionary<string, Data>();

        foreach (var data in DataList)
            _dataDict[data.ID] = data;
    }

    /// <summary>
    /// ID에 따라 데이터 반환
    /// </summary>
    public Data GetDataByID(string id)
    {
        // 딕셔너리 아직 초기화 안했으면 초기화
        if (_dataDict == null)
            InitDictionary();

        // id에 해당하는 데이터가 있으면 반환
        if (_dataDict.TryGetValue(id, out var data))
            return data;

        Debug.LogWarning($"{id} 데이터를 찾을 수 없습니다.");
        return null;
    }

    /// <summary>
    /// 특정 ID에 해당하는 모든 데이터를 리스트로 반환
    /// </summary>
    public List<Data> GetAllDataByID(string id)
    {
        return DataList.Where(data => data.ID == id).ToList();
    }
}
