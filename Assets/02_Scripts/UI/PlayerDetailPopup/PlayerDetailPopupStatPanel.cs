using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetailPopupStatPanel : MonoBehaviour
{
    [SerializeField] private PlayerDetailPopupStatSlot _playerDetailPopupStatSlot;  // ���� ����
    [SerializeField] private RectTransform _slotParent;                             // ���Ե� ������ �θ�

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        SpawnSlots();

    }

    /// <summary>
    /// ���Ե� ���� + �ʱ�ȭ
    /// </summary>
    private void SpawnSlots()
    {
        // �÷��̾� ���� ������ŭ �ݺ�
        foreach (Stat stat in PlayerStatContainer.Instance.GetAllStats())
        {
            PlayerDetailPopupStatSlot slot = Instantiate(_playerDetailPopupStatSlot, _slotParent);
            slot.Init(stat.StatID);
        }
    }
}
