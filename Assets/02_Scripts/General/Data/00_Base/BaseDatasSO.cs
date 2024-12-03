using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseDatasSO<Data> : ScriptableObject where Data : BaseData
{
    public List<Data> DataList = new List<Data>();        // ������ ����Ʈ
    private Dictionary<string, Data> _dataDict;           // ����Ʈ�� ��ųʸ��� ������ ����

    /// <summary>
    /// ��ųʸ� �ʱ�ȭ
    /// </summary>
    public void InitDictionary()
    {
        _dataDict = new Dictionary<string, Data>();

        foreach (var data in DataList)
            _dataDict[data.ID] = data;
    }

    /// <summary>
    /// ID�� ���� ������ ��ȯ
    /// </summary>
    public Data GetDataByID(string id)
    {
        // ��ųʸ� ���� �ʱ�ȭ �������� �ʱ�ȭ
        if (_dataDict == null)
            InitDictionary();

        // id�� �ش��ϴ� �����Ͱ� ������ ��ȯ
        if (_dataDict.TryGetValue(id, out var data))
            return data;

        Debug.LogWarning($"{id} �����͸� ã�� �� �����ϴ�.");
        return null;
    }

    /// <summary>
    /// Ư�� ID�� �ش��ϴ� ��� �����͸� ����Ʈ�� ��ȯ
    /// </summary>
    public List<Data> GetAllDataByID(string id)
    {
        return DataList.Where(data => data.ID == id).ToList();
    }
}
