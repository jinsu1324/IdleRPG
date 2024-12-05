using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponentCollision : AttackComponentBase
{
    private IDamagable _target;     // 공격 타겟
    private bool _canAttackInRange; // 공격가능한 범위에 들어왔는지
    private bool _isFirstAttack;    // 첫번째 공격인지

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _canAttackInRange = false;
        _isFirstAttack = true;
    }
    
    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (_canAttackInRange == false)  // 공격가능 범위가 아니면 리턴
            return;

        if (_isFirstAttack == true) // 첫번째 공격은 쿨타임 없이 바로 공격 후 리턴
        {
            StartAttackProcess();
            _isFirstAttack = false;
            return;
        }

        if (IsAttackCoolTime()) // 쿨타임마다 공격 프로세스 시작
            StartAttackProcess();
    }

    /// <summary>
    /// 플레이어와 닿으면 타겟을 그 플레이어로 설정
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _target = collision.gameObject.GetComponent<IDamagable>();  // 타겟 설정
            GetComponent<MoveComponent>().ChangeMoveState_Stop();   // 움직임 멈추기

            _canAttackInRange = true;
        }
    }

    /// <summary>
    /// 플레이어와 떨어지면 타겟없애기
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _target = null; // 타겟 null로
            GetComponent<MoveComponent>().ChangeMoveState_Move();   // 다시 움직이기

            _canAttackInRange = false;
        }
    }

    /// <summary>
    /// 실제 공격
    /// </summary>
    protected override void Attack()
    {
        if (_target == null)
        {
            Debug.Log("타겟이 없습니다.");
            return;
        }

        _target.TakeDamage(_attackPower); // 데미지 전달   
    }
}
