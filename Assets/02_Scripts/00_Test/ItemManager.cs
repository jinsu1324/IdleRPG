using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonBase<ItemManager>
{
    [SerializeField] private List<ItemDataSO> _itemDataSOList;  // 아이템 데이터 스크립터블 오브젝트 리스트

    /// <summary>
    /// 아이템 강화 가능한지?
    /// </summary>
    public bool CanEnhance(Item item)
    {
        ItemDataSO itemDataSO = _itemDataSOList.Find(x => x.ID == item.ID);

        if (itemDataSO == null)
        {
            Debug.Log($"{item.ID}에 해당하는 아이템 데이터 스크립터블 오브젝트를 찾을 수 없습니다.");
            return false;
        }

        return item.Count >= itemDataSO.GetEnhanceCount(item.Level);
    }


    /// <summary>
    /// 아이템 갯수 강화갯수만큼 감소
    /// </summary>
    public void ReduceCountByEnhance(Item item)
    {
        
    }
}
