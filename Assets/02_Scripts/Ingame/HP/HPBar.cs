using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPBar : MonoBehaviour
{
    [SerializeField] private Slider _hpSlider;  // HP 슬라이더
    private bool _isInit;                       // 초기화 되었는지 플래그

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(int initHp)
    {
        _isInit = false;

        HPComponent hpComponent = GetComponentInParent<HPComponent>();
        if (hpComponent != null)
            hpComponent.OnTakeDamaged += UpdateHPSlider;   // 데미지 받았을 때, HP슬라이더 업데이트

        OnTakeDamagedArgs initArgs = new OnTakeDamagedArgs() { CurrentHp = initHp, MaxHp = initHp };
        UpdateHPSlider(initArgs);  // 초기체력 값으로 HP슬라이더 업데이트
        
        _isInit = true; // 초기화 완료
    }

    /// <summary>
    /// HP슬라이더 업데이트
    /// </summary>
    public void UpdateHPSlider(OnTakeDamagedArgs args)
    {
        _hpSlider.value = (float)args.CurrentHp / (float)args.MaxHp;
    }

    /// <summary>
    /// 이벤트 구독 해제 OnDisable
    /// </summary>
    private void OnDisable()
    {
        if (_isInit == false) // 초기화 안된상태면 리턴 (오브젝트 풀링 때문)
            return;

        HPComponent hpComponent = GetComponentInParent<HPComponent>();
        if (hpComponent != null)
            hpComponent.OnTakeDamaged -= UpdateHPSlider;
    }
}
