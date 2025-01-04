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
        List<Item> dropItemList = new List<Item>(); 


        for (int i = 0; i < dropCount; i++)
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

        //Debug.Log("------------------------------");
        //ItemInven.CheckCurrentItemInven(item);  // �ϴ� ����׷� üũ��
    }

    

    /// <summary>
    /// ��� �������� ��
    /// </summary>
    private Item RandomPickItem()
    {
        // ���� ������Ÿ���� �����۵����ʹ� ��� ��������
        List<GearDataSO> itemDataSOList =  DataManager.Instance.GetAllGearDataSO(_itemType);  

        // �� �����۵����͵� �߿��� �ϳ� ���� ��
        GearDataSO itemDataSO = itemDataSOList[RandomPickItemIndex(itemDataSOList.Count)];

        // �����ȵ� �����ͷ� ������ �����
        Item item = new Item(itemDataSO, 1);

        return item;
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
