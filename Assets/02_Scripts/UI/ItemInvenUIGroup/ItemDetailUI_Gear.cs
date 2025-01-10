using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI_Gear : ItemDetailUI
{
    [SerializeField] private TextMeshProUGUI _levelText;                // 레벨 텍스트
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;     // 강화 가능한 아이템 갯수 텍스트
    [SerializeField] private Slider _countSlider;                       // 갯수 표시 슬라이더

    [Title("강화화살표 GO", bold: false)]
    [SerializeField] private GameObject _equipIcon;                       // 장착되었을 때 아이콘 게임오브젝트
    [SerializeField] private GameObject _enhanceableArrowGO;            // 강화 가능할 때 화살표 게임오브젝트

    [Title("버튼들", bold: false)]
    [SerializeField] private Button _equipButton;                       // 장착 버튼
    [SerializeField] private Button _unEquipButton;                     // 장착 해제 버튼
    [SerializeField] private Button _enhanceButton;                     // 강화 버튼

    [Title("어빌리티 인포 리스트", bold: false)]
    [SerializeField] private List<ItemAbilityInfo> _abilityInfoList;    // 어빌리티 인포 리스트

    protected override void OnEnable()
    {
        ItemSlot.OnSlotSelected += Show; // 아이템 슬롯 선택되었을 때, 선택된 아이템 정보 UI 켜기


        _equipButton.onClick.AddListener(OnClick_EquipButton);      // 장착버튼 핸들러 등록
        _unEquipButton.onClick.AddListener(OnClick_UnEquipButton);  // 장착해제버튼 핸들러 등록
        _enhanceButton.onClick.AddListener(OnClick_EnhanceButton);  // 강화버튼 핸들러 등록
    }

    protected override void OnDisable()
    {
        ItemSlot.OnSlotSelected -= Show;

        _equipButton.onClick.RemoveAllListeners();
        _unEquipButton.onClick.RemoveAllListeners();
        _enhanceButton.onClick.RemoveAllListeners();
    }

    public override void Show(Item item)
    {
        base.Show(item);
    }

    public override void Hide()
    {
        base.Hide();
    }


    protected override void UpdateUI()
    {
        base.UpdateUI();

        if (CurrentItem is IEnhanceableItem enhanceableItem)
        {
            _levelText.text = $"Lv.{enhanceableItem.Level}";
            _countText.text = $"{CurrentItem.Count}";
            _enhanceableCountText.text = $"{enhanceableItem.EnhanceableCount}";
            _countSlider.value = (float)CurrentItem.Count / (float)enhanceableItem.EnhanceableCount;
            _enhanceableArrowGO.gameObject.SetActive(enhanceableItem.CanEnhance());
            _enhanceButton.gameObject.SetActive(enhanceableItem.CanEnhance());
        }

        bool isEquipped = EquipItemManager.IsEquipped(CurrentItem);
        _equipButton.gameObject.SetActive(!isEquipped);
        _unEquipButton.gameObject.SetActive(isEquipped);
        _equipIcon.SetActive(isEquipped);

        if (CurrentItem is GearItem gearItem)
        {
            Update_AbilityInfo(gearItem);
        }
       
        
    }



    /// <summary>
    /// 어빌리티 정보 업데이트
    /// </summary>
    private void Update_AbilityInfo(GearItem gearItem)
    {
        int index = 0;

        // 스탯 Dictionary 순회
        foreach (var kvp in gearItem.GetAbilityDict())
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


    /// <summary>
    /// 장착 버튼 클릭
    /// </summary>
    public void OnClick_EquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipItemManager.Equip(CurrentItem, 0);  // 장착슬롯을 고르지 않고 한개있는 슬롯에 바로 장착
        UpdateUI();
    }

    /// <summary>
    /// 장착 해제 버튼 클릭
    /// </summary>
    public void OnClick_UnEquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipItemManager.UnEquip(CurrentItem);
        UpdateUI();
    }

    /// <summary>
    /// 강화 버튼 클릭
    /// </summary>
    public void OnClick_EnhanceButton()
    {
        if (CurrentItem == null)
            return;

        ItemEnhanceManager.Enhance(CurrentItem);
        UpdateUI();
    }
}
