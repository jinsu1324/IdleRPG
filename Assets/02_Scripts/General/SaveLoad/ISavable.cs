using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���̺��� Ŭ������ ���� �������̽�
/// </summary>
public interface ISavable
{
    string Key { get; }     // �������� ���� Ű
    void DataLoadTask();    // ������ �ҷ������Ҷ� �½�ũ��
}
