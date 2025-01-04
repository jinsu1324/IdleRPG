using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextManager : SingletonBase<DamageTextManager>
{
    [SerializeField] private ObjectPool _damageTextPool;         // 데미지 텍스트 풀
    [SerializeField] private ObjectPool _criticalDamageTextPool; // 크리티컬 데미지 텍스트 풀

    /// <summary>
    /// 데미지 테스트 표시
    /// </summary>
    public void ShowDamageText(float damage, Vector3 position, bool isCritical)
    {
        DamageText damageText = null;

        if (isCritical) // 크리 여부에 따라 다른 텍스트프리팹 풀에서 가져오기
            damageText = _criticalDamageTextPool.GetObj().GetComponent<DamageText>();
        else
            damageText = _damageTextPool.GetObj().GetComponent<DamageText>();

        damageText.transform.position = position; // 좌표 설정
        damageText.SetText(damage.ToString()); // 데미지 텍스트 설정
    }
}
