using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ʈ ���ϸ��� Ű�� Ȱ���� Enum ('FX���������ϸ�' �� '�ش�enum' �� �̸��� �����ؾ���)
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
/// ����Ʈ ����
/// </summary>
public class FXManager : SingletonBase<FXManager>
{
    [SerializeField] private List<ObjectPool> _fxPoolList;  // ����Ʈ Ǯ ����Ʈ
    private Dictionary<FXName, ObjectPool> _fxPoolDict = new Dictionary<FXName, ObjectPool>();  // ����Ʈ Ǯ ��ųʸ�

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Init_FXPoolDict();  //����Ʈ Ǯ ��ųʸ� �ʱ�ȭ
    }

    /// <summary>
    /// ����Ʈ Ǯ ��ųʸ� �ʱ�ȭ
    /// </summary>
    private void Init_FXPoolDict()
    {
        foreach (ObjectPool pool in _fxPoolList) 
        {
            FXName prefabName = (FXName)Enum.Parse(typeof(FXName), pool.GetPrefabName());
            _fxPoolDict[prefabName] = pool; // '������ �̸�'�� Ű�� ���
        }
    }

    /// <summary>
    /// ����Ʈ ������ġ�� ����
    /// </summary>
    public GameObject SpawnFX(FXName fxName, Transform spawnPos)
    {
        if (_fxPoolDict.TryGetValue(fxName, out ObjectPool pool))
        {
            GameObject fx = pool.GetObj();  // ��������
            fx.transform.position = spawnPos.position;   // ��ġ ����
            return fx;
        }
        else
        {
            Debug.Log($"{fxName} ����Ʈ Ǯ�� ã�� �� �����ϴ�.");
            return null;
        }
    }
}
