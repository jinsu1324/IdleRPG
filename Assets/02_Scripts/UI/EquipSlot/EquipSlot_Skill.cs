using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ReddotComponent))]
public class EquipSlot_Skill : EquipSlot
{
    public static event Action<Item> OnClickItemDetailButton;   // 스킬장착슬롯 아이템 디테일버튼 눌렀을때 액션

    [SerializeField] protected SkillSlot _skillSlot;            // 스킬 장착 슬롯 인덱스
    [SerializeField] private ReddotComponent _reddotComponent;  // 레드닷 컴포넌트
    [SerializeField] private Button _itemDetailButton;          // 아이템 디테일 버튼
    [SerializeField] private Button _swapButton;                // 스킬교체 위한 버튼
    [SerializeField] private GameObject _swapGuideArrow;        // 스킬교체 위한 가이드 화살표

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipSkillManager.OnEquipSkillChanged += Update_EquipSlotSkill; // 장착스킬 바뀌었을때 -> 스킬장착슬롯 업데이트
        ItemEnhanceManager.OnItemEnhance += Update_EquipSlotSkill; // 아이템 강화할때 -> 스킬장착슬롯 업데이트
        EquipSkillManager.OnSkillSwapStarted += SwapGuidesON; // 장착스킬교체 시작할때 -> 스킬장착슬롯 스왑가이드들 켜기
        EquipSkillManager.OnSkillSwapFinished += SwapGuidesOFF; // 장착스킬교체 끝났을때 -> 스킬장착슬롯 스왑가이드들 끄기

        _itemDetailButton.onClick.AddListener(Notify_OnClickItemDetailButton);  // 아이템 디테일버튼 누르면 -> 이벤트 알림
        _swapButton.onClick.AddListener(Swap); // 스왑버튼 누르면 -> 아이템 스왑
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
    /// 초기화
    /// </summary>
    public void Init()
    {
        Update_EquipSlotSkill();
        SwapGuidesOFF();
    }

    /// <summary>
    /// 스킬장착슬롯 업데이트
    /// </summary>
    private void Update_EquipSlotSkill(Item item = null)
    {
        // 해당 인덱스에 장착한 스킬 가져오기
        Item equipSkill = EquipSkillManager.GetEquipSkill(_skillSlot);

        // 장착한 스킬 있으면 슬롯 보여주거(+업데이트), 없으면 비우기
        if (equipSkill != null)
            UpdateSlot(equipSkill);
        else
            EmptySlot();

        // 레드닷 업데이트
        Update_ReddotComponent();

        // 아이템 디테일 버튼 업데이트
        Update_ItemDetailButton();
    }

    /// <summary>
    /// 레드닷 컴포넌트 업데이트 (슬롯에 들어있는 아이템이 강화가능한지?)
    /// </summary>
    public void Update_ReddotComponent()
    {
        if (CurrentItem == null) // 스킬장착슬롯은 아이템없으면 레드닷 안보이도록 다 끄기
        {
            _reddotComponent.Hide();
            return;
        }

        _reddotComponent.UpdateReddot(() => ItemEnhanceManager.CanEnhance(CurrentItem));
    }

    /// <summary>
    /// 스킬장착슬롯 아이템 디테일버튼 눌렀을때 이벤트 노티
    /// </summary>
    private void Notify_OnClickItemDetailButton()
    {
        OnClickItemDetailButton?.Invoke(CurrentItem);
    }

    /// <summary>
    /// 아이템 디테일 버튼 업데이트
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
    /// 스왑가이드 켜기
    /// </summary>
    private void SwapGuidesON()
    {
        // 아이템 디테일 버튼을 끄고
        _itemDetailButton.gameObject.SetActive(false);

        // 스왑 버튼을 활성화 시키기
        _swapButton.gameObject.SetActive(true);    

        // 스왑 가이드도 활성화 시키기
        _swapGuideArrow.gameObject.SetActive(true);

        // 사용자가 스왑버튼 클릭함을 대기...
    }

    /// <summary>
    /// 스왑
    /// </summary>
    private void Swap()
    {
        EquipSkillManager.Swap(CurrentItem);
    }

    /// <summary>
    /// 스왑 가이드들 끄기
    /// </summary>
    private void SwapGuidesOFF()
    {
        _swapButton.gameObject.SetActive(false);
        _swapGuideArrow.gameObject.SetActive(false);
    }
}
