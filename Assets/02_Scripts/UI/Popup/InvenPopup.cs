using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenPopup : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> _ItemSlotList;              // ������ ���� ����Ʈ
    [SerializeField] private SelectItemInfo _selectItemInfo;            // ���õ� ������ ���� UI
    [SerializeField] private Button _exitButton;                        // ������ ��ư
    private ItemSlot _selectItemSlot;                                   // ���õ� ������ ����

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public void Show(ItemType itemType)
    {
        if (_selectItemSlot)
        {
            Debug.Log("���õ� �������� �־��!");
        }
        else
        {
            Debug.Log("���õ� �������� ��!!!!!!!!���!");

        }


        ItemSlot.OnSlotClickedAction += TurnON_SelectSlotHighlight; // ������ Ŭ���Ǿ��� ��, ���õ� ���� ���̶���Ʈ �ѱ�
        ItemSlot.OnSlotClickedAction += Show_SelectItemInfo; // ������ Ŭ���Ǿ��� ��, ���õ� ������ ���� �����ֱ�
        
        Init_ItemSlotList(itemType);    // ������ ���Ե� �ʱ�ȭ
        
        _exitButton.onClick.AddListener(Hide);  // ������ ��ư ������ �˾�����

        gameObject.SetActive(true);
    }


    /// <summary>
    /// ������ ���Ե� �ʱ�ȭ
    /// </summary>
    private void Init_ItemSlotList(ItemType itemType)
    {
        // �̹� ���õ� ������ ������
        if (_selectItemSlot != null)
        {
            // ���̶���Ʈ ����
            TurnOFF_SelectSlotHighlight();

            // ������ ������ ����UI ����
            Hide_SelectItemInfo();

            Debug.Log("�׷��� ���ݾ��!");

            // ������ ���� null���� �ʱ�ȭ
            _selectItemSlot = null;
        }

        // ������Ÿ�Կ� �´� ������ �κ��丮 ������
        List<Item> itemInven = ItemInven.GetItemInvenByItemType(itemType);

        // �������κ��� ������, �� �󽽷����� ����� ����
        if (itemInven == null)
        {
            for (int i = 0; i < _ItemSlotList.Count; i++)
            {
                _ItemSlotList[i].OFF_ItemSlotInfo();   // �������� �󽽷����� �α�
            }

            return;
        }


        for (int i = 0; i < _ItemSlotList.Count; i++)
        {
            if (i < itemInven.Count)
            {
                _ItemSlotList[i].Init_ItemSlotInfo(itemInven[i]); // ������ ���� ������ŭ �����۽��� �ʱ�ȭ
            }
            else
            {
                _ItemSlotList[i].OFF_ItemSlotInfo();   // �������� ����
            }
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
    /// ���õ� ���� ���̶���Ʈ ����
    /// </summary>
    public void TurnOFF_SelectSlotHighlight()
    {
        _selectItemSlot.Highlight_OFF();
    }

    /// <summary>
    /// ���õ� ������ ���� �ѱ�
    /// </summary>
    private void Show_SelectItemInfo(ItemSlot newSelectSlot)
    {
        _selectItemInfo.Init(newSelectSlot);
    }

    /// <summary>
    /// ���õ� ������ ���� ����
    /// </summary>
    private void Hide_SelectItemInfo()
    {
        _selectItemInfo.HideUI();
    }

    /// <summary>
    /// �˾� ����
    /// </summary>
    public void Hide()
    {
        ItemSlot.OnSlotClickedAction -= TurnON_SelectSlotHighlight;
        ItemSlot.OnSlotClickedAction -= Show_SelectItemInfo;

        gameObject.SetActive(false);
    }
}
