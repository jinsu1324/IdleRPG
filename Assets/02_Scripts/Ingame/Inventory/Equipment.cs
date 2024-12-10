using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    private readonly EquipmentDataSO _baseData;

    public string Name;
    public Sprite Icon;

    public Equipment(EquipmentDataSO baseData)
    {
        _baseData = baseData;
        Name = _baseData.Name;
        Icon = _baseData.Icon;
        //Debug.Log($"Equipment생성자 ID : {_baseData.Name}");
    }


    // 장비 장착
    public void Equip()
    {

    }

}
