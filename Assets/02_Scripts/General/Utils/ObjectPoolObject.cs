using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolObject : SerializedMonoBehaviour
{
    // 하이어라키에서 들어가있을 부모
    private Transform _parent;

    // 부모 셋팅
    public void Setting(Transform trans)
    {
        _parent = trans;
    }


    // 스폰 (사용하기 위해 ON)
    public virtual void Spawn()
    {
        gameObject.SetActive(true);
    }

    // 사용안하게 되었을때 다시 돌려보내기
    public virtual void BackTrans()
    {
        gameObject.SetActive(false);

        if (_parent != null)
            transform.SetParent(_parent);

        transform.SetAsFirstSibling();
    }


    // 사용안하게 되었을때 일정시간 이후에 다시 돌려보내기
    public virtual void BackTrans_AfterTime(float time)
    {
        Invoke("BackTrans", time);
    }
}
