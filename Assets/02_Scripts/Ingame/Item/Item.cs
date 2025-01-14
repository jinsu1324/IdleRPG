using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public string ID { get; set; }             // ID
    public ItemType ItemType { get; set; }     // ������ Ÿ��
    public string Name { get; set; }           // �̸�
    public string Grade { get; set; }          // ���
    public string Desc { get; set; }           // ����
    public int Count { get; set; }             // ����
    public Sprite Icon { get; set; }           // ������

    /// <summary>
    /// ������ ���� �߰�
    /// </summary>
    public void AddCount() => Count++;
}
