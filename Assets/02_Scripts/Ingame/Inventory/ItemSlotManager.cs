using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ ���Ե��� �������� �Ŵ���
/// </summary>
public class ItemSlotManager : MonoBehaviour
{
    [SerializeField]
    private List<ItemSlot> _ItemSlotList;                   // ������ ���� ����Ʈ
    private ItemSlot _selectItemSlot;                       // ���õ� ������ ����

    [SerializeField]
    private SelectItemInfoPanel _selectItemInfoPanel;       // ���õ� ������ ���� �г�

    /// <summary>
    /// ������ ���� ����Ʈ ��������
    /// </summary>
    public List<ItemSlot> GetItemSlotList()
    {
        return _ItemSlotList;
    }

    /// <summary>
    /// ���õ� ���� ���̶�����
    /// </summary>
    public void HighlightingSelectdSlot(ItemSlot newSelectedSlot)
    {
        // �̹� ���õ� ������ �ٽ� Ŭ������ ��� ����
        if (_selectItemSlot == newSelectedSlot)
            return;

        // ������ ���õ� ������ �ִٸ�, �� ������ ���̶���Ʈ OFF
        if (_selectItemSlot != null)
            _selectItemSlot.SelectFrameOFF();

        // �� �������� ��ü �� ���̶���Ʈ ON
        _selectItemSlot = newSelectedSlot;
        _selectItemSlot.SelectFrameON();

        // ���õ� ������ ���� �г� �ѱ�
        SelectItemInfoPanel_ON();
    }



    /// <summary>
    /// ���õ� ������ ���� �г� �ѱ�
    /// </summary>
    public void SelectItemInfoPanel_ON()
    {
        _selectItemInfoPanel.OpenAndInit(_selectItemSlot);
    }

}
