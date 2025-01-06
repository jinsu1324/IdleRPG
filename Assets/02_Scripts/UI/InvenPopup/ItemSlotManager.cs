using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���Ե� ����
/// </summary>
public class ItemSlotManager : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> _ItemSlotList;              // ������ ���� ����Ʈ
    private ItemSlot _selectItemSlot;                                   // ���õ� ������ ����

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemSlot.OnSlotSelected += TurnON_SelectSlotHighlight; // ������ ���õǾ��� ��, ���õ� ���� ���̶���Ʈ �ѱ�
        SelectItemInfoUI.OnSelectItemInfoUIOFF += TryClear_SelectItemSlot; // ���� ������ ���� UI �� ������ ��, ���õ� ���� ���̶���Ʈ�� ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemSlot.OnSlotSelected -= TurnON_SelectSlotHighlight;
        SelectItemInfoUI.OnSelectItemInfoUIOFF -= TryClear_SelectItemSlot;
    }

    /// <summary>
    /// ������ ���Ե� �ʱ�ȭ
    /// </summary>
    public void Init(ItemType itemType)
    {
        TryClear_SelectItemSlot();

        List<IItem> itemInven = ItemInven.GetItemInvenByItemType(itemType); // ������Ÿ�Կ� �´� ������ �κ��丮 ������

        for (int i = 0; i < _ItemSlotList.Count; i++)
        {
            if (itemInven == null)  // ������ �κ� ������ ���� ���� �� ���⸸
            {
                _ItemSlotList[i].Clear();
                continue;
            }

            if (i < itemInven.Count)
                _ItemSlotList[i].Init(itemInven[i]);
            else
                _ItemSlotList[i].Clear();
        }
    }

    /// <summary>
    /// ���õ� ������ ������ ���� ����
    /// </summary>
    public void TryClear_SelectItemSlot()
    {
        if (_selectItemSlot != null)
        {
            _selectItemSlot.Highlight_OFF();
            _selectItemSlot = null;
        }
    }

    /// <summary>
    /// ���õ� ���� ���̶���Ʈ �ѱ�
    /// </summary>
    public void TurnON_SelectSlotHighlight(ItemSlot newSelectSlot)
    {
        // �̹� ���õ� ������ �ٽ� Ŭ������ ��� ����
        if (_selectItemSlot == newSelectSlot)
            return;

        // ������ ���õ� ������ �ִٸ�, �� ������ ���̶���Ʈ OFF
        if (_selectItemSlot != null)
            _selectItemSlot.Highlight_OFF();

        // �� �������� ��ü �� ���̶���Ʈ ON
        _selectItemSlot = newSelectSlot;
        _selectItemSlot.Highlight_ON();
    }

    /// <summary>
    /// ������ ���� ����Ʈ ������Ʈ
    /// </summary>
    public void UpdateItemSlotList()
    {
        foreach (ItemSlot itemSlot in _ItemSlotList)
            itemSlot.UpdateItemSlot();
    }
}
