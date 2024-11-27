using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> _pool;         // 오브젝트를 저장할 큐
    private T _prefab;              // 생성할 프리팹
    private Transform _parent;      // 풀의 정리를 위한 부모 객체

    /// <summary>
    /// 생성자
    /// </summary>
    public ObjectPool(T prefab, int initialCount, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;
        _pool = new Queue<T>();

        // 초기 오브젝트 생성
        for (int i = 0; i < initialCount; i++)
        {
            AddObjectToPool();
        }
    }

    /// <summary>
    /// 풀에 오브젝트 추가
    /// </summary>
    private void AddObjectToPool()
    {
        T obj = GameObject.Instantiate(_prefab, _parent);
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    /// <summary>
    /// 오브젝트 가져오기
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
    /// 오브젝트 반환
    /// </summary>
    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}
