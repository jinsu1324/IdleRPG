using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGWind : MonoBehaviour
{
    [SerializeField] private Animator _animator;    // 애니메이터

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageBuildStart += WindAnimStart;  // 스테이지 빌딩 시작시 -> 바람 애니메이션 시작
        StageManager.OnStageBuildFinish += WindAnimEnd;   // 스테이지 빌딩 종료시 -> 바람 애니메이션 종료
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageBuildStart -= WindAnimStart;
        StageManager.OnStageBuildFinish -= WindAnimEnd;
    }

    /// <summary>
    /// 바람애니메이션 시작
    /// </summary>
    public void WindAnimStart(StageBuildArgs args)
    {
        _animator.SetBool("isWind", true);
    }

    /// <summary>
    /// 바람애니메이션 종료
    /// </summary>
    public void WindAnimEnd(StageBuildArgs args)
    {
        _animator.SetBool("isWind", false);
    }
}
