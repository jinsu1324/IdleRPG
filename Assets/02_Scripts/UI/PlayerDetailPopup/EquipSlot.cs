using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 장착 슬롯
/// </summary>
public class EquipSlot : MonoBehaviour
{
    [Title("정보들 전체부모 GO", bold: false)]
    [SerializeField] private GameObject _infoParentGO;          // 정보들 전체부모 GO

    [Title("아이템 정보들", bold: false)]
    [SerializeField] private Image _itemIcon;                   // 아이템 아이콘
    [SerializeField] private Image _gradeFrame;                 // 등급 프레임
    [SerializeField] private TextMeshProUGUI _levelText;        // 아이템 레벨 텍스트

    [Title("레드닷 컴포넌트", bold: false)]
    [SerializeField] private ReddotComponent _reddotComponent;  // 레드닷 컴포넌트

    /// <summary>
    /// 장착슬롯 보여주기
    /// </summary>
    public void Show(Item item, ItemType itemType)
    {
        _itemIcon.sprite = item.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(item.Grade);
        _levelText.text = $"Lv.{item.Level}";

        Update_ReddotComponent(itemType);

        _infoParentGO.SetActive(true);
    }

    /// <summary>
    /// 장착슬롯 끄기
    /// </summary>
    public void Hide(ItemType itemType)
    {
        Update_ReddotComponent(itemType);

        _infoParentGO.SetActive(false);
    }

    /// <summary>
    /// 레드닷 컴포넌트 업데이트
    /// </summary>
    public void Update_ReddotComponent(ItemType itemType)
    {
        _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(itemType));
    }
}
