using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameToSecond : MonoBehaviour
{
    /// <summary>
    /// 프레임을 초로 바꿔주는 함수
    /// </summary>
    public static float Convert(float frame)
    {
        return frame / 60.0f;
    }
}
