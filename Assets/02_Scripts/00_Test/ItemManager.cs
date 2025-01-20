using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class ItemManager
{
    private static Dictionary<string, ItemDataSO> _itemDataSODict = new Dictionary<string, ItemDataSO>(); // 아이템데이터 스크립터블 딕셔너리
    private static bool _isLoaded = false;  // 로드 되었는지?

    /// <summary>
    /// Addressables를 통해 ItemDataSO를 로드
    /// </summary>
    public static async Task LoadItemDataAsync()
    {
        // 이미 로드된 경우 중복 방지
        if (_isLoaded) return; 

        // "ItemData" 라벨을 사용해 모든 ItemDataSO 로드
        AsyncOperationHandle<IList<ItemDataSO>> handle = Addressables.LoadAssetsAsync<ItemDataSO>("ItemData", null);

        // 비동기 로드 대기
        await handle.Task; 

        // 완료되면 딕셔너리에 맵핑
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (ItemDataSO itemDataSO in handle.Result)
            {
                if (!_itemDataSODict.ContainsKey(itemDataSO.ID))
                    _itemDataSODict[itemDataSO.ID] = itemDataSO;
            }
            _isLoaded = true;
            Debug.Log("모든 ItemDataSO 로드가 완료되었습니다!!");
        }
        else
            Debug.Log("ItemDataSO 로드가 실패하였습니다.");
    }

    /// <summary>
    /// 특정 아이템타입에 맞는 모든 ItemDataSO 가져오기
    /// </summary>
    public static List<ItemDataSO> GetItemDataSOList_ByType(ItemType itemType)
    {
        List<ItemDataSO> result = new List<ItemDataSO>();

        foreach (ItemDataSO itemDataSO in _itemDataSODict.Values)
        {
            if (itemDataSO.ItemType == itemType.ToString())
                result.Add(itemDataSO);
        }

        return result;
    }

    /// <summary>
    /// ID 에맞는 ItemDataSO 가져오기
    /// </summary>
    public static ItemDataSO GetItemDataSO(string id)
    {
        if (_itemDataSODict.TryGetValue(id, out ItemDataSO itemDataSO))
            return itemDataSO;

        Debug.Log($"{id} 에 맞는 ItemDataSO를 찾을 수 없습니다.");
        return null;
    }

    /// <summary>
    /// 해당 아이템이 강화가능한지?
    /// </summary>
    public static bool CanEnhance(Item item)
    {
        ItemDataSO itemDataSO = GetItemDataSO(item.ID);
        return item.Level >= itemDataSO.GetEnhanceCount(item.Level);
    }
}
