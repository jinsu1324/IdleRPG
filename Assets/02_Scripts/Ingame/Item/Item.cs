using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string ID { get; set; }                  // ID
    public ItemType ItemType { get; set; }          // 아이템 타입
    public int Count { get; set; }                  // 갯수
    [JsonIgnore] public string Name { get; set; }   // 이름
    [JsonIgnore] public string Grade { get; set; }  // 등급
    [JsonIgnore] public string Desc { get; set; }   // 설명
    [JsonIgnore] public Sprite Icon { get; set; }   // 아이콘

    /// <summary>
    /// 아이템 갯수 추가
    /// </summary>
    public void AddCount() => Count++;
}
