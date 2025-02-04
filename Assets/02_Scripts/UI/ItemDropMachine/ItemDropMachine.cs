using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// ������ ȹ�� ���
/// </summary>
public class ItemDropMachine : MonoBehaviour
{
    public static event Action<List<Item>> OnDroppedItem;   // ������ ��� �Ϸ�Ǿ����� �̺�Ʈ
    public static event Action<bool> OnDimdUpdate;  // ���� ������Ʈ�Ҷ� ����� �̺�Ʈ

    [Title("��� ����", bold: false)]
    [SerializeField] 
    [Range(1, 10)] 
    private int _maxDropCount;

    [Title("����� ������ Ÿ��", bold: false)]
    [SerializeField] 
    private ItemType _itemType;

    [Title("��� ���", bold: false)]
    [SerializeField]
    private int _dropCost;

    [Title("��� ��ư", bold: false)]
    [SerializeField]
    private ItemDropButton _dropButton;




    // �Ұ��� �佺Ʈ�޽���




    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _dropButton.ButtonAddListener(OnClickDropItem); // ��ư�� ������ ����Լ� ����
        _dropButton.UpdateDimd(GemManager.HasEnoughGem(_dropCost)); // ��ư ���� ������Ʈ
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _dropButton.ButtonRemoveListener(); // ��ư ������ ����
    }

    /// <summary>
    /// ������ ��� (��ư����)
    /// </summary>
    public void OnClickDropItem()
    {
        // �� �����ϸ� �׳� ����
        if (GemManager.HasEnoughGem(_dropCost) == false)
        {
            ToastManager.Instance.StartShow_ToastCommon("������ �����մϴ�."); // �佺Ʈ �޽���
            return;
        }

        GemManager.ReduceGem(_dropCost); // �� ����

        List<Item> dropItemList = new List<Item>(); 
        for (int i = 0; i < _maxDropCount; i++)
        {
            Item item = CreateItem(); 
            ItemInven.AddItem(item);
            dropItemList.Add(item);
        }

        OnDroppedItem?.Invoke(dropItemList);

        OnDimdUpdate?.Invoke(GemManager.HasEnoughGem(_dropCost)); // ��ư ���� ������Ʈ
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    private Item CreateItem()
    {
        // �ش� ������Ÿ���� ��� ������ ��ũ���ͺ� ������Ʈ�� �߿��� 1���� ����
        List<ItemDataSO> itemDataSOList = ItemDataManager.GetItemDataSOList_ByType(_itemType);
        ItemDataSO itemDataSO = itemDataSOList[RandomIndex(itemDataSOList.Count)];

        Item item = new Item(itemDataSO.ID, itemDataSO.ItemType, 1, 1);
        return item;
    }

    /// <summary>
    /// 0-maxCount ���� ������ ���� ��ȯ
    /// </summary>
    private int RandomIndex(int maxCount)
    {
        int index = Random.Range(0, maxCount);
        return index;
    }
}
