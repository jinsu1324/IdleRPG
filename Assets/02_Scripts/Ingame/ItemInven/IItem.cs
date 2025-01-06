using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    string ID { get; }             // ID
    ItemType ItemType { get; }     // ������ Ÿ��
    string Name { get; }           // �̸�
    string Grade { get; }          // ���
    int Level { get; }             // ����
    int Count { get; }             // ����
    int EnhanceableCount { get; }  // ��ȭ ���� ����
    Sprite Icon { get; }           // ������


    /// <summary>
    /// ������ ���� �߰�
    /// </summary>
    void AddCount();

    /// <summary>
    /// ������ ������
    /// </summary>
    void ItemLevelUp();

    /// <summary>
    /// ������ ������ ��ȭ ������ŭ �Һ�
    /// </summary>
    void RemoveCountByEnhance();

    /// <summary>
    /// ��ȭ ��������?
    /// </summary>
    bool IsEnhanceable();
}
