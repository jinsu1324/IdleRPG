using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquipManager : MonoBehaviour
{
    [SerializeField] private List<Image> _equipItemIconList;        // 장착한 아이템 아이콘 리스트 (최대 3개)

    private List<Item> _equipItemList = new List<Item>();           // 장착한 아이템 리스트
    private int _maxCount = 3;                                      // 최대 장착 가능한 아이템 개수


    /// <summary>
    /// 장착
    /// </summary>
    public void Equip(Item item)
    {
        // 최대 장착갯수를 초과했으면 그냥 리턴
        if (CanEquipMore() == false)
        {
            Debug.Log("장착 가능한 최대 아이템 개수를 초과했습니다!");
            return;
        }

        // 이미 장착된 아이템이면 그냥 리턴
        if (IsEquipped(item))
        {
            Debug.Log("이미 장착된 아이템입니다!");
            return;
        }

        // 장착아이템 리스트에 추가
        AddEquipItemList(item);

        // 플레이어 스탯에 아이템 스탯들 추가
        PlayerStats.Instance.AddItemStatsToPlayer(item.GetStatDictionaryByLevel(), item);

        // 슬롯에 장착 아이콘 ON
        ItemSlotFinder.FindSlot_ContainItem(item, InventoryManager.Instance.GetItemSlotManager(item.ItemType).GetItemSlotList()).EquippedIconON();

        

        // 장착 아이콘 리스트 UI 업데이트
        Update_EquippedItemIconListUI();

        // 스탯 보여주는 UI 업데이트
        PlayerStats.Instance.AllStatUIUpdate();
    }

    /// <summary>
    /// 장착 해제
    /// </summary>
    public void UnEquip(Item item)
    {
        // 해제하려는 아이템이 장착되지 않았으면 그냥 리턴
        if (IsEquipped(item) == false)
        {
            Debug.Log("해제하려는 아이템이 장착되지 않았습니다!");
            return;
        }

        // 장착 아이템 리스트에서 제거
        RemoveEquipItemList(item);

        // 플레이어 스탯에서 아이템 스탯들 제거
        PlayerStats.Instance.RemoveItemStatsToPlayer(item.GetStatDictionaryByLevel(), item);

        // 슬롯에 장착 아이콘 OFF
        ItemSlotFinder.FindSlot_ContainItem(item, InventoryManager.Instance.GetItemSlotManager(item.ItemType).GetItemSlotList()).EquippedIconOFF();

        // 장착 아이콘 리스트 UI 업데이트
        Update_EquippedItemIconListUI();

        // 스탯 보여주는 UI 업데이트
        PlayerStats.Instance.AllStatUIUpdate();
    }

    /// <summary>
    /// 더 장착할 수 있는지?
    /// </summary>
    public bool CanEquipMore()
    {
        return _equipItemList.Count < _maxCount;
    }

    /// <summary>
    /// 해당 아이템이 장착된 아이템인지?
    /// </summary>
    public bool IsEquipped(Item item)
    {
        return _equipItemList.Contains(item);
    }

    /// <summary>
    /// 장착 아이템 리스트에 추가
    /// </summary>
    public void AddEquipItemList(Item item)
    {
        _equipItemList.Add(item);
    }

    /// <summary>
    /// 장착 아이템 리스트에서 제거
    /// </summary>
    public void RemoveEquipItemList(Item item)
    {
        _equipItemList.Remove(item);
    }

    /// <summary>
    /// 장착한 아이템 아이콘 리스트 UI 업데이트
    /// </summary>
    public void Update_EquippedItemIconListUI()
    {
        for (int i = 0; i < _equipItemIconList.Count; i++)   // 3개만큼 반복 (아이콘 슬롯 최대 갯수)
        {
            if (i < _equipItemList.Count) // 가진 아이템 갯수까지는 아이콘 표시
            {
                _equipItemIconList[i].sprite = _equipItemList[i].Icon;
                _equipItemIconList[i].gameObject.SetActive(true);
            }
            else // 나머지는 비활성화
            {
                _equipItemIconList[i].sprite = null;
                _equipItemIconList[i].gameObject.SetActive(false);
            }
        }
    }
}
