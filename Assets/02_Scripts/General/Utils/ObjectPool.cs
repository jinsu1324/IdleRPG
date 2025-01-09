using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트 풀링
/// </summary>
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private ObjectPoolObj _prefab;  // 오브젝트 풀에 생성할 프리팹
    [SerializeField] private int _count = 10;        // 한번에 생성할 갯수

    /// <summary>
    /// Awake
    /// </summary>
    protected void Awake()
    {
        CreateObjs(); // 풀에 오브젝트 생성
    }

    /// <summary>
    /// 풀에 오브젝트 생성
    /// </summary>
    private void CreateObjs()
    {
        for (int i = 0; i < _count; i++)
        {
            ObjectPoolObj obj = Instantiate(_prefab, transform);
            obj.Init(transform);
            obj.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 오브젝트 사용할때 가져오기
    /// </summary>
    public GameObject GetObj()
    {
        // 만약 풀에 자식이 없다면, 새로운 오브젝트 생성
        if (transform.childCount <= 0)
            CreateObjs();

        // 첫 번째 자식부터 확인
        int count = 0;
        GameObject child = transform.GetChild(count).gameObject;

        // 만약 확인한 자식이 활성회되었다면, 다음자식으로 넘어가서 비활성화된 오브젝트를 찾음
        while (child.activeInHierarchy)
        {
            // 다음 자식으로 이동
            count++; 

            // 모든 자식을 확인했지만 사용할 오브젝트가 없으면, 새로운 오브젝트 생성
            if (count >= transform.childCount)
                CreateObjs();

            // 다시 현재 인덱스(count)의 자식을 가져옴
            child = transform.GetChild(count).gameObject;
        }

        // 채택된 자식을 활성화 (스폰)
        child.GetComponent<ObjectPoolObj>().Spawn();

        // 그 자식 반환
        return child;
    }

    /// <summary>
    /// 프리팹 이름 가져오기
    /// </summary>
    public string GetPrefabName()
    {
        return _prefab.name;
    }
}
