using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPCanvas : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;             // HP 슬라이더

    /// <summary>
    /// HP바 업데이트
    /// </summary>
    public void UpdateHPBar(int currentHp, int MaxHp)
    {
        _hpBar.value = (float)currentHp / (float)MaxHp;
    }
}
