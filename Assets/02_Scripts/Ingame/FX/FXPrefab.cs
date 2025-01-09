using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
