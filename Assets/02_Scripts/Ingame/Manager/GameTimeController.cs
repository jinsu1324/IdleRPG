using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeController 
{
    /// <summary>
    /// ���� ����
    /// </summary>
    public static void Pause()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// ���� �ٽ� ����
    /// </summary>
    public static void Resume()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// ���� ��� (�ϴ� �ӽ�)
    /// </summary>
    public static void Speed()
    {
        Time.timeScale = 2f;
    }
}
