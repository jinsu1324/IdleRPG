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
                Debug.Log(match.Groups[1].Value); // 그룹 내부 문자열 출력: Duration, AddAttackSpeed

                Dictionary<SkillStatType, float> skillStats = skillDataSO.GetSkillStats(newItem.Level);


            }




        }

    }


    /// <summary>
    /// 아이템 생성
    /// </summary>
    private Item CreateItem()
    {
        // 해당 아이템타입의 모든 데이터 스크립터블 오브젝트들 중에서 1개만 고르기
        List<ItemDataSO> itemDataSOList = ItemDataManager.GetItemDataSOList_ByType(ItemType.Skill);
        ItemDataSO itemDataSO = itemDataSOList[RandomIndex(itemDataSOList.Count)];

        Item item = new Item(itemDataSO.ID, itemDataSO.ItemType, 1, 1);
        return item;
    }

    /// <summary>
    /// 0-maxCount 사이 랜덤한 숫자 반환
    /// </summary>
    private int RandomIndex(int maxCount)
    {
        int index = Random.Range(0, maxCount);
        return index;
    }


}
