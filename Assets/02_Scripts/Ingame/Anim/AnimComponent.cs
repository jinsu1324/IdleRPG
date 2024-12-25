using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))] // �ִϸ����� �ʿ� �ڵ��߰�
public class AnimComponent : MonoBehaviour
{
    private Animator _animator;     // �ִϸ�����
    private bool _isInit;           // �ʱ�ȭ �Ǿ����� �÷���

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init()
    {
        _isInit = false;

        _animator = GetComponent<Animator>();
        AttackComponentBase attackComponent = GetComponent<AttackComponentBase>();
        if (attackComponent != null)
            attackComponent.OnAttacked += PlayAttackAnim;   // ������ ��, ���� �ִϸ��̼� ���

        _isInit = true; // �ʱ�ȭ �Ϸ�
    }

    /// <summary>
    /// ���� �ִϸ��̼� ���
    /// </summary>
    public void PlayAttackAnim(OnAttackedArgs args)
    {
        _animator.SetFloat("AttackSpeed", args.AttackSpeed);
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    /// ���� �ִϸ��̼� Ÿ�� ����
    /// </summary>
    public void Change_AttackAnimType(AttackAnimType attackAnimType)
    {
        _animator.SetInteger("AttackAnimType", (int)attackAnimType);
    }

    /// <summary>
    /// �̺�Ʈ ���� ���� OnDisable
    /// </summary>
    private void OnDisable()
    {
        if (_isInit == false) // �ʱ�ȭ �ȵȻ��¸� ���� (������Ʈ Ǯ�� ����)
            return;

        AttackComponentBase attackComponent = GetComponentInChildren<AttackComponentBase>();
        if (attackComponent != null)
            attackComponent.OnAttacked -= PlayAttackAnim;
    }
}
