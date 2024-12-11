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
    }



}
