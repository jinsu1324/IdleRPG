using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CastSkillUIManager : MonoBehaviour
{
    //[SerializeField] private CastSkillUI[] _castSkillUIArr; // ĳ���� ��ų UI �� �迭

    ///// <summary>
    ///// OnEnable
    ///// </summary>
    //private void OnEnable()
    //{
    //    EquipSkillManager.OnEquipSkillChanged += Update_CastSkillUIArr; // ������ų �ٲ������ -> ĳ���ý�ųUI�� ������Ʈ
    //}

    ///// <summary>
    ///// OnDisable
    ///// </summary>
    //private void OnDisable()
    //{
    //    EquipSkillManager.OnEquipSkillChanged -= Update_CastSkillUIArr;
    //}

    ///// <summary>
    ///// Start
    ///// </summary>
    //private void Start()
    //{
    //    Update_CastSkillUIArr();
    //}

    //public void Update_CastSkillUIArr(Item item = null)
    //{
    //    // ������ų ��������
    //    ISkill[] equipSkillArr = EquipSkillManager.GetEquipISkill(); 

    //    // �ƿ� ������� UI �� ����� ����
    //    if (equipSkillArr == null)
    //    {
    //        foreach (CastSkillUI castSkillUI in _castSkillUIArr)
    //            castSkillUI.Hide();

    //        return;
    //    }

    //    // �����ѽ�ų����, UI�� ���ε�
    //    for (int i = 0; i < equipSkillArr.Length; i++)
    //    {
    //        // ������� UI �����
    //        if (equipSkillArr[i] == null)
    //        {
    //            _castSkillUIArr[i].Hide();
    //            continue;
    //        }

    //        // �Ⱥ������ UI�� �� ��ų ���ε�
    //        _castSkillUIArr[i].Init_BindSkill(equipSkillArr[i]);
    //    }
    //}
}
