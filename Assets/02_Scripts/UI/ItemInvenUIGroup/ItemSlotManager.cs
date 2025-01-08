using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ���Ե� ����
/// </summary>
public class ItemSlotManager : MonoBehaviour
{
    [Title("������ ���� ����Ʈ", bold: false)]
    [SerializeField] private List<ItemSlot> _ItemSlotList;              // ������ ���� ����Ʈ

    [Title("���� ���̶���Ʈ �̹���", bold: false)]
    [SerializeField] private RectTransform _highlightImage;             // ���� ���̶���Ʈ �̹���

    /// <summary>
    /// ������ ���Ե� �ʱ�ȭ
    /// </summary>
    public void Init(ItemType itemType)
    {
        List<IItem> itemInven = ItemInven.GetItemInvenByItemType(itemType); // ������Ÿ�Կ� �´� ������ �κ��丮 ������

        for (int i = 0; i < _ItemSlotList.Count; i++)
        {
            if (itemInven == null)  // ������ �κ� ������ ���� ���� �� ���⸸
            {
                _ItemSlotList[i].Clear();
                continue;
            }

            if (i < itemInven.Count)
                _ItemSlotList[i].Init(itemInven[i], SnapHighlightImage);
            else
                _ItemSlotList[i].Clear();
        }

        HideHighlightImage(); // ���̶���Ʈ �̹��� �����
    }

    /// <summary>
    /// ���̶���Ʈ �̹��� ����
    /// </summary>
    public void SnapHighlightImage(RectTransform targetRect)
    {
        _highlightImage.SetParent(targetRect);
        _highlightImage.position = targetRect.position;
        _highlightImage.gameObject.SetActive(true);
    }

    /// <summary>
    /// ���̶���Ʈ �̹��� �����
    /// </summary>
    public void HideHighlightImage()
    {
        _highlightImage.gameObject.SetActive(false);
    }
}
