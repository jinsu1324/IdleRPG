using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyRewardView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;    // ȹ�� ��ȭ ���� �ؽ�Ʈ

    /// <summary>
    /// �����ֱ�
    /// </summary>
    public void Show(OnRewardedArgs args)
    {
        ON();
        StartCoroutine(ShowProcess(args));
    }

    /// <summary>
    /// �����ֱ� �ڷ�ƾ
    /// </summary>
    private IEnumerator ShowProcess(OnRewardedArgs args)
    {
        _countText.text = args.Count.ToString();
        yield return new WaitForSecondsRealtime(2.0f);
        OFF();
    }

    /// <summary>
    /// �ѱ�
    /// </summary>
    public void ON()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// ����
    /// </summary>
    public void OFF()
    {
        gameObject.SetActive(false);
    }
}
