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

        Debug.Log("2  ------ OnTest1Action에 구독 전!");
        Test1.Instance.OnTest1Action += Test2Func;
        Debug.Log("2  ------ OnTest1Action에 구독 완료!");
    }

    private void Start()
    {
        Debug.Log("2  ------ Start");
    }

    private void Test2Func()
    {
        Debug.Log("2  ------ OnTest1Action에 구독한 핸들러 함수 실행!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    }

}
