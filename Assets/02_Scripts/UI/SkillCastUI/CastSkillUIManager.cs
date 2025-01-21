using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CastSkillUIManager : MonoBehaviour
{
    //[SerializeField] private CastSkillUI[] _castSkillUIArr; // 캐스팅 스킬 UI 들 배열

    ///// <summary>
    ///// OnEnable
    ///// </summary>
    //private void OnEnable()
    //{
    //    EquipSkillManager.OnEquipSkillChanged += Update_CastSkillUIArr; // 장착스킬 바뀌었을때 -> 캐스팅스킬UI들 업데이트
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
    //    // 장착스킬 가져오기
    //    ISkill[] equipSkillArr = EquipSkillManager.GetEquipISkill(); 

    //    // 아예 비었으면 UI 다 숨기고 리턴
    //    if (equipSkillArr == null)
    //    {
    //        foreach (CastSkillUI castSkillUI in _castSkillUIArr)
    //            castSkillUI.Hide();

    //        return;
    //    }

    //    // 장착한스킬들을, UI에 바인딩
    //    for (int i = 0; i < equipSkillArr.Length; i++)
    //    {
    //        // 비었으면 UI 숨기기
    //        if (equipSkillArr[i] == null)
    //        {
    //            _castSkillUIArr[i].Hide();
    //            continue;
    //        }

    //        // 안비었으면 UI에 그 스킬 바인딩
    //        _castSkillUIArr[i].Init_BindSkill(equipSkillArr[i]);
    //    }
    //}
}
