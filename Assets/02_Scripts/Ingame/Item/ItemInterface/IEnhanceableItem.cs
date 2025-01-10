using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnhanceableItem
{
    int Level { get; }             // 레벨
    int EnhanceableCount { get; }  // 강화 가능 갯수

    /// <summary>
    /// 아이템 레벨업
    /// </summary>
    void ItemLevelUp();

    /// <summary>
    /// 아이템 갯수를 강화 갯수만큼 소비
    /// </summary>
    void RemoveCountByEnhance();

    /// <summary>
    /// 강화 가능한지? 함수 재정의
    /// </summary>
    bool CanEnhance();
}
