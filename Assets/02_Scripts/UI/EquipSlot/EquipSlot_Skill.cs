using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSlot_Skill : EquipSlot
{
    [SerializeField] protected int _slotIndex;  // 슬롯 인덱스


    public static event Action<Item> OnClickDetailButton;       // 슬롯아이템 디테일버튼을 클릭했을 때 이벤트

    //[Title("스킬 장착슬롯 관련", bold: false)]
    //[SerializeField] private GameObject _swapGuide;             // 교체 가이드
    //[SerializeField] private Button _swapButton;                // 교체 버튼
    //[SerializeField] private Button _detailButton;              // 슬롯 아이템 디테일 버튼
    //[SerializeField] private GameObject _enhanceableArrow;      // 강화 가능할 때 화살표

    /// <summary>
    /// OnEnable
    /// </summary>
    protected void OnEnable()
    {
        //EquipSkillManager.OnEquipSwapStarted += SwapGuides_ON;  // 장착스킬 교체 시작되었을 때, 교체가이드 켜기
        //EquipSkillManager.OnEquipSwapStarted += DetailButton_OFF; // 장착스킬 교체 시작되었을 때, 슬롯아이템 디테일버튼은 끄기
        //EquipSkillManager.OnEquipSkillChanged += UpdateSlot; // 장착 스킬이 변경되었을 때, 스킬장착슬롯 업데이트
        //ItemInven.OnItemInvenChanged += UpdateSlot; // 가지고 있는 아이템들 상태가 변경되었을 때도, 스킬장착슬롯 업데이트 (강화)

        //_swapButton.onClick.AddListener(TrySwapSkill); // 교체 버튼 클릭하면, 스킬 교체 시도
        //_detailButton.onClick.AddListener(Notify_OnClickDetailButton);  // 슬롯아이템 디테일버튼 클릭하면, 이벤트 알림
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
    /// 슬롯 업데이트
    /// </summary>
    private void UpdateSlot()
    {
        // 슬롯인덱스에 장착되어있는 스킬을 가져옴
        SkillItem equippedSkill = EquipSkillManager.GetEquippedSkill(_slotIndex);

        // 없으면 빈슬롯 보여주기
        if (equippedSkill == null)
        {
            EmptySlot();
            return;
        }

        // 있으면 해당스킬 정보 보여주기
        UpdateSlot(equippedSkill);
    }

    ///// <summary>
    ///// 스킬 교체 시도
    ///// </summary>
    //private void TrySwapSkill()
    //{
    //    if (CurrentItem is SkillItem skill)
    //        EquipSkillManager.SwapSkill(_slotIndex);
    //}

    /// <summary>
    /// 슬롯아이템 디테일버튼을 클릭했을 때 이벤트 알림
    /// </summary>
    private void Notify_OnClickDetailButton()
    {
        OnClickDetailButton?.Invoke(CurrentItem);
    }

    ///// <summary>
    ///// 강화가능 화살표 On / Off 여부 체크
    ///// </summary>
    //private void Check_EnhanceableArrow()
    //{
    //    // 현재아이템 없으면 끄기
    //    if (CurrentItem == null)
    //    {
    //        _enhanceableArrow.gameObject.SetActive(false);
    //        return;
    //    }

    //    if (CurrentItem is IEnhanceableItem enhanceableItem)
    //    {
    //        // 아이템 강화가능 여부에 따라 On / Off
    //        _enhanceableArrow.gameObject.SetActive(enhanceableItem.CanEnhance());
    //    }

        
    //}

    ///// <summary>
    ///// 교체 가이드들 켜기
    ///// </summary>
    //private void SwapGuides_ON()
    //{
    //    _swapGuide.SetActive(true);
    //    _swapButton.gameObject.SetActive(true);
    //}

    ///// <summary>
    ///// 교체 가이드들 끄기
    ///// </summary>
    //private void SwapGuides_OFF()
    //{
    //    _swapGuide.SetActive(false);
    //    _swapButton.gameObject.SetActive(false);
    //}

    ///// <summary>
    ///// 슬롯 아이템 디테일버튼 켜기
    ///// </summary>
    //private void DetailButton_ON() => _detailButton.gameObject.SetActive(true);

    ///// <summary>
    ///// 슬롯 아이템 디테일버튼 끄기
    ///// </summary>
    //private void DetailButton_OFF() => _detailButton.gameObject.SetActive(false);
}
