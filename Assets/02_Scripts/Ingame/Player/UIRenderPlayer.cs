using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������� ��Ȳ�� UI�� �������� �÷��̾� �ھ�
/// </summary>
public class UIRenderPlayer : MonoBehaviour
{
    private EquipGearComponent _equipGearComponent;                 // ���� ��� ������Ʈ
    private AnimComponent _animComponent;                           // �ִ� ������Ʈ

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Init_AnimComponent();
        Init_EquipGearComponent();
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
    /// EquipGearComponent �ʱ�ȭ
    /// </summary>
    private void Init_EquipGearComponent()
    {
        _equipGearComponent = GetComponent<EquipGearComponent>();
    }
}
