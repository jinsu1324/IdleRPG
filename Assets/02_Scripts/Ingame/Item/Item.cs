using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string ID { get; set; }                  // ID
    public ItemType ItemType { get; set; }          // ������ Ÿ��
    public int Count { get; set; }                  // ����
    [JsonIgnore] public string Name { get; set; }   // �̸�
    [JsonIgnore] public string Grade { get; set; }  // ���
    [JsonIgnore] public string Desc { get; set; }   // ����
    [JsonIgnore] public Sprite Icon { get; set; }   // ������

    /// <summary>
    /// ������ ���� �߰�
    /// </summary>
    public void AddCount() => Count++;
}
