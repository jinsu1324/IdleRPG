using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillCastUIManager : MonoBehaviour
{
    [SerializeField] private SkillCastUI[] _skillCastUIArr;

    private void OnEnable()
    {
        EquipSkillManager.OnEquipSkillChanged += Update_SkillCastUIArr; // ������ų �ٲ������ -> ��ųĳ����UI�� ������Ʈ
    }

    private void OnDisable()
    {
        EquipSkillManager.OnEquipSkillChanged -= Update_SkillCastUIArr;

    }

    private void Start()
    {
        Update_SkillCastUIArr();
    }

    public void Update_SkillCastUIArr(Item item = null)
    {
        ISkill[] equipSkillArr = EquipSkillManager.GetEquipISkill();

        // �ƿ� ������� UI �� ����� ����
        if (equipSkillArr == null)
        {
            foreach (SkillCastUI skillCastUI in _skillCastUIArr)
                skillCastUI.Hide();

            return;
        }

        // ������ ��ų�� UI�� ���ε�
        for (int i = 0; i < equipSkillArr.Length; i++)
        {
            // ������� UI �����
            if (equipSkillArr[i] == null)
            {
                _skillCastUIArr[i].Hide();
                continue;
            }

            // �Ⱥ������ UI�� �� ��ų ���ε�
            _skillCastUIArr[i].Init_BindSkill(equipSkillArr[i]);
        }
    }
}
