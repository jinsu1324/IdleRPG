using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICurrencyManager
{
    void AddCurrency(int amount);           // 재화 추가
    void ReduceCurrency(int amount);        // 재화 감소
    int GetCurrencyCount();                 // 재화 가져오기
    bool HasEnoughCurrency(int cost);       // 재화 충분한지 체크
}
