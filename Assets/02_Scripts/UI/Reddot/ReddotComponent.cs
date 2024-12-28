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
        if (_reddot == null)
        {
            Debug.LogWarning($"[{gameObject.name}] RedDot GameObject가 설정되지 않았습니다.");
            return;
        }

        if (condition == null)
        {
            Debug.LogWarning($"[{gameObject.name}] 활성화 조건이 설정되지 않았습니다.");
            _reddot.SetActive(false);
            return;
        }

        _reddot.SetActive(condition.Invoke());
    }
}
