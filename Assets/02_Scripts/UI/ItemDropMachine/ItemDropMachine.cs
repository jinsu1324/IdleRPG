using Sirenix.OdinInspector;
using System;
using System.Collections;
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
        // ȹ�� �� �������� ��������
        //int dropCount = RandomPickDropCount(_maxDropCount); 
        int dropCount = _maxDropCount; // �ϴ� �׽�Ʈ�� �������� ����������

        // ȹ���� �����۵� ���� ����
        List<IItem> dropItemList = new List<IItem>(); 


        for (int i = 0; i < dropCount; i++)
        {
            // ������ �������� ��������
            IItem item = RandomPickItem(); 

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

        Debug.Log("�ƹ���� �� �ȵǾ����ϴ�.");
        return null;
    }

    private IItem MakeGear(ItemType itemType)
    {
        // ���� ������Ÿ���� �����۵����ʹ� ��� ��������
        List<GearDataSO> gearDataSOList = DataManager.Instance.GetAllGearDataSO(itemType);

        // �� �����۵����͵� �߿��� �ϳ� ���� ��
        GearDataSO gearDataSO = gearDataSOList[RandomPickItemIndex(gearDataSOList.Count)];

        Gear gear = new Gear();
        gear.Init(gearDataSO, 1);

        return gear;
    }

    private Skill MakeSkill(ItemType itemType)
    {
        // ���� ������Ÿ���� �����۵����ʹ� ��� ��������
        List<SkillDataSO> skillDataSOList = DataManager.Instance.GetAllSkillDataSO(itemType);

        // �� �����۵����͵� �߿��� �ϳ� ���� ��
        SkillDataSO skillDataSO = skillDataSOList[RandomPickItemIndex(skillDataSOList.Count)];

        Skill skill = new Skill();
        skill.Init(skillDataSO, 1);

        return skill;
    }




    /// <summary>
    /// ������ ���� �� �������� ��
    /// </summary>
    private int RandomPickItemIndex(int maxCount)
    {
        int index = Random.Range(0, maxCount);
        return index;
    }

    /// <summary>
    /// ȹ�� ���� �������� ��
    /// </summary>
    private int RandomPickDropCount(int maxCount)
    {
        int dropCount = Random.Range(1, 5 + 1);
        return dropCount;
    }
}
