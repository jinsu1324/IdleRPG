using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogView : MonoBehaviour
{
    [SerializeField] private TypingEffect _typingEffect;    // Ÿ���� ����Ʈ

    /// <summary>
    /// �ѱ�
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
        _typingEffect.Init();

        StartCoroutine(HideDelay());
    }

    /// <summary>
    /// �������ð� �Ŀ� �ٽ� ������ �ڷ�ƾ
    /// </summary>
    private IEnumerator HideDelay()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        gameObject.SetActive(false);
    }
}
