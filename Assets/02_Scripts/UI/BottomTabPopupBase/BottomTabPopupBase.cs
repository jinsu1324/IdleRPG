using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BottomTabPopupBase : SerializedMonoBehaviour
{
    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public abstract void Show();

    /// <summary>
    /// �˾� ����
    /// </summary>
    public abstract void Hide();
}
