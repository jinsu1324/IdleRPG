using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkOnHit : MonoBehaviour
{
    [Title("데미지 받았을 때 깜빡일 스프라이트를 설정해주세요.", Bold = false)]
    [SerializeField] private List<SpriteRenderer> _spriteList; // 깜빡일 스프라이트 렌더러 리스트

    private void OnEnable()
    {
        HPComponent hpComponent = GetComponent<HPComponent>();
        if (hpComponent != null)
            hpComponent.OnTakeDamaged += SpriteBlink;   // 데미지 받았을 때, 스프라이트 깜빡이기
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        HPComponent hpComponent = GetComponent<HPComponent>();
        if (hpComponent != null)
            hpComponent.OnTakeDamaged -= SpriteBlink;
    }

    /// <summary>
    /// 스프라이트 깜빡이기
    /// </summary>
    private void SpriteBlink(OnTakeDamagedArgs args)
    {
        foreach (SpriteRenderer sprite in _spriteList)
            sprite.DOColor(Color.red, 0.1f).OnComplete(() => sprite.DOColor(Color.white, 0.1f));
    }
}
