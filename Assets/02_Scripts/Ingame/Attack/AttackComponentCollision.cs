using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponentCollision : AttackComponentBase
{
    private IDamagable _target;     // ���� Ÿ��
    private bool _canAttackInRange; // ���ݰ����� ������ ���Դ���
    private bool _isFirstAttack;    // ù��° ��������

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
        if (_canAttackInRange == false)  // ���ݰ��� ������ �ƴϸ� ����
            return;

        if (_isFirstAttack == true) // ù��° ������ ��Ÿ�� ���� �ٷ� ���� �� ����
        {
            StartAttackProcess();
            _isFirstAttack = false;
            return;
        }

        if (IsAttackCoolTime()) // ��Ÿ�Ӹ��� ���� ���μ��� ����
            StartAttackProcess();
    }

    /// <summary>
    /// �÷��̾�� ������ Ÿ���� �� �÷��̾�� ����
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _target = collision.gameObject.GetComponent<IDamagable>();  // Ÿ�� ����
            GetComponent<MoveComponent>().ChangeMoveState_Stop();   // ������ ���߱�

            _canAttackInRange = true;
        }
    }

    /// <summary>
    /// �÷��̾�� �������� Ÿ�پ��ֱ�
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _target = null; // Ÿ�� null��
            GetComponent<MoveComponent>().ChangeMoveState_Move();   // �ٽ� �����̱�

            _canAttackInRange = false;
        }
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    protected override void Attack()
    {
        if (_target == null)
        {
            Debug.Log("Ÿ���� �����ϴ�.");
            return;
        }

        _target.TakeDamage(_attackPower); // ������ ����   
    }
}
