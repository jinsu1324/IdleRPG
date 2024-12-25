using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))] // 애니메이터 필요 자동추가
public class AnimComponent : MonoBehaviour
{
    private Animator _animator;     // 애니메이터
    private bool _isInit;           // 초기화 되었는지 플래그

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        _isInit = false;

        _animator = GetComponent<Animator>();
        AttackComponentBase attackComponent = GetComponent<AttackComponentBase>();
        if (attackComponent != null)
            attackComponent.OnAttacked += PlayAttackAnim;   // 공격할 때, 공격 애니메이션 재생

        _isInit = true; // 초기화 완료
    }

    /// <summary>
    /// 공격 애니메이션 재생
    /// </summary>
    public void PlayAttackAnim(OnAttackedArgs args)
    {
        _animator.SetFloat("AttackSpeed", args.AttackSpeed);
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    /// 공격 애니메이션 타입 변경
    /// </summary>
    public void Change_AttackAnimType(AttackAnimType attackAnimType)
    {
        _animator.SetInteger("AttackAnimType", (int)attackAnimType);
    }

    /// <summary>
    /// 이벤트 구독 해제 OnDisable
    /// </summary>
    private void OnDisable()
    {
        if (_isInit == false) // 초기화 안된상태면 리턴 (오브젝트 풀링 때문)
            return;

        AttackComponentBase attackComponent = GetComponentInChildren<AttackComponentBase>();
        if (attackComponent != null)
            attackComponent.OnAttacked -= PlayAttackAnim;
    }
}
