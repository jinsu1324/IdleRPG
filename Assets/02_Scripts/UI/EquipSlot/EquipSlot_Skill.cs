using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot_Skill : EquipSlot
{
    [SerializeField] protected int _slotIndex;  // ���� �ε���


    public static event Action<Item> OnClickDetailButton;       // ���Ծ����� �����Ϲ�ư�� Ŭ������ �� �̺�Ʈ

    //[Title("��ų �������� ����", bold: false)]
    //[SerializeField] private GameObject _swapGuide;             // ��ü ���̵�
    //[SerializeField] private Button _swapButton;                // ��ü ��ư
    //[SerializeField] private Button _detailButton;              // ���� ������ ������ ��ư
    //[SerializeField] private GameObject _enhanceableArrow;      // ��ȭ ������ �� ȭ��ǥ

    /// <summary>
    /// OnEnable
    /// </summary>
    protected void OnEnable()
    {
        //EquipSkillManager.OnEquipSwapStarted += SwapGuides_ON;  // ������ų ��ü ���۵Ǿ��� ��, ��ü���̵� �ѱ�
        //EquipSkillManager.OnEquipSwapStarted += DetailButton_OFF; // ������ų ��ü ���۵Ǿ��� ��, ���Ծ����� �����Ϲ�ư�� ����
        //EquipSkillManager.OnEquipSkillChanged += UpdateSlot; // ���� ��ų�� ����Ǿ��� ��, ��ų�������� ������Ʈ
        //ItemInven.OnItemInvenChanged += UpdateSlot; // ������ �ִ� �����۵� ���°� ����Ǿ��� ����, ��ų�������� ������Ʈ (��ȭ)

        //_swapButton.onClick.AddListener(TrySwapSkill); // ��ü ��ư Ŭ���ϸ�, ��ų ��ü �õ�
        //_detailButton.onClick.AddListener(Notify_OnClickDetailButton);  // ���Ծ����� �����Ϲ�ư Ŭ���ϸ�, �̺�Ʈ �˸�
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    protected void OnDisable()
    {
        //EquipSkillManager.OnEquipSwapStarted -= SwapGuides_ON;
        //EquipSkillManager.OnEquipSwapStarted -= DetailButton_OFF;
        //EquipSkillManager.OnEquipSkillChanged -= UpdateSlot;
        //ItemInven.OnItemInvenChanged -= UpdateSlot;

        //_swapButton.onClick.RemoveAllListeners();
        //_detailButton.onClick.RemoveAllListeners();
    }

    

    /// <summary>
    /// ���� ������Ʈ
    /// </summary>
    private void UpdateSlot()
    {
        // �����ε����� �����Ǿ��ִ� ��ų�� ������
        SkillItem equippedSkill = EquipSkillManager.GetEquippedSkill(_slotIndex);

        // ������ �󽽷� �����ֱ�
        if (equippedSkill == null)
        {
            EmptySlot();
            return;
        }

        // ������ �ش罺ų ���� �����ֱ�
        UpdateSlot(equippedSkill);
    }

    ///// <summary>
    ///// ��ų ��ü �õ�
    ///// </summary>
    //private void TrySwapSkill()
    //{
    //    if (CurrentItem is SkillItem skill)
    //        EquipSkillManager.SwapSkill(_slotIndex);
    //}

    /// <summary>
    /// ���Ծ����� �����Ϲ�ư�� Ŭ������ �� �̺�Ʈ �˸�
    /// </summary>
    private void Notify_OnClickDetailButton()
    {
        OnClickDetailButton?.Invoke(CurrentItem);
    }

    ///// <summary>
    ///// ��ȭ���� ȭ��ǥ On / Off ���� üũ
    ///// </summary>
    //private void Check_EnhanceableArrow()
    //{
    //    // ��������� ������ ����
    //    if (CurrentItem == null)
    //    {
    //        _enhanceableArrow.gameObject.SetActive(false);
    //        return;
    //    }

    //    if (CurrentItem is IEnhanceableItem enhanceableItem)
    //    {
    //        // ������ ��ȭ���� ���ο� ���� On / Off
    //        _enhanceableArrow.gameObject.SetActive(enhanceableItem.CanEnhance());
    //    }

        
    //}

    ///// <summary>
    ///// ��ü ���̵�� �ѱ�
    ///// </summary>
    //private void SwapGuides_ON()
    //{
    //    _swapGuide.SetActive(true);
    //    _swapButton.gameObject.SetActive(true);
    //}

    ///// <summary>
    ///// ��ü ���̵�� ����
    ///// </summary>
    //private void SwapGuides_OFF()
    //{
    //    _swapGuide.SetActive(false);
    //    _swapButton.gameObject.SetActive(false);
    //}

    ///// <summary>
    ///// ���� ������ �����Ϲ�ư �ѱ�
    ///// </summary>
    //private void DetailButton_ON() => _detailButton.gameObject.SetActive(true);

    ///// <summary>
    ///// ���� ������ �����Ϲ�ư ����
    ///// </summary>
    //private void DetailButton_OFF() => _detailButton.gameObject.SetActive(false);
}
