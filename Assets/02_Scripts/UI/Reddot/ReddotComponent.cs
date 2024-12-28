using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReddotComponent : MonoBehaviour
{
    [SerializeField] private GameObject _reddot;    // ����� ǥ�� �̹���

    /// <summary>
    /// ����� ���¸� ������Ʈ
    /// </summary>
    public void UpdateReddot(Func<bool> condition)
    {
        _reddot.SetActive(condition.Invoke());
    }
}
