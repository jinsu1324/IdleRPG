using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 장비장착 상황만 UI에 렌더링될 플레이어 코어
/// </summary>
public class UIRenderPlayer : MonoBehaviour
{
    private EquipGearComponent _equipGearComponent;                 // 장착 장비 컴포넌트
    private AnimComponent _animComponent;                           // 애님 컴포넌트

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Init_AnimComponent();
        Init_EquipGearComponent();
    }

    /// <summary>
    /// Anim 컴포넌트 초기화
    /// </summary>
    private void Init_AnimComponent()
    {
        _animComponent = GetComponent<AnimComponent>();
        _animComponent.Init();
    }

    /// <summary>
    /// EquipGearComponent 초기화
    /// </summary>
    private void Init_EquipGearComponent()
    {
        _equipGearComponent = GetComponent<EquipGearComponent>();
    }
}
