using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogView : MonoBehaviour
{
    [SerializeField] private TypingEffect _typingEffect;    // 타이핑 이펙트

    /// <summary>
    /// 켜기
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
        _typingEffect.Init();

        StartCoroutine(HideDelay());
    }

    /// <summary>
    /// 일정정시간 후에 다시 꺼지는 코루틴
    /// </summary>
    private IEnumerator HideDelay()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        gameObject.SetActive(false);
    }
}
