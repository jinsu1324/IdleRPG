using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastDefeat : MonoBehaviour
{
    /// <summary>
    /// �����ֱ�
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// ���߱�
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
