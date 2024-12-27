using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TabPopupBase : SerializedMonoBehaviour
{
    /// <summary>
    /// ÆË¾÷ ÄÑ±â
    /// </summary>
    public abstract void Show();

    /// <summary>
    /// ÆË¾÷ ²ô±â
    /// </summary>
    public abstract void Hide();
}
