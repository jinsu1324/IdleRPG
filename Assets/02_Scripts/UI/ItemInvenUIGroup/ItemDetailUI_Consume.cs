using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Һ������ ������ UI
/// </summary>
public class ItemDetailUI_Consume : ItemDetailUI
{
    [Title("�Һ������ ������", bold: false)]
    [SerializeField] protected TextMeshProUGUI _descText;               // �󼼼��� �ؽ�Ʈ

    [Title("������ ��ư", bold: false)]
    [SerializeField] private Button _exitButton;    // ������ ��ư

    protected override void UpdateUI(Item item)
    {
        //base.UpdateUI(item);

        //// �󼼼��� ������Ʈ
        //_descText.text = item.Desc;
    }
}
