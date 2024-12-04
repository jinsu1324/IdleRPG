using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test4 : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("4  ------ Awake");
    }

    private void OnEnable()
    {
        Debug.Log("4  ------ OnEnable");

    }

    private void Start()
    {
        Debug.Log("4  ------ Start");
    }
}
