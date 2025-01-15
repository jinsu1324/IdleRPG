using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCastUI : MonoBehaviour
{
    private ISkill _currentSkill;
    [SerializeField] private Image _skillIcon;
    [SerializeField] private Image _coolTimeDimd;

    private void Update()
    {
        if (_currentSkill == null)
            return;

        // 쿨타임 진행 상태를 이미지의 fillAmount로 설정
        _coolTimeDimd.fillAmount = 1 - _currentSkill.GetCurrentCoolTimeProgress();
    }

    public void Init_BindSkill(ISkill skill)
    {
        _currentSkill = skill;

        if (skill is Item item)
            _skillIcon.sprite = item.Icon;

        _skillIcon.gameObject.SetActive(true);
        _coolTimeDimd.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _currentSkill = null;
        _skillIcon.gameObject.SetActive(false);
        _coolTimeDimd.gameObject.SetActive(false);
    }
}
