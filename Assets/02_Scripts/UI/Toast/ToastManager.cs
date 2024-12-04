using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastManager : MonoBehaviour
{
    public static ToastManager Instance { get; private set; }       // �̱��� �ν��Ͻ�

    [SerializeField] private ToastCombatPower _toastCombatPower;    // ������ ��ġ �佺Ʈ �޽���
    private Coroutine _toastCombatPowerCoroutine;                   // ������ ��ġ �佺Ʈ �޽��� �ڷ�ƾ ���� ����

    /// <summary>
    /// Awake
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Start
    /// </summary>
    private void OnEnable()
    {
        PlayerManager.Instance.OnStatChanged += ShowToastCombatPower; // ���� �ٲ�� �佺Ʈ�޽��� �ߵ��� �̺�Ʈ ����
        Debug.Log("ToastManager OnEnable �����Ϸ�!");
    }

    /// <summary>
    /// ������ ��ġ �佺Ʈ�޽��� �����ֱ�
    /// </summary>
    public void ShowToastCombatPower(OnStatChangedArgs? args)
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
        PlayerManager.Instance.OnStatChanged -= ShowToastCombatPower;
    }
}
