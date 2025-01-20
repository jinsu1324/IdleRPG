using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class ItemManager
{
    private static Dictionary<string, ItemDataSO> _itemDataSODict = new Dictionary<string, ItemDataSO>(); // �����۵����� ��ũ���ͺ� ��ųʸ�
    private static bool _isLoaded = false;  // �ε� �Ǿ�����?

    /// <summary>
    /// Addressables�� ���� ItemDataSO�� �ε�
    /// </summary>
    public static async Task LoadItemDataAsync()
    {
        // �̹� �ε�� ��� �ߺ� ����
        if (_isLoaded) return; 

        // "ItemData" ���� ����� ��� ItemDataSO �ε�
        AsyncOperationHandle<IList<ItemDataSO>> handle = Addressables.LoadAssetsAsync<ItemDataSO>("ItemData", null);

        // �񵿱� �ε� ���
        await handle.Task; 

        // �Ϸ�Ǹ� ��ųʸ��� ����
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (ItemDataSO itemDataSO in handle.Result)
            {
                if (!_itemDataSODict.ContainsKey(itemDataSO.ID))
                    _itemDataSODict[itemDataSO.ID] = itemDataSO;
            }
            _isLoaded = true;
            Debug.Log("��� ItemDataSO �ε尡 �Ϸ�Ǿ����ϴ�!!");
        }
        else
            Debug.Log("ItemDataSO �ε尡 �����Ͽ����ϴ�.");
    }

    /// <summary>
    /// Ư�� ������Ÿ�Կ� �´� ��� ItemDataSO ��������
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
    /// ID ���´� ItemDataSO ��������
    /// </summary>
    public static ItemDataSO GetItemDataSO(string id)
    {
        if (_itemDataSODict.TryGetValue(id, out ItemDataSO itemDataSO))
            return itemDataSO;

        Debug.Log($"{id} �� �´� ItemDataSO�� ã�� �� �����ϴ�.");
        return null;
    }

    /// <summary>
    /// �ش� �������� ��ȭ��������?
    /// </summary>
    public static bool CanEnhance(Item item)
    {
        ItemDataSO itemDataSO = GetItemDataSO(item.ID);
        return item.Level >= itemDataSO.GetEnhanceCount(item.Level);
    }
}
