using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HPBar : MonoBehaviour
{
    [SerializeField] protected Slider _hpSlider;  // HP 슬라이더

    /// <summary>
    /// OnEnable
    /// </summary>
    protected virtual void OnEnable()
    {
        GetComponent<HPComponent>().OnTakeDamage += UpdateHPSlider;   // 데미지 받았을 때, HP슬라이더 업데이트
        ResetHpBar();
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    protected virtual void OnDisable()
    {
        GetComponent<HPComponent>().OnTakeDamage -= UpdateHPSlider;
    }

    /// <summary>
    /// HP슬라이더 업데이트
    /// </summary>
    public void UpdateHPSlider(TakeDamageArgs args)
    {
        _hpSlider.value = (float)args.CurrentHp / (float)args.MaxHp;
    }

    /// <summary>
    /// HP바 리셋
    /// </summary>
    protected void ResetHpBar()
    {
        _hpSlider.value = 1;
    }
}
