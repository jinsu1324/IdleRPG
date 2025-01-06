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
    public static event Action SelectItemInfoChanged;                   // ���� ������ ������ �ٲ���� �� �̺�Ʈ

    public IItem CurrentItem { get; private set; }                      // ���� ������

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
        _equipButton.onClick.AddListener(OnClick_EquipButton);      // ������ư �ڵ鷯 ���
        _unEquipButton.onClick.AddListener(OnClick_UnEquipButton);  // ����������ư �ڵ鷯 ���
        _enhanceButton.onClick.AddListener(OnClick_EnhanceButton);  // ��ȭ��ư �ڵ鷯 ���
        _exitButton.onClick.AddListener(() => gameObject.SetActive(false)); // ������ ��ư �ڵ鷯 ���
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
    /// �����ֱ�
    /// </summary>
    public void Show(ItemSlot selectSlot)
    {
        CurrentItem = selectSlot.CurrentItem;

        UpdateUI();

        gameObject.SetActive(true);
    }

    /// <summary>
    /// ���߱�
    /// </summary>
    public void Hide()
    {
        CurrentItem = null;

        gameObject.SetActive(false);
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

        Update_EnhanceButton();

        // ��� �������̸�
        if (CurrentItem is Gear gear) 
        {
            // ���� ���� ��ư�� On Off
            bool isEquipped = EquipGearManager.IsEquippedGear(gear);
            Update_EquipButtonsGroup(isEquipped);
            
            Update_AbilityInfo(gear);    // �����Ƽ ���� �ѱ�
            Hide_DescText(); // �󼼼��� ����

            _equipGO.SetActive(isEquipped);
        }

        // ��ų �������̸�
        if (CurrentItem is Skill skill) 
        {
            // ���� ���� ��ư�� On Off
            bool isEquipped = EquipSkillManager.IsEquippedSkill(skill);
            Update_EquipButtonsGroup(isEquipped);

            Update_DescText(skill); // �󼼼��� �ѱ�  
            Hide_AbilityInfo();  // �����Ƽ ���� ����
            
            _equipGO.SetActive(isEquipped);
        }
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
    /// ���� ���� ��ư Ŭ��
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
    /// ��ȭ ��ư Ŭ��
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
    /// ���� ������ ���� �ٲ�������� �̺�Ʈ ����
    /// </summary>
    private void NotifySelectItemInfoChanged()
    {
        SelectItemInfoChanged?.Invoke();
    }
}
