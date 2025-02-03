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
        StageManager.OnStageBuildStart += WindAnimStart;  // �������� ���� ���۽� -> �ٶ� �ִϸ��̼� ����
        StageManager.OnStageBuildFinish += WindAnimEnd;   // �������� ���� ����� -> �ٶ� �ִϸ��̼� ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageBuildStart -= WindAnimStart;
        StageManager.OnStageBuildFinish -= WindAnimEnd;
    }

    /// <summary>
    /// �ٶ��ִϸ��̼� ����
    /// </summary>
    public void WindAnimStart(StageBuildArgs args)
    {
        _animator.SetBool("isWind", true);
    }

    /// <summary>
    /// �ٶ��ִϸ��̼� ����
    /// </summary>
    public void WindAnimEnd(StageBuildArgs args)
    {
        _animator.SetBool("isWind", false);
    }
}
