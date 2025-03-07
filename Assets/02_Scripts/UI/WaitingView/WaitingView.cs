using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingView : MonoBehaviour
{
    /// <summary>
    /// 보여주기
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 감추기
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
