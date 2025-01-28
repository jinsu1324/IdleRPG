using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = UnityEngine.Random;

public class Testtttttt : MonoBehaviour
{

    private void Start()
    {
        Item newItem = CreateItem();

        Debug.Log(newItem.ID);

        ItemDataSO itemDataSO = ItemDataManager.GetItemDataSO(newItem.ID);
        if (itemDataSO is SkillDataSO skillDataSO)
        {
            string text = skillDataSO.Desc;
            string pattern = @"\{(.*?)\}";

            MatchCollection matches = Regex.Matches(text, pattern);

            foreach (Match match in matches)
            {
                Debug.Log(match.Groups[1].Value); // �׷� ���� ���ڿ� ���: Duration, AddAttackSpeed

                Dictionary<SkillStatType, float> skillStats = skillDataSO.GetSkillStats(newItem.Level);


            }




        }

    }


    /// <summary>
    /// ������ ����
    /// </summary>
    private Item CreateItem()
    {
        // �ش� ������Ÿ���� ��� ������ ��ũ���ͺ� ������Ʈ�� �߿��� 1���� ����
        List<ItemDataSO> itemDataSOList = ItemDataManager.GetItemDataSOList_ByType(ItemType.Skill);
        ItemDataSO itemDataSO = itemDataSOList[RandomIndex(itemDataSOList.Count)];

        Item item = new Item(itemDataSO.ID, itemDataSO.ItemType, 1, 1);
        return item;
    }

    /// <summary>
    /// 0-maxCount ���� ������ ���� ��ȯ
    /// </summary>
    private int RandomIndex(int maxCount)
    {
        int index = Random.Range(0, maxCount);
        return index;
    }


}
