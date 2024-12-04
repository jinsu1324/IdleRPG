using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPBar : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider;  // HP �����̴�
    private bool _isInit;                       // �ʱ�ȭ �Ǿ����� �÷���

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(int initHp)
    {
        _isInit = false;

        HPComponent hpComponent = GetComponentInParent<HPComponent>();
        if (hpComponent != null)
            hpComponent.OnTakeDamaged += UpdateHPSlider;   // ������ �޾��� ��, HP�����̴� ������Ʈ

        OnTakeDamagedArgs initArgs = new OnTakeDamagedArgs() { CurrentHp = initHp, MaxHp = initHp };
        UpdateHPSlider(initArgs);  // �ʱ�ü�� ������ HP�����̴� ������Ʈ
        
        _isInit = true; // �ʱ�ȭ �Ϸ�
    }

    /// <summary>
    /// HP�����̴� ������Ʈ
    /// </summary>
    public void UpdateHPSlider(OnTakeDamagedArgs args)
    {
        _hpSlider.value = (float)args.CurrentHp / (float)args.MaxHp;
    }

    /// <summary>
    /// �̺�Ʈ ���� ���� OnDisable
    /// </summary>
    private void OnDisable()
    {
        if (_isInit == false) // �ʱ�ȭ �ȵȻ��¸� ���� (������Ʈ Ǯ�� ����)
            return;

        HPComponent hpComponent = GetComponentInParent<HPComponent>();
        if (hpComponent != null)
            hpComponent.OnTakeDamaged -= UpdateHPSlider;
    }
}
