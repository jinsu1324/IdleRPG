using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameToSecond : MonoBehaviour
{
    /// <summary>
    /// �������� �ʷ� �ٲ��ִ� �Լ�
    /// </summary>
    public static float Convert(float frame)
    {
        return frame / 60.0f;
    }
}
