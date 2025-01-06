using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 아이템 획득 기계
/// </summary>
public class ItemDropMachine : MonoBehaviour
{
    // 획득 카운트
    [Title("획득 갯수", bold: false)]
    [SerializeField] 
    [Range(1, 10)] 
    private int _maxDropCount;

    // 획득할 아이템 타입
    [Title("획득할 아이템 타입", bold: false)]
    [SerializeField] 
    private ItemType _itemType;

    // 획득 View
    [Title("획득 View", bold: false)]
    [SerializeField] 
    private RewardView _rewardView; 


    /// <summary>
    /// 랜덤 아이템 획득 버튼
    /// </summary>
    public void AcquireRandomItemButton()
    {
        // 획득 수 랜덤으로 가져오기
        //int dropCount = RandomPickDropCount(_maxDropCount); 
        int dropCount = _maxDropCount; // 일단 테스트용 랜덤말고 일정갯수로

        // 획득한 아이템들 담을 변수
        List<IItem> dropItemList = new List<IItem>(); 


        for (int i = 0; i < dropCount; i++)
        {
            // 아이템 랜덤으로 가져오기
            IItem item = RandomPickItem(); 

            // 인벤토리에 추가
            ItemInven.AddItem(item);

            // 전달할 드랍아이템 리스트에 추가
            dropItemList.Add(item);
        }

        // 획득 view 켜기
        _rewardView.Show(dropItemList); 
    }

    

    /// <summary>
    /// 장비 랜덤으로 픽
    /// </summary>
    private IItem RandomPickItem()
    {
        switch (_itemType)
        {
            case ItemType.Weapon:
                return MakeGear(_itemType);
            case ItemType.Armor:
                return MakeGear(_itemType);
            case ItemType.Helmet:
                return MakeGear(_itemType);
            case ItemType.Skill:
                return MakeSkill(_itemType);
        }

        Debug.Log("아무장비도 픽 안되었습니다.");
        return null;
    }

    private IItem MakeGear(ItemType itemType)
    {
        // 같은 아이템타입의 아이템데이터는 모두 가져오기
        List<GearDataSO> gearDataSOList = DataManager.Instance.GetAllGearDataSO(itemType);

        // 그 아이템데이터들 중에서 하나 랜덤 픽
        GearDataSO gearDataSO = gearDataSOList[RandomPickItemIndex(gearDataSOList.Count)];

        Gear gear = new Gear();
        gear.Init(gearDataSO, 1);

        return gear;
    }

    private Skill MakeSkill(ItemType itemType)
    {
        // 같은 아이템타입의 아이템데이터는 모두 가져오기
        List<SkillDataSO> skillDataSOList = DataManager.Instance.GetAllSkillDataSO(itemType);

        // 그 아이템데이터들 중에서 하나 랜덤 픽
        SkillDataSO skillDataSO = skillDataSOList[RandomPickItemIndex(skillDataSOList.Count)];

        Skill skill = new Skill();
        skill.Init(skillDataSO, 1);

        return skill;
    }




    /// <summary>
    /// 아이템 갯수 중 랜덤으로 픽
    /// </summary>
    private int RandomPickItemIndex(int maxCount)
    {
        int index = Random.Range(0, maxCount);
        return index;
    }

    /// <summary>
    /// 획득 숫자 랜덤으로 픽
    /// </summary>
    private int RandomPickDropCount(int maxCount)
    {
        int dropCount = Random.Range(1, 5 + 1);
        return dropCount;
    }
}
