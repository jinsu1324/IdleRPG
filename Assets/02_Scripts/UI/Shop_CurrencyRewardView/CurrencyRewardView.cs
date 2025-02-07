using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyRewardView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countText;    // 획득 재화 갯수 텍스트

    /// <summary>
    /// 보여주기
    /// </summary>
    public void Show(OnRewardedArgs args)
    {
        ON();
        StartCoroutine(ShowProcess(args));
    }

    /// <summary>
    /// 보여주기 코루틴
    /// </summary>
    private IEnumerator ShowProcess(OnRewardedArgs args)
    {
        _countText.text = args.Count.ToString();
        yield return new WaitForSecondsRealtime(2.0f);
        OFF();
    }

    /// <summary>
    /// 켜기
    /// </summary>
    public void ON()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 끄기
    /// </summary>
    public void OFF()
    {
        gameObject.SetActive(false);
    }
}
