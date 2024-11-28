using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SerializedMonoBehaviour
{
    private PlayerData _playerStats;


    private int _atk;         // 공격력
    private int _atkSpeed;    // 공격속도
    private int _hp;          // 체력
    private int _maxHp;       // 최대체력
    private int _critical;    // 크리티컬 확률


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
