using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3 : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("3  ------ Awake");
    }

    private void OnEnable()
    {
        Debug.Log("3  ------ OnEnable");

    }

    private void Start()
    {
        Debug.Log("3  ------ Start");
    }
}
