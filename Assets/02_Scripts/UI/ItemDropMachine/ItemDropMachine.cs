using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// ������ ȹ�� ���
/// </summary>
public class ItemDropMachine : MonoBehaviour
{
    // ȹ�� ī��Ʈ
    [Title("ȹ�� ����", bold: false)]
    [SerializeField] 
    [Range(1, 10)] 
    private int _maxDropCount;

    // ȹ���� ������ Ÿ��
    [Title("ȹ���� ������ Ÿ��", bold: false)]
    [SerializeField] 
    private ItemType _itemType;

    // ȹ�� View
    [Title("ȹ�� View", bold: false)]
    [SerializeField] 
    private RewardView _rewardView; 


    /// <summary>
    /// ���� ������ ȹ�� ��ư
    /// </summary>
    public void AcquireRandomItemButton()
    {
        // ȹ���� �����۵� ���� ����
        List<Item> dropItemList = new List<Item>(); 

        for (int i = 0; i < _maxDropCount; i++)
        {
            // ������ �������� ��������
            Item item = RandomPickItem(); 

            // �κ��丮�� �߰�
            ItemInven.AddItem(item);

            // ������ ��������� ����Ʈ�� �߰�
            dropItemList.Add(item);
        }

        // ȹ�� view �ѱ�
        _rewardView.Show(dropItemList); 
    }

    /// <summary>
    /// ��� �������� ��
    /// </summary>
    private Item RandomPickItem()
    {
        //switch (_itemType)
        //{
        //    case ItemType.Weapon:
        //        return MakeGear(_itemType);
        //    case ItemType.Armor:
        //        return MakeGear(_itemType);
        //    case ItemType.Helmet:
        //        return MakeGear(_itemType);
        //    case ItemType.Skill:
        //        return MakeSkill(_itemType);
        //}

        Debug.Log("�ƹ���� �� �ȵǾ����ϴ�.");
        return null;
    }

    /// <summary>
    /// ��� �ν��Ͻ� ����
    /// </summary>
    private Item MakeGear(ItemType itemType)
    {
        //// ���� ������Ÿ���� �����۵����ʹ� ��� ��������
        //List<GearDataSO> gearDataSOList = DataManager.Instance.GetAllGearDataSO(itemType);

        //// �� �����۵����͵� �߿��� �ϳ� ���� ��
        //GearDataSO gearDataSO = gearDataSOList[RandomPickItemIndex(gearDataSOList.Count)];

        //GearItem gearItem = new GearItem();
        //gearItem.Init(gearDataSO, 1);

        //return gearItem;

        return null;
    }

    /// <summary>
    /// ��ų������ �ν��Ͻ� ����
    /// </summary>
    private SkillItem MakeSkill(ItemType itemType)
    {
        //// ���� ������Ÿ���� �����۵����ʹ� ��� ��������

        //List<SkillDataSO> skillDataSOList = DataManager.Instance.GetAllSkillDataSO(itemType);

        //// �� �����۵����͵� �߿��� �ϳ� ���� ��
        //SkillDataSO skillDataSO = skillDataSOList[RandomPickItemIndex(skillDataSOList.Count)];
        //string id = skillDataSO.ID;

        //SkillItem skillItem = SkillFactory.CreateSkill(id);
        //skillItem.Init(skillDataSO, 1);

        //return skillItem;

        return null;
    }

    /// <summary>
    /// ������ ���� �� �������� ��
    /// </summary>
    private int RandomPickItemIndex(int maxCount)
    {
        int index = Random.Range(0, maxCount);
        return index;
    }
}
