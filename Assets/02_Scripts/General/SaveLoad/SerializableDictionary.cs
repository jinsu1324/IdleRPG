using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>(); // Ű ����Ʈ

    [SerializeField]
    private List<TValue> values = new List<TValue>(); // �� ����Ʈ

    private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

    /// <summary>
    /// ����ȭ ���� ȣ��: Dictionary �����͸� keys�� values ����Ʈ�� ��ȯ
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
    /// ������ȭ �� ȣ��: keys�� values ����Ʈ�� Dictionary�� ��ȯ
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
    /// Ű�� �߰�
    /// </summary>
    public void Add(TKey key, TValue value)
    {
        dictionary.Add(key, value);
    }

    /// <summary>
    /// Ű�� �� ��������/�����ϱ�
    /// </summary>
    public TValue this[TKey key]
    {
        get => dictionary[key];
        set => dictionary[key] = value;
    }

    /// <summary>
    /// Dictionary ���·� ��ȯ
    /// </summary>
    public Dictionary<TKey, TValue> ToDictionary()
    {
        return new Dictionary<TKey, TValue>(dictionary);
    }

    /// <summary>
    /// Dictionary���� Ű�� �ִ��� Ȯ��
    /// </summary>
    public bool ContainsKey(TKey key)
    {
        return dictionary.ContainsKey(key);
    }

    /// <summary>
    /// Ű ����
    /// </summary>
    public bool Remove(TKey key)
    {
        return dictionary.Remove(key);
    }

    /// <summary>
    /// Ű�� ���� �������ų� ����Ʈ�� ��ȯ
    /// </summary>
    public bool TryGetValue(TKey key, out TValue value)
    {
        return dictionary.TryGetValue(key, out value);
    }
}
