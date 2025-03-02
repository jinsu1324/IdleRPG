using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastSkillUI : MonoBehaviour
{
    private Skill _currentSkill;                        // 현재 스킬
    [SerializeField] private Image _skillIcon;          // 스킬 아이콘
    [SerializeField] private Image _coolTimeDimd;       // 쿨타임 딤드

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (_currentSkill == null)
            return;

        // 쿨타임에따라 딤드 점점 사라지게
        _coolTimeDimd.fillAmount = 1 - _currentSkill.Get_CurrentCoolTimeProgress();
    }

    /// <summary>
    /// 초기화 및 스킬 바인딩
    /// </summary>
    public void Init_BindSkill(Skill skill)
    {
        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(skill.ID);
        
        _currentSkill = skill;
        _skillIcon.sprite = itemDataSO.Icon;
        _skillIcon.gameObject.SetActive(true);
        _coolTimeDimd.gameObject.SetActive(true);
    }

    /// <summary>
    /// 숨기기
    /// </summary>
    public void Hide()
    {
        _currentSkill = null;
        _skillIcon.gameObject.SetActive(false);
        _coolTimeDimd.gameObject.SetActive(false);
    }
}
