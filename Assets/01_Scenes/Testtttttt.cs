using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Testtttttt : MonoBehaviour
{
    private static Dictionary<int, Item> _equipSkillDict = new Dictionary<int, Item>()
    {
        //{ 1, null },
        //{ 2, new Item("Armor_SteelArmor", "Armor", 1, 1) },
        ////{ 3, new Item("Weapon_Sword", "Weapon", 1, 1) },
        //{ 3, null },
    };

    private void Start()
    {
        //Debug.Log(_equipSkillDict.Values.Count);

        //Debug.Log(GetEmptySlotIndex());

        //Debug.Log(IsEquipMax());

        Item item = new Item("Weapon_Sword", "Weapon", 1, 1);

        Debug.Log(IsEquipped(item));

        //Item item = GetEquipSkill(2);
        //if (item != null)
        //    Debug.Log(item.ID);
        //else
        //    Debug.Log("null!");

    }


    /// <summary>
    /// 장착한 아이템인지?
    /// </summary>
    public static bool IsEquipped(Item item)
    {
        return _equipSkillDict.Values.Any(x => x.ID == item.ID);
    }

    /// <summary>
    /// 최대로 착용했는지?
    /// </summary>
    private static bool IsEquipMax()
    {
        CheckAnd_SetDict();

        return _equipSkillDict.Values.All(x => x != null);
    }

    /// <summary>
    /// 해당 아이템이 장착되어있는 슬롯인덱스 가져오기
    /// </summary>
    private static int GetEquippedIndex(Item item)
    {
        foreach (var kvp in _equipSkillDict)
        {
            int keyIndex = kvp.Key;
            Item existItem = kvp.Value;

            if (existItem == null)
                continue;

            if (existItem.ID == item.ID)
                return keyIndex;
        }

        return -1;
    }


    /// <summary>
    /// 비어있는 슬롯 인덱스 가져오기
    /// </summary>
    private static int GetEmptySlotIndex()
    {
        CheckAnd_SetDict();

        foreach (var kvp in _equipSkillDict)
        {
            int keyIndex = kvp.Key;
            Item item = kvp.Value;

            if (item == null)
                return keyIndex;
        }

        return -1;
    }

    /// <summary>
    /// 해당 슬롯에 장착되어있는 스킬아이템 가져오기
    /// </summary>
    public static Item GetEquipSkill(int slotIndex)
    {
        return _equipSkillDict[slotIndex];
    }








    /// <summary>
    /// 딕셔너리에 키 체크해보고 없으면 딕셔너리 만들기
    /// </summary>
    private static void CheckAnd_SetDict()
    {
        for (int i = 1; i <= 3; i++)
        {
            if (_equipSkillDict.ContainsKey(i) == false)
                _equipSkillDict[i] = null;
            else
                continue;
        }
    }



    /// <summary>
    /// 장착한 스킬들 가져오기 (사용하기 위해 '스킬'형태로 변환해서)
    /// </summary>
    public static Skill[] GetEquipSkills_SkillType()
    {
        CheckAnd_SetDict();

        Skill[] castSkillArr = new Skill[3];

        int index = 0;
        foreach (var kvp in _equipSkillDict)
        {
            int keyIndex = kvp.Key;
            Item item = kvp.Value;

            if (item == null)
            {
                castSkillArr[index] = null;
                index++;
                continue;
            }

            castSkillArr[index] = SkillFactory.CreateSkill(item.ID, item.Level);
        }

        return castSkillArr;
    }






}
