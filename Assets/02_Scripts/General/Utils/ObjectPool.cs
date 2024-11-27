using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> _pool;         // ������Ʈ�� ������ ť
    private T _prefab;              // ������ ������
    private Transform _parent;      // Ǯ�� ������ ���� �θ� ��ü

    /// <summary>
    /// ������
    /// </summary>
    public ObjectPool(T prefab, int initialCount, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;
        _pool = new Queue<T>();

        // �ʱ� ������Ʈ ����
        for (int i = 0; i < initialCount; i++)
        {
            AddObjectToPool();
        }
    }

    /// <summary>
    /// Ǯ�� ������Ʈ �߰�
    /// </summary>
    private void AddObjectToPool()
    {
        T obj = GameObject.Instantiate(_prefab, _parent);
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    /// <summary>
    /// ������Ʈ ��������
    /// </summary>
    public T GetObject()
    {
        if (_pool.Count == 0)
            AddObjectToPool();

        T obj = _pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    /// <summary>
    /// ������Ʈ ��ȯ
    /// </summary>
    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}
