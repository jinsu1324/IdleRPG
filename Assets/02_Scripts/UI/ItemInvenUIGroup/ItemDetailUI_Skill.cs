using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스킬아이템 상세정보 UI
/// </summary>
public class ItemDetailUI_Skill : ItemDetailUI
{
    [Title("스킬아이템 상세정보 UI", bold: false)]
    [SerializeField] protected TextMeshProUGUI _descText;               // 상세설명 텍스트
    [SerializeField] private TextMeshProUGUI _levelText;                // 레벨 텍스트
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;     // 강화 가능한 아이템 갯수 텍스트
    [SerializeField] private Slider _countSlider;                       // 갯수 표시 슬라이더
    [SerializeField] private GameObject _equipIcon;                     // 장착 아이콘
    [SerializeField] private GameObject _enhanceableArrowIcon;          // 강화가능 아이콘

    [Title("버튼들", bold: false)]
    [SerializeField] private Button _equipButton;                       // 장착 버튼
    [SerializeField] private Button _unEquipButton;                     // 장착 해제 버튼
    [SerializeField] private Button _enhanceButton;                     // 강화 버튼

    [Title("나가기 버튼", bold: false)]
    [SerializeField] private Button _exitButton;                        // 나가기 버튼

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _equipButton.onClick.AddListener(OnClick_EquipButton);      // 장착버튼 핸들러 등록
        _unEquipButton.onClick.AddListener(OnClick_UnEquipButton);  // 장착해제버튼 핸들러 등록
        _enhanceButton.onClick.AddListener(OnClick_EnhanceButton);  // 강화버튼 핸들러 등록
        _exitButton.onClick.AddListener(() => Hide());              // 나가기 버튼 핸들러 등록
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _equipButton.onClick.RemoveAllListeners();
        _unEquipButton.onClick.RemoveAllListeners();
        _enhanceButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    protected override void UpdateUI(Item item)
    {
        base.UpdateUI(item);
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(item.ID);

        _descText.text = itemDataSO.Desc; // Todo Diynamic 텍스트로 해야함
        _levelText.text = $"Lv.{item.Level}";
        _countText.text = $"{item.Count}";
        _enhanceableCountText.text = $"{itemDataSO.GetEnhanceCount(item.Level)}";
        _countSlider.value = (float)item.Level / (float)itemDataSO.GetEnhanceCount(item.Level);
        _enhanceableArrowIcon.gameObject.SetActive(ItemEnhanceManager.CanEnhance(item));
        _enhanceButton.gameObject.SetActive(ItemEnhanceManager.CanEnhance(item));

        bool isEquipped = EquipSkillManager.IsEquipped(item);
        _equipButton.gameObject.SetActive(!isEquipped);
        _unEquipButton.gameObject.SetActive(isEquipped);
        _equipIcon.SetActive(isEquipped);
    }

    /// <summary>
    /// 장착 버튼 클릭
    /// </summary>
    public void OnClick_EquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipSkillManager.Equip(CurrentItem);
    }

    /// <summary>
    /// 장착 해제 버튼 클릭
    /// </summary>
    public void OnClick_UnEquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipSkillManager.UnEquip(CurrentItem);
    }

    /// <summary>
    /// 강화 버튼 클릭
    /// </summary>
    public void OnClick_EnhanceButton()
    {
        if (CurrentItem == null)
            return;

        ItemEnhanceManager.Enhance(CurrentItem);
    }
}
