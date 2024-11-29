using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SerializedMonoBehaviour
{
    private int _attackPower;    // ���ݷ�
    private int _attackDelay;    // ���ݼӵ�
    private int _maxHp;          // �ִ�ü��
    private int _critical;       // ũ��Ƽ�� Ȯ��
    private int _currentHp;      // ����ü��

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Initialize(PlayerData playerData)
    {
        UpdateStat(playerData);
    }

    /// <summary>
    /// ���� ������Ʈ
    /// </summary>
    public void UpdateStat(PlayerData playerData)
    {
        Debug.Log("�÷��̾� ������ ���� ������Ʈ!");
        _attackPower = playerData.GetStat(StatID.AttackPower.ToString()).Value;
        _attackDelay = playerData.GetStat(StatID.AttackDelay.ToString()).Value;
        _maxHp = playerData.GetStat(StatID.MaxHp.ToString()).Value;
        _critical = playerData.GetStat(StatID.Critical.ToString()).Value;
        _currentHp = _maxHp;
    }

    private void Attack()
    {

    }

    public void Damaged(int atk)
    {
        _currentHp -= atk;

        Debug.Log($"{atk}�� �������� ����! ���� ü�� : {_currentHp}");
        //if (_currentHp <= 0)
        //    Died();
    }

    private void Died()
    {

    }


}
