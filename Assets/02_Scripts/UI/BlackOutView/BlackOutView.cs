using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOutView : MonoBehaviour
{
    [SerializeField] private Image _blackImage;     // 블랙 이미지
    private float _fadeDuration = 0.25f;            // 페이드 인/아웃에 걸리는 시간

    /// <summary>
    /// 보여주기
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
        _blackImage.DOFade(1f, _fadeDuration).SetUpdate(true); // timescale 영향 안받게
    }

    /// <summary>
    /// 감추기
    /// </summary>
    public void Hide()
    {
        _blackImage.DOFade(0f, _fadeDuration).SetUpdate(true);
        gameObject.SetActive(false);
    }
}
