using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier
{
    public float Value;     // ���� ��ȭ�� (��: +50, -20)
    public string SourceID;   // ���� ��ȭ�� ��óID (��: ���, ����, Ư�� �̺�Ʈ)

    /// <summary>
    /// ������
    /// </summary>
    public StatModifier(float value, string sourceID)
    {
        Value = value;
        SourceID = sourceID;
    }
}
