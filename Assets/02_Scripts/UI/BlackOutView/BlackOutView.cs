using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOutView : MonoBehaviour
{
    [SerializeField] private Image _blackImage;     // �� �̹���
    private float _fadeDuration = 0.25f;            // ���̵� ��/�ƿ��� �ɸ��� �ð�

    /// <summary>
    /// �����ֱ�
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
        _blackImage.DOFade(1f, _fadeDuration).SetUpdate(true); // timescale ���� �ȹް�
    }

    /// <summary>
    /// ���߱�
    /// </summary>
    public void Hide()
    {
        _blackImage.DOFade(0f, _fadeDuration).SetUpdate(true);
        gameObject.SetActive(false);
    }
}
