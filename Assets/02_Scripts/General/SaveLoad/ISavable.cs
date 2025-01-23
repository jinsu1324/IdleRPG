using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 세이브할 클래스에 붙일 인터페이스
/// </summary>
public interface ISavable
{
    string Key { get; }     // 데이터의 고유 키
    void DataLoadTask();    // 데이터 불러오기할때 태스크들
}
