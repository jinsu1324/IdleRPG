using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier
{
    public float Value;     // ���� ��ȭ�� (��: +50, -20)
    public object Source;   // ���� ��ȭ�� ��ó (��: ���, ����, Ư�� �̺�Ʈ)

    /// <summary>
    /// ������
    /// </summary>
    public StatModifier(float value, object source)
    {
        Value = value;
        Source = source;
    }
}
