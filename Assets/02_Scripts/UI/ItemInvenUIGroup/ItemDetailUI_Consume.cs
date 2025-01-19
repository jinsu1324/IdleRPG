using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 소비아이템 상세정보 UI
/// </summary>
public class ItemDetailUI_Consume : ItemDetailUI
{
    [Title("소비아이템 정보들", bold: false)]
    [SerializeField] protected TextMeshProUGUI _descText;               // 상세설명 텍스트

    [Title("나가기 버튼", bold: false)]
    [SerializeField] private Button _exitButton;    // 나가기 버튼

    protected override void UpdateUI(Item item)
    {
        //base.UpdateUI(item);

        //// 상세설명 업데이트
        //_descText.text = item.Desc;
    }
}
