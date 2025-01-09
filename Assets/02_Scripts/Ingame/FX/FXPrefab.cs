using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이펙트 프리팹에 붙여서 콜백함수 받아줄 클래스
/// </summary>
public class FXPrefab : ObjectPoolObj
{
    /// <summary>
    /// 파티클 끝났을 때 콜백함수 (Particle Stop Action - Callback)
    /// </summary>
    private void OnParticleSystemStopped()
    {
        BackTrans(); // 풀로 돌려보내기
    }
}
