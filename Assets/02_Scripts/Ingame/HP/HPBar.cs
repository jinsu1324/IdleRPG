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
        GetComponent<HPComponent>().OnTakeDamaged += UpdateHPSlider;   // ������ �޾��� ��, HP�����̴� ������Ʈ
        ResetHpBar();
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    protected virtual void OnDisable()
    {
        GetComponent<HPComponent>().OnTakeDamaged -= UpdateHPSlider;
    }

    /// <summary>
    /// HP�����̴� ������Ʈ
    /// </summary>
    public void UpdateHPSlider(OnTakeDamagedArgs args)
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
