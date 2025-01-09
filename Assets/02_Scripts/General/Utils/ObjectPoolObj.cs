using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolObj : SerializedMonoBehaviour
{
    private Transform _parent; // 하이어라키에서 들어가있을 부모
    private Vector3 _originalLocalScale; // 원래 로컬스케일 저장 (부모가 바꼈을때 스케일 원래보존용)
    private Vector3 _originalPos;

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(Transform trans)
    {
        _parent = trans;
        _originalLocalScale = transform.localScale;
        _originalPos = transform.localPosition;
    }

    /// <summary>
    /// 스폰 (사용하기 위해 활성화)
    /// </summary>
    public virtual void Spawn()
    {
        // 켜기
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 다시 풀로 돌려보내기
    /// </summary>
    public virtual void BackTrans()
    {
        // 끄기
        gameObject.SetActive(false);

        // 부모도 원래 부모로 설정
        if (_parent != null)
            transform.SetParent(_parent);

        // 내 부모의, 자식 리스트에서 맨 첫번째 위치로 이동
        transform.SetAsFirstSibling();

        // 스케일도 원래 스케일대로 리셋
        transform.localScale = _originalLocalScale;

        transform.localPosition = _originalPos;
    }

    /// <summary>
    /// 일정시간 후에 다시 풀로 돌려보내기
    /// </summary>
    public virtual void BackTrans_AfterTime(float time)
    {
        Invoke("BackTrans", time);
    }

    /// <summary>
    /// 스케일을 오리지날 스케일로 셋팅
    /// </summary>
    public void SetScale_ToOriginalScale()
    {
        transform.localScale = _originalLocalScale;
    }
}
