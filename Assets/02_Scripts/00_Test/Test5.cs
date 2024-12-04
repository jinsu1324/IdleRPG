using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test5 : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("5  ------ Awake");
    }

    private void OnEnable()
    {
        Debug.Log("5  ------ OnEnable");

    }

    private void Start()
    {
        Debug.Log("5  ------ Start");
    }
}
