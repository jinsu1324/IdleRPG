using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // 오브젝트 풀에 생성할 프리팹
    [SerializeField]
    private ObjectPoolObject _prefab;


    // 한번에 생성할 갯수
    [SerializeField]
    private int _count = 10;


    protected void Awake()
    {
        CreateObjs();
    }


    // 오브젝트 생성
    private void CreateObjs()
    {
        for (int i = 0; i < _count; i++)
        {
            ObjectPoolObject obj = Instantiate(_prefab, transform);

            obj.gameObject.SetActive(false);

            obj.Setting(transform);
        }
    }

    // 오브젝트 사용할때 가져오기
    public GameObject GetObj()
    {
        if (transform.childCount <= 0)
            CreateObjs();

        int count = 0;

        GameObject returnObj = transform.GetChild(count).gameObject;

        while (returnObj.activeInHierarchy) // -----> 첫번째 자식이 켜져있어서 true면, count를 올려줘서 다시 다음자식을 받아와봄
        {
            if (++count >= transform.childCount)
                CreateObjs();

            returnObj = transform.GetChild(count).gameObject;
        }

        //returnObj.transform.localScale = Vector3.one;

        returnObj.GetComponent<ObjectPoolObject>().Spawn();

        return returnObj;
    }
}
