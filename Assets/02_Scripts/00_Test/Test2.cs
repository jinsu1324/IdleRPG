using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("2  ------ Awake");
    }

    private void OnEnable()
    {
        Debug.Log("2  ------ OnEnable");

        Debug.Log("2  ------ OnTest1Action�� ���� ��!");
        Test1.Instance.OnTest1Action += Test2Func;
        Debug.Log("2  ------ OnTest1Action�� ���� �Ϸ�!");
    }

    private void Start()
    {
        Debug.Log("2  ------ Start");
    }

    private void Test2Func()
    {
        Debug.Log("2  ------ OnTest1Action�� ������ �ڵ鷯 �Լ� ����!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }

}
