using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI_Skill : ItemDetailUI
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

    [Title("나가기 버튼", bold: false)]
    [SerializeField] private Button _exitButton;                        // 나가기 버튼


    protected override void OnEnable()
    {
        ItemSlot.OnSlotSelected += Show; // 아이템 슬롯 선택되었을 때, 선택된 아이템 정보 UI 켜기

        _equipButton.onClick.AddListener(OnClick_EquipButton);      // 장착버튼 핸들러 등록
        _unEquipButton.onClick.AddListener(OnClick_UnEquipButton);  // 장착해제버튼 핸들러 등록
        _enhanceButton.onClick.AddListener(OnClick_EnhanceButton);  // 강화버튼 핸들러 등록
        _exitButton.onClick.AddListener(Hide);                      // 나가기 버튼 핸들러 등록
    }

    protected override void OnDisable()
    {
        ItemSlot.OnSlotSelected -= Show;

        _equipButton.onClick.RemoveAllListeners();
        _unEquipButton.onClick.RemoveAllListeners();
        _enhanceButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
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

    }

    /// <summary>
    /// 장착 버튼 클릭
    /// </summary>
    public void OnClick_EquipButton()
    {
        if (CurrentItem == null)
            return;

        EquipItemManager.Equip(CurrentItem, 0); // 스킬은 장착슬롯을 고르는게 필요할듯
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
