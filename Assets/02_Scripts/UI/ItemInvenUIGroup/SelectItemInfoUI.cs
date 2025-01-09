using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���õ� ������ ����ǥ�� �� ��ȣ�ۿ�(����, ����, ��ȭ)
/// </summary>
public class SelectItemInfoUI : MonoBehaviour
{
    public static event Action OnSelectItemInfoChanged;                 // ���� ������ ������ �ٲ���� �� �̺�Ʈ
    public IItem CurrentItem { get; private set; }                      // ���� ������

    [Title("������ GO", bold: false)]
    [SerializeField] private GameObject _masterGO;                      // ������ GO

    [Title("������", bold: false)]
    [SerializeField] private Image _itemIcon;                           // ������ ������
    [SerializeField] private Image _gradeFrame;                         // ��� ������
    [SerializeField] private TextMeshProUGUI _nameText;                 // �̸� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _gradeText;                // ��� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _levelText;                // ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _countText;                // ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _enhanceableCountText;     // ��ȭ ������ ������ ���� �ؽ�Ʈ
    [SerializeField] private Slider _countSlider;                       // ���� ǥ�� �����̴�

    [Title("��ȭȭ��ǥ GO", bold: false)]
    [SerializeField] private GameObject _equipGO;                       // �����Ǿ��� �� ������ ���ӿ�����Ʈ
    [SerializeField] private GameObject _enhanceableArrowGO;            // ��ȭ ������ �� ȭ��ǥ ���ӿ�����Ʈ

    [Title("��ư��", bold: false)]
    [SerializeField] private Button _equipButton;                       // ���� ��ư
    [SerializeField] private Button _unEquipButton;                     // ���� ���� ��ư
    [SerializeField] private Button _enhanceButton;                     // ��ȭ ��ư

    [Title("������ ��ư", bold: false)]
    [SerializeField] private Button _exitButton;                        // ������ ��ư

    [Title("[Skill Item] �󼼼��� �ؽ�Ʈ", bold: false)]
    [SerializeField] private GameObject _descGO;                        // �󼼼��� GO
    [SerializeField] private TextMeshProUGUI _descText;                 // �󼼼��� �ؽ�Ʈ

    [Title("[Gear Item] �����Ƽ ���� ����Ʈ", bold: false)]
    [SerializeField] private GameObject _abilityGO;                     // �����Ƽ GO
    [SerializeField] private List<ItemAbilityInfo> _abilityInfoList;    // �����Ƽ ���� ����Ʈ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        ItemSlot.OnSlotSelected += Show; // ������ ���� ���õǾ��� ��, ���õ� ������ ���� UI �ѱ�
        EquipSlotSkill.OnClickDetailButton += Show; // ��ų �������� �����Ϲ�ư Ŭ������ �� (����Ŭ��), ���õ� ������ ���� UI �ѱ�

        _equipButton.onClick.AddListener(OnClick_EquipButton);      // ������ư �ڵ鷯 ���
        _unEquipButton.onClick.AddListener(OnClick_UnEquipButton);  // ����������ư �ڵ鷯 ���
        _enhanceButton.onClick.AddListener(OnClick_EnhanceButton);  // ��ȭ��ư �ڵ鷯 ���
        _exitButton.onClick.AddListener(Hide);                      // ������ ��ư �ڵ鷯 ���
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        ItemSlot.OnSlotSelected -= Show;
        EquipSlotSkill.OnClickDetailButton -= Show;

        _equipButton.onClick.RemoveAllListeners();
        _unEquipButton.onClick.RemoveAllListeners();
        _enhanceButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// �����ֱ�
    /// </summary>
    public void Show(IItem item)
    {
        CurrentItem = item;
        UpdateUI();
        _masterGO.SetActive(true);
    }

    /// <summary>
    /// ���߱�
    /// </summary>
    public void Hide()
    {
        CurrentItem = null;
        _masterGO.SetActive(false);
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    private void UpdateUI()
    {
        // UI������ ������Ʈ
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

        // ��� �������̸�, ��� ���� ��ư�� ����UI ������Ʈ
        if (CurrentItem is Gear gear) 
            Update_ButtonsAndInfoUI_ByGear(gear);

        // ��ų �������̸�, ��ų�� ���� ��ư�� ����UI ������Ʈ
        if (CurrentItem is Skill skill) 
            Update_ButtonsAndInfoUI_BySkill(skill);
    }

    /// <summary>
    /// ��� ���� ��ư�� ����UI ������Ʈ
    /// </summary>
    private void Update_ButtonsAndInfoUI_ByGear(Gear gear)
    {
        // ��ȭ ��ư ������Ʈ
        Update_EnhanceButton();

        // ������ư�� ON/OFF
        bool isEquipped = EquipGearManager.IsEquippedGear(gear);
        Update_EquipButtonsGroup(isEquipped);

        // ���� ������ ON/OFF
        EquipIconONOFF(isEquipped);

        // �����Ƽ ���� �ѱ�, �󼼼��� ����
        Update_AbilityInfo(gear);
        Hide_DescText();
    }

    /// <summary>
    /// ��ų�� ���� ��ư�� ����UI ������Ʈ
    /// </summary>
    private void Update_ButtonsAndInfoUI_BySkill(Skill skill)
    {
        // ��ȭ ��ư ������Ʈ
        Update_EnhanceButton();

        // ������ư�� ON/OFF
        bool isEquipped = EquipSkillManager.IsEquippedSkill(skill);
        Update_EquipButtonsGroup(isEquipped);

        // ���� ������ ON/OFF
        EquipIconONOFF(isEquipped);

        // �󼼼��� �ѱ�, �����Ƽ ���� ����
        Update_DescText(skill);
        Hide_AbilityInfo();
    }

    /// <summary>
    /// �����Ƽ ���� ������Ʈ
    /// </summary>
    private void Update_AbilityInfo(Gear gear)
    {
        int index = 0;

        // ���� Dictionary ��ȸ
        foreach (var kvp in gear.GetAbilityDict())
        {
            StatType statType = kvp.Key;
            int value = kvp.Value;

            _abilityInfoList[index].Show(statType, value);
            index++;
        }

        // ������ ����Ʈ ��� ��Ȱ��ȭ
        for (int i = index; i < _abilityInfoList.Count; i++)
        {
            _abilityInfoList[index].Hide();
        }

        _abilityGO.SetActive(true);
    }

    /// <summary>
    /// �����Ƽ ���� ���߱�
    /// </summary>
    private void Hide_AbilityInfo()
    {
        _abilityGO.SetActive(false);

        foreach (ItemAbilityInfo abilityInfo in _abilityInfoList)
            abilityInfo.Hide();
    }

    /// <summary>
    /// ���� �ؽ�Ʈ ������Ʈ
    /// </summary>
    private void Update_DescText(Skill skill)
    {
        _descText.text = skill.Desc;
        _descText.gameObject.SetActive(true);
        _descGO.SetActive(true);
    }

    /// <summary>
    /// ���� �ؽ�Ʈ ���߱�
    /// </summary>
    private void Hide_DescText()
    {
        _descGO.SetActive(false);
        _descText.gameObject.SetActive(false);
    }

    /// <summary>
    /// �������� ��ư�� ������Ʈ
    /// </summary>
    private void Update_EquipButtonsGroup(bool isEquipped)
    {
        _equipButton.gameObject.SetActive(!isEquipped);
        _unEquipButton.gameObject.SetActive(isEquipped);
    }

    /// <summary>
    /// ��ȭ��ư ������Ʈ
    /// </summary>
    private void Update_EnhanceButton()
    {
        bool isEnhanceable = CurrentItem.IsEnhanceable();
        _enhanceButton.gameObject.SetActive(isEnhanceable);
    }

    /// <summary>
    /// ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_EquipButton()
    {
        if (CurrentItem == null)
            return;

        if (CurrentItem is Gear gear)
        {
            EquipGearManager.EquipGear(gear);
            UpdateUIAndNotify();
        }

        if (CurrentItem is Skill skill)
        {
            EquipSkillManager.EquipSkill(skill);
            UpdateUIAndNotify(true);
        }
    }

    /// <summary>
    /// ���� ���� ��ư Ŭ��
    /// </summary>
    public void OnClick_UnEquipButton()
    {
        if (CurrentItem == null)
            return;

        if (CurrentItem is Gear gear)
        {
            EquipGearManager.UnEquipGear(gear);
            UpdateUIAndNotify();
        }

        if (CurrentItem is Skill skill)
        {
            EquipSkillManager.UnEquipSkill(skill);
            UpdateUIAndNotify(true);
        }
    }

    /// <summary>
    /// ��ȭ ��ư Ŭ��
    /// </summary>
    public void OnClick_EnhanceButton()
    {
        if (CurrentItem == null)
            return;

        if (CurrentItem is Gear gear)
        {
            ItemInven.Enhance(gear);
            UpdateUIAndNotify();
        }

        if (CurrentItem is Skill skill)
        {
            ItemInven.Enhance(skill);
            UpdateUIAndNotify(true);
        }
    }

    /// <summary>
    /// UI������Ʈ �� �̺�Ʈ �˸�
    /// </summary>
    private void UpdateUIAndNotify(bool isHideMasterUI = false)
    {
        UpdateUI(); // UI ������Ʈ
        Notify_SelectItemInfoChanged(); // ���õ� ���������� �����̺�Ʈ �˸�

        FXManager.Instance.SpawnFX(FXName.UIFX_UpgradeItem, _itemIcon.transform); // ����Ʈ �����ֱ�

        if (isHideMasterUI)
            Hide(); // ��ü UI �����
    }

    /// <summary>
    /// ���õ� ���������� ���ŵǾ����� �̺�Ʈ �˸�
    /// </summary>
    private void Notify_SelectItemInfoChanged()
    {
        OnSelectItemInfoChanged?.Invoke();
    }

    /// <summary>
    /// '������' ������ ON/OFF
    /// </summary>
    private void EquipIconONOFF(bool isEquipped)
    {
        _equipGO.SetActive(isEquipped);
    }
}
