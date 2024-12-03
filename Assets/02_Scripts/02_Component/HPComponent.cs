using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OnTakeDamagedArgs
{
    public int CurrentHp;
    public int MaxHp;
}


public class HPComponent : MonoBehaviour
{
    public Action<OnTakeDamagedArgs> OnTakeDamaged; // ������ �޾����� �̺�Ʈ
    
    private int _currentHp;                         // ���� ü��  
    private int _maxHp;                             // �ִ� ü��

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(int initHp)
    {
        _maxHp = initHp;
        _currentHp = _maxHp;
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void TakeDamage(int atk)
    {
        _currentHp -= atk;
        
        OnTakeDamagedArgs args = new OnTakeDamagedArgs() { CurrentHp = _currentHp, MaxHp = _maxHp };
        OnTakeDamaged?.Invoke(args);
    }
}
