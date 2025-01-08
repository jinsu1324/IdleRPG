using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastManager : MonoBehaviour
{
    [SerializeField] private ToastCombatPower _toastCombatPower;    // ������ ��ġ �佺Ʈ �޽���
    private Coroutine _toastCombatPowerCoroutine;                   // ������ ��ġ �佺Ʈ �޽��� �ڷ�ƾ ���� ����

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        PlayerStats.OnPlayerStatChanged += StartShow_ToastCombatPower;  // �÷��̾� ���� ����Ǿ��� ��, ������ �佺Ʈ�޽��� �����ֱ�
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStats.OnPlayerStatChanged -= StartShow_ToastCombatPower;
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
        yield return new WaitForSeconds(1f);

        _toastCombatPower.Hide();
    }
}
