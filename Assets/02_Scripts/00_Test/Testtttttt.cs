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
    /// ������ ����������?
    /// </summary>
    public static bool IsEquipped(Item item)
    {
        return _equipSkillDict.Values.Any(x => x.ID == item.ID);
    }

    /// <summary>
    /// �ִ�� �����ߴ���?
    /// </summary>
    private static bool IsEquipMax()
    {
        CheckAnd_SetDict();

        return _equipSkillDict.Values.All(x => x != null);
    }

    /// <summary>
    /// �ش� �������� �����Ǿ��ִ� �����ε��� ��������
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
    /// ����ִ� ���� �ε��� ��������
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
    /// �ش� ���Կ� �����Ǿ��ִ� ��ų������ ��������
    /// </summary>
    public static Item GetEquipSkill(int slotIndex)
    {
        return _equipSkillDict[slotIndex];
    }








    /// <summary>
    /// ��ųʸ��� Ű üũ�غ��� ������ ��ųʸ� �����
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
    /// ������ ��ų�� �������� (����ϱ� ���� '��ų'���·� ��ȯ�ؼ�)
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
