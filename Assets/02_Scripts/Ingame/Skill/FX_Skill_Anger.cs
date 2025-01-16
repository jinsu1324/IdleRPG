using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 분노스킬 이펙트
/// </summary>
public class FX_Skill_Anger : ObjectPoolObj
{
    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(float duration)
    {
        StartCoroutine(StartFX_AfterBackPool(duration));
    }

    /// <summary>
    /// 버프 지속시간만큼 기다린 후에 다시 풀로 보내기
    /// </summary>
    private IEnumerator StartFX_AfterBackPool(float duration)
    {
        yield return new WaitForSeconds(duration);

        BackTrans();
    }
}
