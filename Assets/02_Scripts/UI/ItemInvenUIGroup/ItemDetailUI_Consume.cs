using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 소비아이템 상세정보 UI
/// </summary>
public class ItemDetailUI_Consume : ItemDetailUI
{
    [Title("나가기 버튼", bold: false)]
    [SerializeField] private Button _exitButton;    // 나가기 버튼

}
