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
    public Action<OnTakeDamagedArgs> OnTakeDamaged; // 데미지 받았을때 이벤트
    
    private int _currentHp;                         // 현재 체력  
    private int _maxHp;                             // 최대 체력

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(int initHp)
    {
        _maxHp = initHp;
        _currentHp = _maxHp;
    }

    /// <summary>
    /// 데미지 받음
    /// </summary>
    public void TakeDamage(int atk)
    {
        _currentHp -= atk;
        
        OnTakeDamagedArgs args = new OnTakeDamagedArgs() { CurrentHp = _currentHp, MaxHp = _maxHp };
        OnTakeDamaged?.Invoke(args);
    }
}
