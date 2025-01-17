using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    float CoolTime { get; }     // 스킬 쿨타임
    float CurrentTime { get; }  // 쿨타임 체크할 시간

    /// <summary>
    /// 쿨타임 체크
    /// </summary>
    bool CheckCoolTime();

    /// <summary>
    /// 스킬 실행
    /// </summary>
    void ExecuteSkill();

    /// <summary>
    /// 현재 쿨타임 진행상황 가져오기
    /// </summary>
    float GetCurrentCoolTimeProgress();
}
