using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReddotComponent : MonoBehaviour
{
    [SerializeField] private GameObject _reddot;    // 레드닷 표시 이미지

    /// <summary>
    /// 레드닷 상태를 업데이트
    /// </summary>
    public void UpdateReddot(Func<bool> condition)
    {
        _reddot.SetActive(condition.Invoke());
    }
}
