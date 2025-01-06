using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 선택된 아이템 정보표시 및 상호작용(장착, 해제, 강화)
/// </summary>
public class SelectItemInfoUI : MonoBehaviour
{
    public static event Action SelectItemInfoChanged;                   // 선택 아이템 정보가 바뀌었을 때 이벤트

    public IItem CurrentItem { get; private set; }                      // 현재 아이템

    [Title("정보들", bold: false)]
    [SerializeField] private Image _itemIcon;                           // 아이템 아이콘
    [SerializeField] private Image _gradeFrame;                         // 등급 프레임
    [SerializeField] private TextMeshProUGUI _nameText;                 // 이름 텍스트
    [SerializeField] private TextMeshProUGUI _gradeText;                // 등급 텍스트
    [SerializeField] private TextMeshProUGUI _levelText;                // 레벨 텍스트
    [SerializeField] private TextMeshProUGUI _countText;                // 갯수 텍스트
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;     // 강화 가능한 아이템 갯수 텍스트
    [SerializeField] private Slider _countSlider;                       // 갯수 표시 슬라이더

    [Title("강화화살표 GO", bold: false)]
    [SerializeField] private GameObject _equipGO;                       // 장착되었을 때 아이콘 게임오브젝트
    [SerializeField] private GameObject _enhanceableArrowGO;            // 강화 가능할 때 화살표 게임오브젝트

    [Title("버튼들", bold: false)]
    [SerializeField] private Button _equipButton;                       // 장착 버튼
    [SerializeField] private Button _unEquipButton;                     // 장착 해제 버튼
    [SerializeField] private Button _enhanceButton;                     // 강화 버튼

    [Title("나가기 버튼", bold: false)]
    [SerializeField] private Button _exitButton;                        // 나가기 버튼

    [Title("[Skill Item] 상세설명 텍스트", bold: false)]
    [SerializeField] private GameObject _descGO;                        // 상세설명 GO
    [SerializeField] private TextMeshProUGUI _descText;                 // 상세설명 텍스트

    [Title("[Gear Item] 어빌리티 인포 리스트", bold: false)]
    [SerializeField] private GameObject _abilityGO;                     // 어빌리티 GO
    [SerializeField] private List<ItemAbilityInfo> _abilityInfoList;    // 어빌리티 인포 리스트


    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _equipButton.onClick.AddListener(OnClick_EquipButton);      // 장착버튼 핸들러 등록
        _unEquipButton.onClick.AddListener(OnClick_UnEquipButton);  // 장착해제버튼 핸들러 등록
        _enhanceButton.onClick.AddListener(OnClick_EnhanceButton);  // 강화버튼 핸들러 등록
        _exitButton.onClick.AddListener(() => gameObject.SetActive(false)); // 나가기 버튼 핸들러 등록
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
    /// 보여주기
    /// </summary>
    public void Show(ItemSlot selectSlot)
    {
        CurrentItem = selectSlot.CurrentItem;

        UpdateUI();

        gameObject.SetActive(true);
    }

    /// <summary>
    /// 감추기
    /// </summary>
    public void Hide()
    {
        CurrentItem = null;

        gameObject.SetActive(false);
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    private void UpdateUI()
    {
        // UI정보들 업데이트
        _itemIcon.sprite = CurrentItem.Icon;
        _gradeFrame.sprite = ResourceManager.Instance.GetItemGradeFrame(CurrentItem.Grade);
        _nameText.text = $"{CurrentItem.Name}";
        _nameText.color = ResourceManager.Instance.GetItemGradeColor(CurrentItem.Grade);
        _gradeText.text = $"{CurrentItem.Grade}";
        _gradeText.color = ResourceManager.Instance.GetItemGradeColor(CurrentItem.Grade);
        _levelText.text = $"Lv.{CurrentItem.Level}";
        _countText.text = $"{CurrentItem.Count}";
        _enhanceableCountText.text = $"{CurrentItem.EnhanceableCount}";
        _countSlider.value = (float)CurrentItem.Count / (float)CurrentItem.EnhanceableCount;
        _enhanceableArrowGO.gameObject.SetActive(CurrentItem.IsEnhanceable());

        Update_EnhanceButton();

        // 장비 아이템이면
        if (CurrentItem is Gear gear) 
        {
            // 장착 관련 버튼들 On Off
            bool isEquipped = EquipGearManager.IsEquippedGear(gear);
            Update_EquipButtonsGroup(isEquipped);
            
            Update_AbilityInfo(gear);    // 어빌리티 정보 켜기
            Hide_DescText(); // 상세설명 끄기

            _equipGO.SetActive(isEquipped);
        }

        // 스킬 아이템이면
        if (CurrentItem is Skill skill) 
        {
            // 장착 관련 버튼들 On Off
            bool isEquipped = EquipSkillManager.IsEquippedSkill(skill);
            Update_EquipButtonsGroup(isEquipped);

            Update_DescText(skill); // 상세설명 켜기  
            Hide_AbilityInfo();  // 어빌리티 정보 끄기
            
            _equipGO.SetActive(isEquipped);
        }
    }

    /// <summary>
    /// 어빌리티 정보 업데이트
    /// </summary>
    private void Update_AbilityInfo(Gear gear)
    {
        int index = 0;

        // 스탯 Dictionary 순회
        foreach (var kvp in gear.GetAbilityDict())
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

        _abilityGO.SetActive(true);
    }

    /// <summary>
    /// 어빌리티 인포 감추기
    /// </summary>
    private void Hide_AbilityInfo()
    {
        _abilityGO.SetActive(false);

        foreach (ItemAbilityInfo abilityInfo in _abilityInfoList)
            abilityInfo.Hide();
    }

    /// <summary>
    /// 설명 텍스트 업데이트
    /// </summary>
    private void Update_DescText(Skill skill)
    {
        _descText.text = skill.Desc;
        _descText.gameObject.SetActive(true);

        _descGO.SetActive(true);
    }

    /// <summary>
    /// 설명 텍스트 감추기
    /// </summary>
    private void Hide_DescText()
    {
        _descGO.SetActive(false);

        _descText.gameObject.SetActive(false);
    }

    /// <summary>
    /// 장착관련 버튼들 업데이트
    /// </summary>
    private void Update_EquipButtonsGroup(bool isEquipped)
    {
        _equipButton.gameObject.SetActive(!isEquipped);
        _unEquipButton.gameObject.SetActive(isEquipped);
    }

    /// <summary>
    /// 강화버튼 업데이트
    /// </summary>
    private void Update_EnhanceButton()
    {
        bool isEnhanceable = CurrentItem.IsEnhanceable();
        _enhanceButton.gameObject.SetActive(isEnhanceable);
    }

    /// <summary>
    /// 장착 버튼 클릭
    /// </summary>
    public void OnClick_EquipButton()
    {
        if (CurrentItem == null)
            return;

        if (CurrentItem is Gear gear)
        {
            EquipGearManager.EquipGear(gear);
            UpdateUI();
            NotifySelectItemInfoChanged();
        }

        if (CurrentItem is Skill skill)
        {
            EquipSkillManager.EquipSkill(skill);
            UpdateUI();
            NotifySelectItemInfoChanged();

            Hide();
        }
    }

    /// <summary>
    /// 장착 해제 버튼 클릭
    /// </summary>
    public void OnClick_UnEquipButton()
    {
        if (CurrentItem == null)
            return;

        if (CurrentItem is Gear gear)
        {
            EquipGearManager.UnEquipGear(gear);
            UpdateUI();
            NotifySelectItemInfoChanged();
        }

        if (CurrentItem is Skill skill)
        {
            EquipSkillManager.UnEquipSkill(skill);
            UpdateUI();
            NotifySelectItemInfoChanged();

            Hide();
        }
    }

    /// <summary>
    /// 강화 버튼 클릭
    /// </summary>
    public void OnClick_EnhanceButton()
    {
        if (CurrentItem == null)
            return;

        if (CurrentItem is Gear gear)
        {
            ItemInven.Enhance(gear);
            UpdateUI();
            NotifySelectItemInfoChanged();
        }

        if (CurrentItem is Skill skill)
        {
            ItemInven.Enhance(skill);
            UpdateUI();
            NotifySelectItemInfoChanged();

            Hide();
        }
    }

    /// <summary>
    /// 선택 아이템 정보 바뀌었을때의 이벤트 실행
    /// </summary>
    private void NotifySelectItemInfoChanged()
    {
        SelectItemInfoChanged?.Invoke();
    }
}
