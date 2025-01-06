using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 장착 슬롯
/// </summary>
[RequireComponent(typeof(ReddotComponent))]
public class EquipSlot : MonoBehaviour
{
    [Title("장착슬롯의 아이템타입", bold: false)]
    [SerializeField] private ItemType _itemType;                // 장착슬롯의 아이템타입

    [Title("On Off GO", bold: false)]
    [SerializeField] private GameObject _emptyGO;               // 빈슬롯 GO
    [SerializeField] private GameObject _infoGO;                // 정보들 GO

    [Title("정보들", bold: false)]
    [SerializeField] private Image _itemIcon;                   // 아이템 아이콘
    [SerializeField] private Image _gradeFrame;                 // 등급 프레임
    [SerializeField] private TextMeshProUGUI _levelText;        // 아이템 레벨 텍스트

    [Title("레드닷 컴포넌트 (장비만 업데이트됨)", bold: false)]
    [SerializeField] private ReddotComponent _reddotComponent;  // 레드닷 컴포넌트

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemInven.OnItemInvenChanged += UpdateReddotComponent; // 가지고 있는 아이템이 변경되었을 때, 장착슬롯 레드닷 업데이트
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemInven.OnItemInvenChanged -= UpdateReddotComponent;
    }

    /// <summary>
    /// 정보 보여주기
    /// </summary>
    public void ShowInfoGO(IItem item)
    {
        _itemIcon.sprite = item.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(item.Grade);
        _levelText.text = $"Lv.{item.Level}";

        _emptyGO.SetActive(false);
        _infoGO.SetActive(true); // 정보 켜기

        UpdateReddotComponent(); // 레드닷 업데이트
    }

    /// <summary>
    /// 빈슬롯 보여주기
    /// </summary>
    public void ShowEmptyGO()
    {
        _emptyGO.SetActive(true);
        _infoGO.SetActive(false); // 정보 끄기

        UpdateReddotComponent(); // 레드닷 업데이트
    }

    /// <summary>
    /// 레드닷 컴포넌트 업데이트
    /// </summary>
    public void UpdateReddotComponent()
    {
        switch (_itemType)
        {
            case ItemType.Weapon: // 장비들은 인벤토리에 강화가능한 아이템이 있는지? 레드닷 업데이트
            case ItemType.Armor:
            case ItemType.Helmet:
                _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(_itemType)); 
                break;
            case ItemType.Skill: // 스킬은 그냥 레드닷 숨기기
                _reddotComponent.Hide();
                break;
        }
    }
}
