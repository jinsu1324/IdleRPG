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
        if (_reddot == null)
        {
            Debug.LogWarning($"[{gameObject.name}] RedDot GameObject�� �������� �ʾҽ��ϴ�.");
            return;
        }

        if (condition == null)
        {
            Debug.LogWarning($"[{gameObject.name}] Ȱ��ȭ ������ �������� �ʾҽ��ϴ�.");
            _reddot.SetActive(false);
            return;
        }

        _reddot.SetActive(condition.Invoke());
    }
}
