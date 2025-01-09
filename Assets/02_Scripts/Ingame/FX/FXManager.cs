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
    UIFX_UpgradeItem
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
    public void SpawnFX(FXName fxName, Transform spawnPos)
    {
        if (_fxPoolDict.TryGetValue(fxName, out ObjectPool pool))
        {
            GameObject fx = pool.GetObj();  // 가져오기
            fx.transform.position = spawnPos.position;   // 위치 설정
        }
        else
        {
            Debug.Log($"{fxName} 이펙트 풀을 찾을 수 없습니다.");
        }
    }

    /// <summary>
    /// 이펙트 지정위치에 스폰하고 부모도 새로 설정 (UI같은곳에 사용)
    /// </summary>
    public void SpawnFX_NewParent(FXName fxName, Transform newParent)
    {
        if (_fxPoolDict.TryGetValue(fxName, out ObjectPool pool))
        {
            GameObject fx = pool.GetObj();  // 가져오기
            fx.transform.position = newParent.position; // 위치 설정   
            
            fx.gameObject.transform.SetParent(newParent);   // 새로운 부모로 부모를 설정 (내부의 원래부모(pool)는 그대로있음)
            fx.GetComponent<ObjectPoolObj>().SetScale_ToOriginalScale();    // 부모가 바뀌었기 때문에 스케일이 바뀌니까, 원래 의도한 스케일대로 한번 더 셋팅
        }
        else
        {
            Debug.Log($"{fxName} 이펙트 풀을 찾을 수 없습니다.");
        }
    }
}
