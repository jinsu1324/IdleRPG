using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 장비아이템 상세정보 UI
/// </summary>
public class ItemDetailUI_Gear : ItemDetailUI
{
    [Title("장비아이템 정보들", bold: false)]
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

    [Title("장비에 붙어있는 어빌리티들", bold: false)]
    [SerializeField] private List<ItemAbilityInfo> _abilityInfoList;    // 장비에 붙어있는 어빌리티들
    
    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipGearManager.OnEquipGear += UpdateUI; // 장비 장착할때 -> 장비아이템 상세정보 UI 업데이트
        EquipGearManager.OnUnEquipGear += UpdateUI; // 장비 해제할때 -> 장비아이템 상세정보 UI 업데이트
        ItemEnhanceManager.OnItemEnhance += UpdateUI; // 아이템 강화할때 -> 장비아이템 상세정보 UI 업데이트

        _equipButton.onClick.AddListener(OnClick_EquipButton);      // 장착버튼 핸들러 등록
        _unEquipButton.onClick.AddListener(OnClick_UnEquipButton);  // 장착해제버튼 핸들러 등록
        _enhanceButton.onClick.AddListener(OnClick_EnhanceButton);  // 강화버튼 핸들러 등록
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipGearManager.OnEquipGear -= UpdateUI;
        EquipGearManager.OnUnEquipGear -= UpdateUI;
        ItemEnhanceManager.OnItemEnhance -= UpdateUI;

        _equipButton.onClick.RemoveAllListeners();
        _unEquipButton.onClick.RemoveAllListeners();
        _enhanceButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    protected override void UpdateUI(Item item)
    {
        // 기본정보 업데이트
        base.UpdateUI(item);

        // 상세설명 업데이트
        _descText.text = item.Desc;

        // 강화관련정보 업데이트
        if (item is IEnhanceableItem enhanceableItem)
        {
            _levelText.text = $"Lv.{enhanceableItem.Level}";
            _countText.text = $"{item.Count}";
            _enhanceableCountText.text = $"{enhanceableItem.EnhanceableCount}";
            _countSlider.value = (float)CurrentItem.Count / (float)enhanceableItem.EnhanceableCount;
            _enhanceableArrowIcon.gameObject.SetActive(enhanceableItem.CanEnhance());
            _enhanceButton.gameObject.SetActive(enhanceableItem.CanEnhance());
        }

        // 어빌리티 정보 업데이트
        Update_AbilityInfo(item);

        // 장착관련정보 + 버튼들 업데이트
        bool isEquipped = EquipGearManager.IsEquipped(item);
        _equipButton.gameObject.SetActive(!isEquipped);
        _unEquipButton.gameObject.SetActive(isEquipped);
        _equipIcon.SetActive(isEquipped);

    }

    /// <summary>
    /// 어빌리티 정보 업데이트
    /// </summary>
    private void Update_AbilityInfo(Item item)
    {
        if (item is GearItem gearItem)
        {
            int index = 0;

            // 스탯 Dictionary 순회
            foreach (var kvp in gearItem.GetAttributeDict())
            {
                StatType statType = kvp.Key;
                int value = kvp.Value;

                _abilityInfoList[index].Show(statType, value);
                index++;
            }

            // 나머지 리스트 요소 비활성화
            for (int i = index; i < _abilityInfoList.Count; i++)
            {
                _abilityInfoList[index].Hide();
            }
        }
    }

    /// <summary>
    /// 장착 버튼 클릭
    /// </summary>
    public void OnClick_EquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipGearManager.Equip(CurrentItem);
    }

    /// <summary>
    /// 장착 해제 버튼 클릭
    /// </summary>
    public void OnClick_UnEquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipGearManager.UnEquip(CurrentItem);
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
