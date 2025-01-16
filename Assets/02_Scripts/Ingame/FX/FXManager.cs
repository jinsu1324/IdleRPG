using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이펙트 파일명을 키로 활용할 Enum ('FX프리팹파일명' 과 '해당enum' 은 이름이 동일해야함)
/// </summary>
public enum FXName
{
    FX_Player_Upgrade,
    FX_Enemy_Damaged,
    FX_Enemy_Die,
    UIFX_UpgradeItem,
    FX_Skill_Anger
}

/// <summary>
/// 이펙트 관리
/// </summary>
public class FXManager : SingletonBase<FXManager>
{
    [SerializeField] private List<ObjectPool> _fxPoolList;  // 이펙트 풀 리스트
    private Dictionary<FXName, ObjectPool> _fxPoolDict = new Dictionary<FXName, ObjectPool>();  // 이펙트 풀 딕셔너리

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Init_FXPoolDict();  //이펙트 풀 딕셔너리 초기화
    }

    /// <summary>
    /// 이펙트 풀 딕셔너리 초기화
    /// </summary>
    private void Init_FXPoolDict()
    {
        foreach (ObjectPool pool in _fxPoolList) 
        {
            FXName prefabName = (FXName)Enum.Parse(typeof(FXName), pool.GetPrefabName());
            _fxPoolDict[prefabName] = pool; // '프리팹 이름'을 키로 사용
        }
    }

    /// <summary>
    /// 이펙트 지정위치에 스폰
    /// </summary>
    public GameObject SpawnFX(FXName fxName, Transform spawnPos)
    {
        if (_fxPoolDict.TryGetValue(fxName, out ObjectPool pool))
        {
            GameObject fx = pool.GetObj();  // 가져오기
            fx.transform.position = spawnPos.position;   // 위치 설정
            return fx;
        }
        else
        {
            Debug.Log($"{fxName} 이펙트 풀을 찾을 수 없습니다.");
            return null;
        }
    }
}
