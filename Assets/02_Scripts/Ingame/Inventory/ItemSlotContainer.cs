using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���Ե��� �����ϰ� ���� �����̳�
/// </summary>
public class ItemSlotContainer : SingletonBase<ItemSlotContainer>
{
    [SerializeField]
    private List<ItemSlot> _ItemSlotList;                     // ������ ���� ����Ʈ
    private ItemSlot _selectedItemSlot;                       // ���õ� ������ ����



    [SerializeField]
    private SelectItemInfoPanel _selectedItemInfoPanel;                 // ���õ� ������ ���� �г�





    public List<ItemSlot> GetItemSlotList() => _ItemSlotList;    // ������ ���� ����Ʈ  ��������



    /// <summary>
    /// ���õ� ���� ���̶�����
    /// </summary>
    public void HighlightingSelectdSlot(ItemSlot newSelectedSlot)
    {
        // �̹� ���õ� ������ �ٽ� Ŭ������ ��� ����
        if (_selectedItemSlot == newSelectedSlot)
            return;

        // ������ ���õ� ������ �ִٸ�, �� ������ ���̶���Ʈ OFF
        if (_selectedItemSlot != null)
            _selectedItemSlot.SelectFrameOFF();

        // �� �������� ��ü �� ���̶���Ʈ ON
        _selectedItemSlot = newSelectedSlot;
        _selectedItemSlot.SelectFrameON();

        // ���õ� ������ ���� �г� �ѱ�
        SelectedItemInfoPanel_ON();
    }



    /// <summary>
    /// ���õ� ������ ���� �г� �ѱ�
    /// </summary>
    public void SelectedItemInfoPanel_ON()
    {
        _selectedItemInfoPanel.OpenAndInit(_selectedItemSlot);
    }

}
