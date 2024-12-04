using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    // 싱글톤
    private static Test1 _instance;
    public static Test1 Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError($"{typeof(Test1).Name} 싱글톤이 초기화되지 않았습니다.");
                return null;
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        Debug.Log("1  ------ Awake");

        if (_instance == null)
        {
            _instance = this as Test1;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }





    public event Action OnTest1Action;


    private void OnEnable()
    {
        Debug.Log("1  ------ OnEnable");

    }

    private void Start()
    {
        Debug.Log("1  ------ Start");

        Debug.Log("1  ------ OnTest1Action 실행 전!");
        OnTest1Action?.Invoke();
        Debug.Log("1  ------ OnTest1Action 실행완료!");
    }

}
