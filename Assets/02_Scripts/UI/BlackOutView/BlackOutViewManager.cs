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
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageChallange -= FadeInOut;
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

}
