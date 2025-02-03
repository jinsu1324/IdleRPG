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
        StageManager.OnStageBuilding += WindAnimStart;  // 스테이지 빌딩시 -> 바람 애니메이션 시작
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageBuilding -= WindAnimStart;
    }

    /// <summary>
    /// 바람애니메이션 시작
    /// </summary>
    public void WindAnimStart()
    {
        _animator.SetTrigger("Wind");
    }
}
