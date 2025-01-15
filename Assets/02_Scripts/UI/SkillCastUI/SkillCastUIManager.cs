using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillCastUIManager : MonoBehaviour
{
    [SerializeField] private SkillCastUI[] _skillCastUIArr;

    private void OnEnable()
    {
        EquipSkillManager.OnEquipSkillChanged += Update_SkillCastUIArr; // 장착스킬 바뀌었을때 -> 스킬캐스팅UI들 업데이트
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

        // 아예 비었으면 UI 다 숨기고 리턴
        if (equipSkillArr == null)
        {
            foreach (SkillCastUI skillCastUI in _skillCastUIArr)
                skillCastUI.Hide();

            return;
        }

        // 장착한 스킬들 UI에 바인딩
        for (int i = 0; i < equipSkillArr.Length; i++)
        {
            // 비었으면 UI 숨기기
            if (equipSkillArr[i] == null)
            {
                _skillCastUIArr[i].Hide();
                continue;
            }

            // 안비었으면 UI에 그 스킬 바인딩
            _skillCastUIArr[i].Init_BindSkill(equipSkillArr[i]);
        }
    }
}
