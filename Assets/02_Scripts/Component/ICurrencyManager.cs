using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICurrencyManager
{
    void AddCurrency(int amount);           // ��ȭ �߰�
    void ReduceCurrency(int amount);        // ��ȭ ����
    int GetCurrencyCount();                 // ��ȭ ��������
    bool HasEnoughCurrency(int cost);       // ��ȭ ������� üũ
}
