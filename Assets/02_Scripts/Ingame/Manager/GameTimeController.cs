using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeController 
{
    /// <summary>
    /// 게임 멈춤
    /// </summary>
    public static void Pause()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// 게임 다시 시작
    /// </summary>
    public static void Resume()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// 게임 배속 (일단 임시)
    /// </summary>
    public static void Speed()
    {
        Time.timeScale = 2f;
    }
}
