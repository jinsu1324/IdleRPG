using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ReddotComponent))]
public class EquipSlot_Skill : EquipSlot
{
    public static event Action<Item> OnClickItemDetailButton;   // ��ų�������� ������ �����Ϲ�ư �������� �׼�

    [SerializeField] protected SkillSlot _skillSlot;            // ��ų ���� ���� �ε���
    [SerializeField] private ReddotComponent _reddotComponent;  // ����� ������Ʈ
    [SerializeField] private Button _itemDetailButton;          // ������ ������ ��ư
    [SerializeField] private Button _swapButton;                // ��ų��ü ���� ��ư
    [SerializeField] private GameObject _swapGuideArrow;        // ��ų��ü ���� ���̵� ȭ��ǥ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipSkillManager.OnEquipSkillChanged += Update_EquipSlotSkill; // ������ų �ٲ������ -> ��ų�������� ������Ʈ
        ItemEnhanceManager.OnItemEnhance += Update_EquipSlotSkill; // ������ ��ȭ�Ҷ� -> ��ų�������� ������Ʈ
        EquipSkillManager.OnSkillSwapStarted += SwapGuidesON; // ������ų��ü �����Ҷ� -> ��ų�������� ���Ұ��̵�� �ѱ�
        EquipSkillManager.OnSkillSwapFinished += SwapGuidesOFF; // ������ų��ü �������� -> ��ų�������� ���Ұ��̵�� ����

        _itemDetailButton.onClick.AddListener(Notify_OnClickItemDetailButton);  // ������ �����Ϲ�ư ������ -> �̺�Ʈ �˸�
        _swapButton.onClick.AddListener(Swap); // ���ҹ�ư ������ -> ������ ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipSkillManager.OnEquipSkillChanged -= Update_EquipSlotSkill;
        ItemEnhanceManager.OnItemEnhance -= Update_EquipSlotSkill;
        EquipSkillManager.OnSkillSwapStarted -= SwapGuidesON;
        EquipSkillManager.OnSkillSwapFinished -= SwapGuidesOFF;

        _itemDetailButton.onClick.RemoveAllListeners();
        _swapButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init()
    {
        Update_EquipSlotSkill();
        SwapGuidesOFF();
    }

    /// <summary>
    /// ��ų�������� ������Ʈ
    /// </summary>
    private void Update_EquipSlotSkill(Item item = null)
    {
        // �ش� �ε����� ������ ��ų ��������
        Item equipSkill = EquipSkillManager.GetEquipSkill(_skillSlot);

        // ������ ��ų ������ ���� �����ְ�(+������Ʈ), ������ ����
        if (equipSkill != null)
            UpdateSlot(equipSkill);
        else
            EmptySlot();

        // ����� ������Ʈ
        Update_ReddotComponent();

        // ������ ������ ��ư ������Ʈ
        Update_ItemDetailButton();
    }

    /// <summary>
    /// ����� ������Ʈ ������Ʈ (���Կ� ����ִ� �������� ��ȭ��������?)
    /// </summary>
    public void Update_ReddotComponent()
    {
        if (CurrentItem == null) // ��ų���������� �����۾����� ����� �Ⱥ��̵��� �� ����
        {
            _reddotComponent.Hide();
            return;
        }

        _reddotComponent.UpdateReddot(() => ItemEnhanceManager.CanEnhance(CurrentItem));
    }

    /// <summary>
    /// ��ų�������� ������ �����Ϲ�ư �������� �̺�Ʈ ��Ƽ
    /// </summary>
    private void Notify_OnClickItemDetailButton()
    {
        OnClickItemDetailButton?.Invoke(CurrentItem);
    }

    /// <summary>
    /// ������ ������ ��ư ������Ʈ
    /// </summary>
    private void Update_ItemDetailButton()
    {
        if (CurrentItem == null)
        {
            _itemDetailButton.gameObject.SetActive(false);
            return;
        }

        _itemDetailButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// ���Ұ��̵� �ѱ�
    /// </summary>
    private void SwapGuidesON()
    {
        // ������ ������ ��ư�� ����
        _itemDetailButton.gameObject.SetActive(false);

        // ���� ��ư�� Ȱ��ȭ ��Ű��
        _swapButton.gameObject.SetActive(true);    

        // ���� ���̵嵵 Ȱ��ȭ ��Ű��
        _swapGuideArrow.gameObject.SetActive(true);

        // ����ڰ� ���ҹ�ư Ŭ������ ���...
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Swap()
    {
        EquipSkillManager.Swap(CurrentItem);
    }

    /// <summary>
    /// ���� ���̵�� ����
    /// </summary>
    private void SwapGuidesOFF()
    {
        _swapButton.gameObject.SetActive(false);
        _swapGuideArrow.gameObject.SetActive(false);
    }
}
