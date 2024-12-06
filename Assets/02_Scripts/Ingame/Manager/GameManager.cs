using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    /// <summary>
    /// ���� ����
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// ���� �ٽ� ����
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// ���� ��� (�ϴ� �ӽ�)
    /// </summary>
    public void Speed()
    {
        Time.timeScale = 2f;
    }
}
