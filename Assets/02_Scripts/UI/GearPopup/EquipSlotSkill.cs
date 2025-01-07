using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlotSkill : EquipSlot
{
    public static event Action<IItem> OnClickEquipSlotDetailButton;

    [Title("���� ��ų ��ü ����", bold: false)]
    [SerializeField] private int _slotIndex;                                     // ���� �ε���
    [SerializeField] private GameObject _swapGuideGO;           // ���� ���̵� GO
    [SerializeField] private Button _swapButton;                // ��ü ��ư

    [Title("������ ��ư", bold: false)]
    [SerializeField] private Button _itemDetailButton;
    
    [Title("��ȭ ������ �� ȭ��ǥ ���ӿ�����Ʈ", bold: false)]
    [SerializeField] private GameObject _enhanceableArrowGO;            // ��ȭ ������ �� ȭ��ǥ ���ӿ�����Ʈ
    
    private void RefreshEquipSkillSlot()
    {
        Skill equippedSkill = EquipSkillManager.GetEquipSkillByIndex(_slotIndex);

        if (equippedSkill == null)
        {
            Debug.Log($"{_slotIndex} ��° ���� �׳� ��������!!");
            ShowEmptyGO();
            return;
        }

        ShowInfoGO(equippedSkill);
    }


    protected override void OnEnable()
    {
        EquipSkillManager.OnEquipSwapStarted += SwapGuidesON;
        EquipSkillManager.OnEquipSwapStarted += ItemDetailButtonOFF;

        EquipSkillManager.OnEquipSkillChanged += RefreshEquipSkillSlot;

        ItemInven.OnItemInvenChanged += RefreshEquipSkillSlot;


        _swapButton.onClick.AddListener(OnClickSwapButton);
        _itemDetailButton.onClick.AddListener(NotifyOnClickEquipSlotDetailButton);
    }

    protected override void OnDisable()
    {
        EquipSkillManager.OnEquipSwapStarted -= SwapGuidesON;
        EquipSkillManager.OnEquipSwapStarted -= ItemDetailButtonOFF;


        EquipSkillManager.OnEquipSkillChanged -= RefreshEquipSkillSlot;
        ItemInven.OnItemInvenChanged -= RefreshEquipSkillSlot;


        _swapButton.onClick.RemoveAllListeners();
        _itemDetailButton.onClick.RemoveAllListeners();
    }

    public override void ShowInfoGO(IItem item)
    {
        base.ShowInfoGO(item);

        ItemDetailButtonOn();
        SwapGuidesOFF();
        CheckOnOff_EnhanceableArrow();
    }

    public override void ShowEmptyGO()
    {
        base.ShowEmptyGO();

        ItemDetailButtonOFF();
        SwapGuidesOFF();
        CheckOnOff_EnhanceableArrow();

    }

    private void CheckOnOff_EnhanceableArrow()
    {
        if (CurrentItem == null)
        {
            Debug.Log($"CurrentItem == null!");

            _enhanceableArrowGO.gameObject.SetActive(false);
            return;
        }

        Debug.Log($"���� �������� üũ! {CurrentItem.IsEnhanceable()}");
        _enhanceableArrowGO.gameObject.SetActive(CurrentItem.IsEnhanceable());

    }

private void SwapGuidesON()
    {
        _swapGuideGO.SetActive(true);
        _swapButton.gameObject.SetActive(true);
    }

    private void SwapGuidesOFF()
    {
        _swapGuideGO.SetActive(false);
        _swapButton.gameObject.SetActive(false);
    }

    private void ItemDetailButtonOn()
    {
        _itemDetailButton.gameObject.SetActive(true);

    }

    private void ItemDetailButtonOFF()
    {
        _itemDetailButton.gameObject.SetActive(false);

    }



    private void OnClickSwapButton()
    {
        if (CurrentItem is Skill skill)
        {
            EquipSkillManager.SwapSkill(_slotIndex);

        }

    }

    private void NotifyOnClickEquipSlotDetailButton()
    {
        OnClickEquipSlotDetailButton?.Invoke(CurrentItem);
    }


}
