using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastManager : MonoBehaviour
{
    [SerializeField] private ToastCombatPower _toastCombatPower;    // ������ ��ġ �佺Ʈ �޽���
    private Coroutine _toastCombatPowerCoroutine;                   // ������ ��ġ �佺Ʈ �޽��� �ڷ�ƾ ���� ����

    [SerializeField] private ToastDefeat _toastDefeat;              // �й� �佺Ʈ�޽���
    private Coroutine _toastDefeatCoroutine;                        // �й� �佺Ʈ�޽��� �ڷ�ƾ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += StartShow_ToastCombatPower;  // �÷��̾� ���� ����Ǿ��� ��, ������ �佺Ʈ�޽��� �����ֱ�
        StageManager.OnStageDefeat += StartShow_ToastDefeat;    // �������� �й�� �й��佺Ʈ �����ֱ�
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= StartShow_ToastCombatPower;
        StageManager.OnStageDefeat -= StartShow_ToastDefeat;
    }

    /// <summary>
    /// ������ ��ġ �佺Ʈ�޽��� �����ֱ�
    /// </summary>
    public void StartShow_ToastCombatPower(PlayerStatArgs args)
    {
        // �̹� �ڷ�ƾ ������ ���� �ߴ�
        if (_toastCombatPowerCoroutine != null) 
            StopCoroutine(_toastCombatPowerCoroutine);

        // ���ο� �ڷ�ƾ ����
        _toastCombatPowerCoroutine = StartCoroutine(Show_ToastCombatPower(args));
    }

    /// <summary>
    /// ������ ��ġ �佺Ʈ�޽��� �����ִ� �ڷ�ƾ
    /// </summary>
    private IEnumerator Show_ToastCombatPower(PlayerStatArgs args)
    {
        _toastCombatPower.Hide();

        _toastCombatPower.Init(args);
        yield return new WaitForSecondsRealtime(1f);

        _toastCombatPower.Hide();
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
}
