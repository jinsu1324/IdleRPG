using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageTextManager : SingletonBase<DamageTextManager>
{
    [SerializeField] private DamageText _damageTextPrefab;          // 데미지 텍스트 프리팹
    private ObjectPool<DamageText> _damageTextPool;                 // 데미지 텍스트 풀

    [SerializeField] private DamageText _criticalDamageTextPrefab;  // 크리티컬 데미지 텍스트 프리팹
    private ObjectPool<DamageText> _criticalDamageTextPool;         // 크리티컬 데미지 텍스트 풀

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake(); // 싱글톤 먼저

        _damageTextPool = new ObjectPool<DamageText>(_damageTextPrefab, 10, transform); // 풀 생성
        _criticalDamageTextPool = new ObjectPool<DamageText>(_criticalDamageTextPrefab, 10, transform); // 크리 풀 생성
    }

    /// <summary>
    /// 데미지 테스트 표시
    /// </summary>
    public void ShowDamageText(float damage, Vector3 position, bool isCritical)
    {
        DamageText damageText = null;

        if (isCritical) // 크리 여부에 따라 다른 텍스트프리팹 풀에서 가져오기
            damageText = _criticalDamageTextPool.GetObject();
        else
            damageText = _damageTextPool.GetObject();
        
        damageText.transform.position = position; // 좌표 설정
        damageText.SetText(damage.ToString(), _damageTextPool); // 데미지 텍스트 설정
    }
}
