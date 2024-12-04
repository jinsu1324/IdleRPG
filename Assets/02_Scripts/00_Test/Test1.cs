using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    // �̱���
    private static Test1 _instance;
    public static Test1 Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError($"{typeof(Test1).Name} �̱����� �ʱ�ȭ���� �ʾҽ��ϴ�.");
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

        Debug.Log("1  ------ OnTest1Action ���� ��!");
        OnTest1Action?.Invoke();
        Debug.Log("1  ------ OnTest1Action ����Ϸ�!");
    }

}
