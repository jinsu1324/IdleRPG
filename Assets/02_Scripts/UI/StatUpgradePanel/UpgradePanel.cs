using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private List<UpgradeSlot> _upgradeSlotList;        // ���׷��̵� ���� ����Ʈ

    private void Start()
    {
        InitUpgradeSlotList();
    }

    /// <summary>
    /// ���׷��̵� ���Ե� �ʱ�ȭ
    /// </summary>
    private void InitUpgradeSlotList()
    {
        foreach (UpgradeSlot upgradeSlot in _upgradeSlotList)
            upgradeSlot.Init();
    }
}
