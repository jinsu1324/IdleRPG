using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimComponent : MonoBehaviour
{
    [SerializeField] private Animator _animator;     // �ִϸ�����

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        AttackComponent attackComponent = GetComponent<AttackComponent>();
        if (attackComponent != null)
            attackComponent.OnAttack += PlayAttackAnim; // ������ ��, ���� �ִϸ��̼� ���
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        AttackComponent attackComponent = GetComponent<AttackComponent>();
        if (attackComponent != null)
            attackComponent.OnAttack -= PlayAttackAnim;
    }

    /// <summary>
    /// ���� �ִϸ��̼� ���
    /// </summary>
    public void PlayAttackAnim(AttackArgs args)
    {
        _animator.SetFloat("AttackSpeed", args.AttackSpeed); // �ִϸ��̼� ���ǵ� ����
        _animator.SetTrigger("Attack"); // ���� �ִϸ��̼� ���
    }

    /// <summary>
    /// ���� �ִϸ��̼� Ÿ�� ����
    /// </summary>
    public void Change_AttackAnimType(AttackAnimType attackAnimType)
    {
        _animator.SetInteger("AttackAnimType", (int)attackAnimType);
    }

    /// <summary>
    /// ���� �ִϸ��̼� ����
    /// </summary>
    public void MoveAnimStart()
    {
        _animator.SetBool("isMove", true);
    }

    /// <summary>
    /// ���� �ִϸ��̼� ��
    /// </summary>
    public void MoveAnimStop(OnStageChangedArgs args)
    {
        _animator.SetBool("isMove", false);
    }
}
