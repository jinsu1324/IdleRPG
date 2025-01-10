using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI_Consume : ItemDetailUI
{
    [Title("나가기 버튼", bold: false)]
    [SerializeField] private Button _exitButton;                        // 나가기 버튼

    protected override void OnEnable()
    {
        ItemSlot.OnSlotSelected += Show; // 아이템 슬롯 선택되었을 때, 선택된 아이템 정보 UI 켜기

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
