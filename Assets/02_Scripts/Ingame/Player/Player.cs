using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SerializedMonoBehaviour
{
    private PlayerData _playerStats;


    private int _atk;         // ���ݷ�
    private int _atkSpeed;    // ���ݼӵ�
    private int _hp;          // ü��
    private int _maxHp;       // �ִ�ü��
    private int _critical;    // ũ��Ƽ�� Ȯ��


    private void Initialize()
    {
        
    }

    private void Attack()
    {

    }

    private void Damaged(int atk)
    {
        _hp -= atk;
        
        if (_hp <= 0)
            Died();
    }

    private void Died()
    {

    }


}
