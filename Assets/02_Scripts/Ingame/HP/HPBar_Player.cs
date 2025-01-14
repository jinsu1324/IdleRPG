using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar_Player : HPBar
{
    /// <summary>
    /// OnEnable
    /// </summary>
    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerResetService.OnReset += ResetHpBar; // 플레이어 리셋할때, HP바 리셋
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerResetService.OnReset -= ResetHpBar;
    }
}
