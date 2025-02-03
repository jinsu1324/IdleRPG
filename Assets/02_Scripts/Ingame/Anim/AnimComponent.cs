using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimComponent : MonoBehaviour
{
    [SerializeField] private Animator _animator;     // 애니메이터

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        AttackComponent attackComponent = GetComponent<AttackComponent>();
        if (attackComponent != null)
            attackComponent.OnAttack += PlayAttackAnim; // 공격할 때, 공격 애니메이션 재생
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
    /// 공격 애니메이션 재생
    /// </summary>
    public void PlayAttackAnim(AttackArgs args)
    {
        _animator.SetFloat("AttackSpeed", args.AttackSpeed); // 애니메이션 스피드 조정
        _animator.SetTrigger("Attack"); // 공격 애니메이션 재생
    }

    /// <summary>
    /// 공격 애니메이션 타입 변경
    /// </summary>
    public void Change_AttackAnimType(AttackAnimType attackAnimType)
    {
        _animator.SetInteger("AttackAnimType", (int)attackAnimType);
    }

    /// <summary>
    /// 무브 애니메이션 시작
    /// </summary>
    public void MoveAnimStart()
    {
        _animator.SetBool("isMove", true);
    }

    /// <summary>
    /// 무브 애니메이션 끝
    /// </summary>
    public void MoveAnimStop(OnStageChangedArgs args)
    {
        _animator.SetBool("isMove", false);
    }
}
