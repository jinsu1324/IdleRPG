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

    [Title("장비속성 UI들", bold: false)]
    [SerializeField] private List<GearStatUI> _gearStatUIList;          // 장비속성 UI 리스트
    
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
        base.UpdateUI(item);
        ItemDataSO itemDataSO = ItemManager.GetItemDataSO(item.ID);

        _descText.text = itemDataSO.Desc;
        _levelText.text = $"Lv.{item.Level}";
        _countText.text = $"{item.Count}";
        _enhanceableCountText.text = $"{itemDataSO.GetEnhanceCount(item.Level)}";
        _countSlider.value = (float)item.Level / (float)itemDataSO.GetEnhanceCount(item.Level);
        _enhanceableArrowIcon.gameObject.SetActive(ItemManager.CanEnhance(item));
        _enhanceButton.gameObject.SetActive(ItemManager.CanEnhance(item));

        Update_GearStatUI(item);

        bool isEquipped = EquipGearManager.IsEquipped(item);
        _equipButton.gameObject.SetActive(!isEquipped);
        _unEquipButton.gameObject.SetActive(isEquipped);
        _equipIcon.SetActive(isEquipped);

    }

    /// <summary>
    /// 장비속성들 UI 업데이트
    /// </summary>
    private void Update_GearStatUI(Item item)
    {
        ItemDataSO itemDataSO = ItemManager.GetItemDataSO(item.ID);

        if (itemDataSO is GearDataSO gearDataSO)
        {
            int index = 0;
            foreach (var kvp in gearDataSO.GetGearStats(item.Level)) 
            {
                StatType statType = kvp.Key;
                float value = kvp.Value;
                _gearStatUIList[index].Show(statType, value); // 스탯갯수만큼 UI 표시
                
                index++;
            }

            for (int i = index; i < _gearStatUIList.Count; i++)
                _gearStatUIList[index].Hide(); // 나머지는 비활성화
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
