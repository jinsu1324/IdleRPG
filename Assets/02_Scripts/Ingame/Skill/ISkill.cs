using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    float CurrentTime { get; }      // 딜레이 체크할 시간

    /// <summary>
    /// 쿨타임 체크
    /// </summary>
    bool CheckCoolTime();

    /// <summary>
    /// 스킬 실행
    /// </summary>
    void ExecuteSkill();
}
