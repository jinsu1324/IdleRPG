using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffSystem : SingletonBase<PlayerBuffSystem>
{
    private Coroutine _angerSkillCoroutine; // 분노스킬 코루틴

    /// <summary>
    /// 플레이어에게 버프 적용 시작
    /// </summary>
    public void StartBuffToPlayer(float buffDuration, PlayerStatUpdateArgs args)
    {
        // 이미 코루틴 있다면 실행중단
        if (_angerSkillCoroutine != null)
            StopCoroutine(_angerSkillCoroutine);

        // 새로운 코루틴시작
        _angerSkillCoroutine = StartCoroutine(Buff(buffDuration, args));
    }

    /// <summary>
    /// 버프
    /// </summary>
    private IEnumerator Buff(float buffDuration, PlayerStatUpdateArgs args)
    {
        PlayerStats.UpdateStatModifier(args);   // 스탯에 적용

        yield return new WaitForSeconds(buffDuration);  // 버프시간만큼 대기 후
            
        PlayerStats.RemoveStatModifier(args); // 스탯에서 삭제
    }
}
