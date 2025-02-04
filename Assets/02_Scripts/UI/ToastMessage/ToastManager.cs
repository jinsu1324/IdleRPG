using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastManager : SingletonBase<ToastManager>
{
    [SerializeField] private ToastTotalPower _toastTotalPower;      // ������ ��ġ �佺Ʈ �޽���
    private Coroutine _toastTotalPowerCoroutine;                    // ������ ��ġ �佺Ʈ �޽��� �ڷ�ƾ ���� ����

    [SerializeField] private ToastDefeat _toastDefeat;              // �й� �佺Ʈ�޽���
    private Coroutine _toastDefeatCoroutine;                        // �й� �佺Ʈ�޽��� �ڷ�ƾ

    [SerializeField] private ToastCommon _toastCommon;              // Ŀ�� �佺Ʈ�޽���
    private Coroutine _toastCommonCoroutine;                        // Ŀ�� �佺Ʈ�޽��� �ڷ�ƾ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += StartShow_ToastTotalPower;  // �÷��̾� ���� ����Ǿ��� ��, ������ �佺Ʈ�޽��� �����ֱ�
        StageManager.OnStageDefeat += StartShow_ToastDefeat;    // �������� �й�� �й��佺Ʈ �����ֱ�
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= StartShow_ToastTotalPower;
        StageManager.OnStageDefeat -= StartShow_ToastDefeat;
    }




    /// <summary>
    /// ������ ��ġ �佺Ʈ�޽��� �����ֱ�
    /// </summary>
    public void StartShow_ToastTotalPower(PlayerStatArgs args)
    {
        // �̹� �ڷ�ƾ ������ ���� �ߴ�
        if (_toastTotalPowerCoroutine != null) 
            StopCoroutine(_toastTotalPowerCoroutine);

        // ���ο� �ڷ�ƾ ����
        _toastTotalPowerCoroutine = StartCoroutine(Show_ToastTotalPower(args));
    }

    /// <summary>
    /// ������ ��ġ �佺Ʈ�޽��� �����ִ� �ڷ�ƾ
    /// </summary>
    private IEnumerator Show_ToastTotalPower(PlayerStatArgs args)
    {
        _toastTotalPower.Hide();

        _toastTotalPower.Init(args);
        yield return new WaitForSecondsRealtime(1f);

        _toastTotalPower.Hide();
    }



    /// <summary>
    /// �й� �佺Ʈ�޽��� �����ֱ�
    /// </summary>
    public void StartShow_ToastDefeat()
    {
        // �̹� �ڷ�ƾ ������ ���� �ߴ�
        if (_toastDefeatCoroutine != null)
            StopCoroutine(_toastDefeatCoroutine);

        // ���ο� �ڷ�ƾ ����
        _toastDefeatCoroutine = StartCoroutine(Show_ToastDefeat());
    }

    /// <summary>
    /// �й� �佺Ʈ�޽��� �ڷ�ƾ
    /// </summary>
    private IEnumerator Show_ToastDefeat()
    {
        _toastDefeat.Hide();

        _toastDefeat.Show();
        yield return new WaitForSecondsRealtime(3f);
     
        _toastDefeat.Hide();
    }






    /// <summary>
    /// Ŀ�� �佺Ʈ�޽��� �����ֱ�
    /// </summary>
    public void StartShow_ToastCommon(string text)
    {
        // �̹� �ڷ�ƾ ������ ���� �ߴ�
        if (_toastCommonCoroutine != null)
            StopCoroutine(_toastCommonCoroutine);

        // ���ο� �ڷ�ƾ ����
        _toastCommonCoroutine = StartCoroutine(Show_ToastCommon(text));
    }

    /// <summary>
    /// Ŀ�� �佺Ʈ�޽��� �ڷ�ƾ
    /// </summary>
    private IEnumerator Show_ToastCommon(string text)
    {
        _toastCommon.Hide();

        _toastCommon.Show(text);
        yield return new WaitForSecondsRealtime(1f);

        _toastCommon.Hide();
    }
}
