using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>(); // 키 리스트

    [SerializeField]
    private List<TValue> values = new List<TValue>(); // 값 리스트

    private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

    /// <summary>
    /// 직렬화 전에 호출: Dictionary 데이터를 keys와 values 리스트로 변환
    /// </summary>
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();

        foreach (var kvp in dictionary)
        {
            keys.Add(kvp.Key);
            values.Add(kvp.Value);
        }
    }

    /// <summary>
    /// 역직렬화 후 호출: keys와 values 리스트를 Dictionary로 변환
    /// </summary>
    public void OnAfterDeserialize()
    {
        dictionary.Clear();

        if (keys.Count != values.Count)
        {
            Debug.LogError($"SerializableDictionary: Keys and values count mismatch. Keys: {keys.Count}, Values: {values.Count}");
            return;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            dictionary[keys[i]] = values[i];
        }
    }

    /// <summary>
    /// 키를 추가
    /// </summary>
    public void Add(TKey key, TValue value)
    {
        dictionary.Add(key, value);
    }

    /// <summary>
    /// 키로 값 가져오기/설정하기
    /// </summary>
    public TValue this[TKey key]
    {
        get => dictionary[key];
        set => dictionary[key] = value;
    }

    /// <summary>
    /// Dictionary 형태로 반환
    /// </summary>
    public Dictionary<TKey, TValue> ToDictionary()
    {
        return new Dictionary<TKey, TValue>(dictionary);
    }

    /// <summary>
    /// Dictionary에서 키가 있는지 확인
    /// </summary>
    public bool ContainsKey(TKey key)
    {
        return dictionary.ContainsKey(key);
    }

    /// <summary>
    /// 키 제거
    /// </summary>
    public bool Remove(TKey key)
    {
        return dictionary.Remove(key);
    }

    /// <summary>
    /// 키의 값을 가져오거나 디폴트를 반환
    /// </summary>
    public bool TryGetValue(TKey key, out TValue value)
    {
        return dictionary.TryGetValue(key, out value);
    }
}
