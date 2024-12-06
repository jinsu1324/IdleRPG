using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    /// <summary>
    /// 게임 멈춤
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// 게임 다시 시작
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// 게임 배속 (일단 임시)
    /// </summary>
    public void Speed()
    {
        Time.timeScale = 2f;
    }
}
