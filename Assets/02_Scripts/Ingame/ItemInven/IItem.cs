using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem
{
    string ID { get; }             // ID
    ItemType ItemType { get; }     // 아이템 타입
    string Name { get; }           // 이름
    string Grade { get; }          // 등급
    int Level { get; }             // 레벨
    int Count { get; }             // 갯수
    int EnhanceableCount { get; }  // 강화 가능 갯수
    Sprite Icon { get; }           // 아이콘


    /// <summary>
    /// 아이템 갯수 추가
    /// </summary>
    void AddCount();

    /// <summary>
    /// 아이템 레벨업
    /// </summary>
    void ItemLevelUp();

    /// <summary>
    /// 아이템 갯수를 강화 갯수만큼 소비
    /// </summary>
    void RemoveCountByEnhance();

    /// <summary>
    /// 강화 가능한지?
    /// </summary>
    bool IsEnhanceable();
}
