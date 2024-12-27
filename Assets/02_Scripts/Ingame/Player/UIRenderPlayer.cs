using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRenderPlayer : MonoBehaviour
{
    private EquipItemComponent _equipItemComponent;                 // 장착 아이템 컴포넌트
    private AnimComponent _animComponent;                           // 애님 컴포넌트

    private void Start()
    {
        Init_AnimComponent();
        Init_EquipItemComponent();
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
    /// EquipItemComponent 초기화
    /// </summary>
    private void Init_EquipItemComponent()
    {
        _equipItemComponent = GetComponent<EquipItemComponent>();
    }
}
