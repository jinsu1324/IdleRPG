using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnhanceableItem
{
    int Level { get; }             // ����
    int EnhanceableCount { get; }  // ��ȭ ���� ����

    /// <summary>
    /// ������ ������
    /// </summary>
    void ItemLevelUp();

    /// <summary>
    /// ������ ������ ��ȭ ������ŭ �Һ�
    /// </summary>
    void RemoveCountByEnhance();

    /// <summary>
    /// ��ȭ ��������? �Լ� ������
    /// </summary>
    bool CanEnhance();
}
