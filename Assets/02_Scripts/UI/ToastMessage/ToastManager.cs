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
        PlayerStatManager.OnStatChanged += ShowToastCombatPower; // ������ ����� ��, ���հ��ݷ� �佺Ʈ�޽��� �����ֱ�
    }

    /// <summary>
    /// ������ ��ġ �佺Ʈ�޽��� �����ֱ�
    /// </summary>
    public void ShowToastCombatPower(OnStatChangedArgs args)
    {
        // �̹� �ڷ�ƾ ������ ���� �ߴ�
        if (_toastCombatPowerCoroutine != null) 
            StopCoroutine(_toastCombatPowerCoroutine);

        // ���ο� �ڷ�ƾ ����
        _toastCombatPowerCoroutine = StartCoroutine(ShowToastCombatPowerCoroutine());
    }

    /// <summary>
    /// ������ ��ġ �佺Ʈ�޽��� �����ִ� �ڷ�ƾ
    /// </summary>
    private IEnumerator ShowToastCombatPowerCoroutine()
    {
        _toastCombatPower.gameObject.SetActive(false);

        _toastCombatPower.Init();
        _toastCombatPower.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        _toastCombatPower.gameObject.SetActive(false);
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        PlayerStatManager.OnStatChanged -= ShowToastCombatPower;
    }
}
