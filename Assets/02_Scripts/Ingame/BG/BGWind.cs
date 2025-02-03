using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGWind : MonoBehaviour
{
    [SerializeField] private Animator _animator;    // �ִϸ�����

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageBuilding += WindAnimStart;  // �������� ������ -> �ٶ� �ִϸ��̼� ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageBuilding -= WindAnimStart;
    }

    /// <summary>
    /// �ٶ��ִϸ��̼� ����
    /// </summary>
    public void WindAnimStart()
    {
        _animator.SetTrigger("Wind");
    }
}
