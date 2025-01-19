using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingletonBase<ItemManager>
{
    [SerializeField] private List<ItemDataSO> _itemDataSOList;  // ������ ������ ��ũ���ͺ� ������Ʈ ����Ʈ

    /// <summary>
    /// ������ ��ȭ ��������?
    /// </summary>
    public bool CanEnhance(Item item)
    {
        ItemDataSO itemDataSO = _itemDataSOList.Find(x => x.ID == item.ID);

        if (itemDataSO == null)
        {
            Debug.Log($"{item.ID}�� �ش��ϴ� ������ ������ ��ũ���ͺ� ������Ʈ�� ã�� �� �����ϴ�.");
            return false;
        }

        return item.Count >= itemDataSO.GetEnhanceCount(item.Level);
    }


    /// <summary>
    /// ������ ���� ��ȭ������ŭ ����
    /// </summary>
    public void ReduceCountByEnhance(Item item)
    {
        
    }
}
