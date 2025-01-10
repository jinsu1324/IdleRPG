using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI_Consume : ItemDetailUI
{
    [Title("������ ��ư", bold: false)]
    [SerializeField] private Button _exitButton;                        // ������ ��ư

    protected override void OnEnable()
    {
        ItemSlot.OnSlotSelected += Show; // ������ ���� ���õǾ��� ��, ���õ� ������ ���� UI �ѱ�

    }

    protected override void OnDisable()
    {
        ItemSlot.OnSlotSelected -= Show;


    }

    public override void Show(Item item)
    {
        base.Show(item);


    }

    public override void Hide()
    {
        base.Hide();
    }
}
