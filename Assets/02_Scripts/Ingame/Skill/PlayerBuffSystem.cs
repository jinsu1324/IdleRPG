using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffSystem : SingletonBase<PlayerBuffSystem>
{
    /// <summary>
    /// 플레이어에게 버프 적용 시작
    /// </summary>
    public void StartBuffToPlayer(float buffDuration, PlayerStatUpdateArgs args)
    {
        StartCoroutine(Buff(buffDuration, args));
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
