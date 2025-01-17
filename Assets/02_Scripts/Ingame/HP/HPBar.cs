using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HPBar : MonoBehaviour
{
    [SerializeField] protected Slider _hpSlider;  // HP �����̴�

    /// <summary>
    /// OnEnable
    /// </summary>
    protected virtual void OnEnable()
    {
        GetComponent<HPComponent>().OnTakeDamage += UpdateHPSlider;   // ������ �޾��� ��, HP�����̴� ������Ʈ
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
    /// HP�����̴� ������Ʈ
    /// </summary>
    public void UpdateHPSlider(TakeDamageArgs args)
    {
        _hpSlider.value = (float)args.CurrentHp / (float)args.MaxHp;
    }

    /// <summary>
    /// HP�� ����
    /// </summary>
    protected void ResetHpBar()
    {
        _hpSlider.value = 1;
    }
}
