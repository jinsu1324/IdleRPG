using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastSkillUI : MonoBehaviour
{
    private ISkill _currentSkill;                       // ���� ��ų
    [SerializeField] private Image _skillIcon;          // ��ų ������
    [SerializeField] private Image _coolTimeDimd;       // ��Ÿ�� ����

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        if (_currentSkill == null)
            return;

        // ��Ÿ�ӿ����� ���� ���� �������
        _coolTimeDimd.fillAmount = 1 - _currentSkill.GetCurrentCoolTimeProgress();
    }

    /// <summary>
    /// �ʱ�ȭ �� ��ų ���ε�
    /// </summary>
    public void Init_BindSkill(ISkill skill)
    {
        //_currentSkill = skill;

        //if (skill is Item item)
        //    _skillIcon.sprite = item.Icon;

        //_skillIcon.gameObject.SetActive(true);
        //_coolTimeDimd.gameObject.SetActive(true);
    }

    /// <summary>
    /// �����
    /// </summary>
    public void Hide()
    {
        _currentSkill = null;
        _skillIcon.gameObject.SetActive(false);
        _coolTimeDimd.gameObject.SetActive(false);
    }
}
