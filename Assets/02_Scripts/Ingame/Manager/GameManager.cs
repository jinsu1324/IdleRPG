using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    /// <summary>
    /// ∞‘¿” ∏ÿ√„
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    /// <summary>
    /// ∞‘¿” ¥ŸΩ√ Ω√¿€
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1f;
    }
}
