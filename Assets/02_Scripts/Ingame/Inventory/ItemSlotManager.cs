using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotManager : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> _ItemSlotList;              // ������ ���� ����Ʈ
    private ItemSlot _selectItemSlot;                                   // ���õ� ������ ����

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        ItemSlot.OnSlotSelected += TurnON_SelectSlotHighlight; // ������ ���õǾ��� ��, ���õ� ���� ���̶���Ʈ �ѱ�
    }

    /// <summary>
    /// ������ ���Ե� �ʱ�ȭ
    /// </summary>
    public void Init(ItemType itemType)
    {
        TryClear_SelectItemSlot();

        List<Item> itemInven = ItemInven.GetItemInvenByItemType(itemType); // ������Ÿ�Կ� �´� ������ �κ��丮 ������

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
    /// OnDestroy
    /// </summary>
    private void OnDestroy()
    {
        ItemSlot.OnSlotSelected -= TurnON_SelectSlotHighlight;
    }
}
