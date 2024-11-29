using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SerializedMonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;  // ����ü ������ 

    private int _attackPower;                               // ���ݷ�
    private int _attackSpeed;                               // ���ݼӵ�
    private int _maxHp;                                     // �ִ�ü��
    private int _critical;                                  // ũ��Ƽ�� Ȯ��
    private int _currentHp;                                 // ����ü��

    private float _attackCooldown;                          // ���� ��Ÿ��
    private float _time;                                    // ��Ÿ�� �ð� ����

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Initialize(PlayerData playerData)
    {
        UpdateStat(playerData);
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (IsAttackCoolTime())
            Attack();   
    }

    /// <summary>
    /// ���� ������Ʈ
    /// </summary>
    public void UpdateStat(PlayerData playerData)
    {
        _attackPower = playerData.GetStat(StatID.AttackPower.ToString()).Value;
        _attackSpeed = playerData.GetStat(StatID.AttackSpeed.ToString()).Value;
        _maxHp = playerData.GetStat(StatID.MaxHp.ToString()).Value;
        _critical = playerData.GetStat(StatID.Critical.ToString()).Value;
        _currentHp = _maxHp;
        _attackCooldown = 1f / _attackSpeed;
    }

    /// <summary>
    /// ���� ��Ÿ�� ���
    /// </summary>
    private bool IsAttackCoolTime()
    {
        _time += Time.deltaTime;

        if (_time >= _attackCooldown)
        {
            _time %= _attackCooldown;
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Attack()
    {
        // Ÿ�� ã��
        Enemy targetEnemy = EnemyManager.Instance.Get_ClosestEnemy(transform.position);
        if (targetEnemy == null)
        {
            Debug.Log("Ÿ�� ����!");
            return;
        }

        // ����ü ���� �� �ʱ�ȭ
        Projectile projectile = Instantiate(_projectilePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        projectile.Initialize(targetEnemy, _attackPower);
    }


    /// <summary>
    /// ������ ����
    /// </summary>
    public void TakeDamage(int atk)
    {
        _currentHp -= atk;

        //if (_currentHp <= 0)
        //    Die();
    }

    private void Die()
    {

    }


}
