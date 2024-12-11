using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier
{
    public float Value;     // 스탯 변화량 (예: +50, -20)
    public object Source;   // 스탯 변화의 출처 (예: 장비, 버프, 특정 이벤트)

    /// <summary>
    /// 생성자
    /// </summary>
    public StatModifier(float value, object source)
    {
        Value = value;
        Source = source;
    }
}
