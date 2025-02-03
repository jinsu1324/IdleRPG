using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackOutViewManager : MonoBehaviour
{
    [SerializeField] private BlackOutView _blackOutView;    // ���ƿ� ��

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageChallange += FadeInOut; // �������� ������ -> ���̵��ξƿ�
        StageManager.OnStageDefeat += FadeInOut_Delay;  // �������� �й�� -> ���̵��ξƿ�(������)
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageChallange -= FadeInOut;
        StageManager.OnStageDefeat -= FadeInOut_Delay;
    }


    /// <summary>
    /// ���̵� �� �ƿ�
    /// </summary>
    private void FadeInOut()
    {
        StartCoroutine(FadeInOutCoroutine());
    }

    /// <summary>
    /// ���̵� �� �ƿ� �ڷ�ƾ
    /// </summary>
    private IEnumerator FadeInOutCoroutine()
    {
        _blackOutView.Show();
        yield return new WaitForSecondsRealtime(2.0f);
        _blackOutView.Hide();
    }


    /// <summary>
    /// ���̵� �� �ƿ� ������
    /// </summary>
    private void FadeInOut_Delay()
    {
        StartCoroutine(FadeInOutCoroutine_Delay());
    }

    /// <summary>
    /// ���̵� �� �ƿ� �ڷ�ƾ ������
    /// </summary>
    private IEnumerator FadeInOutCoroutine_Delay()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        _blackOutView.Show();
        yield return new WaitForSecondsRealtime(1.0f);
        _blackOutView.Hide();
    }
}
