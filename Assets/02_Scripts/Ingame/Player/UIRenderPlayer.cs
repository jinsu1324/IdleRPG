using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRenderPlayer : MonoBehaviour
{
    private EquipItemComponent _equipItemComponent;                 // ���� ������ ������Ʈ
    private AnimComponent _animComponent;                           // �ִ� ������Ʈ

    private void Start()
    {
        Init_AnimComponent();
        Init_EquipItemComponent();
    }

    /// <summary>
    /// Anim ������Ʈ �ʱ�ȭ
    /// </summary>
    private void Init_AnimComponent()
    {
        _animComponent = GetComponent<AnimComponent>();
        _animComponent.Init();
    }

    /// <summary>
    /// EquipItemComponent �ʱ�ȭ
    /// </summary>
    private void Init_EquipItemComponent()
    {
        _equipItemComponent = GetComponent<EquipItemComponent>();
    }
}
