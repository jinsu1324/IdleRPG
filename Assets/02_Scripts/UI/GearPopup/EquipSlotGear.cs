using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ReddotComponent))]
public class EquipSlotGear : EquipSlot
{
    [Title("����� ������Ʈ (��� ������Ʈ��)", bold: false)]
    [SerializeField] private ReddotComponent _reddotComponent;  // ����� ������Ʈ

    [Title("�κ��丮 ���� ��ư", bold: false)]
    [SerializeField] private Button _invenOpenButton;

    [Title("��� �κ��丮 �˾�", bold: false)]
    [SerializeField] private GearInvenPopup _gearInvenPopup;        // ��� �κ��丮 �˾�

    protected override void OnEnable()
    {
        ItemInven.OnItemInvenChanged += UpdateReddotComponent; // ������ �ִ� �������� ����Ǿ��� ��, �������� ����� ������Ʈ
        EquipGearManager.OnEquipGearChanged += Handler;

        _invenOpenButton.onClick.AddListener(() => _gearInvenPopup.Show(_itemType));
    }

    protected override void OnDisable()
    {
        ItemInven.OnItemInvenChanged -= UpdateReddotComponent;
        EquipGearManager.OnEquipGearChanged -= Handler;


        _invenOpenButton.onClick.RemoveAllListeners();
    }

    public override void ShowInfoGO(IItem item)
    {
        base.ShowInfoGO(item);

        
    }

    public override void ShowEmptyGO()
    {
        base.ShowEmptyGO();
    }

    public void UpdateEquipSlotGear()
    {
        Gear gear = EquipGearManager.GetEquipGear(_itemType);

        if (gear != null)
        {
            ShowInfoGO(gear);
            UpdateReddotComponent(); // ����� ������Ʈ
            _gearInvenPopup.Hide(); // �����Ǹ� �˾� ����
            return;
        }
        else
        {
            ShowEmptyGO();
            UpdateReddotComponent(); // ����� ������Ʈ
            _gearInvenPopup.Hide();
            return;
        }
    }

    private void Handler(OnEquipGearChangedArgs args)
    {
        if (args.ItemType != _itemType)
            return;

        UpdateEquipSlotGear();
    }



    /// <summary>
    /// ����� ������Ʈ ������Ʈ (�κ��丮�� ��ȭ������ �������� �ִ���?)
    /// </summary>
    public void UpdateReddotComponent()
    {
        _reddotComponent.UpdateReddot(() => ItemInven.HasEnhanceableItem(_itemType));
    }
}
