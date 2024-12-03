using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatUpgradePanel : MonoBehaviour
{
    [SerializeField] private StatUpgradeSlot _statUpgradeSlotPrefab;      // ������ ���� ���׷��̵� ���� ������
    [SerializeField] private RectTransform _slotParent;                   // ���Ե� ������ �θ�
    [SerializeField] private TextMeshProUGUI _totalCombatPowerText;       // ���� ������ �ؽ�Ʈ


    private void Awake()
    {
        if (PlayerManager.Instance != null)
            PlayerManager.Instance.OnStatChanged += Update_TotalCombatPowerText;
    }

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
        foreach (StatComponent stat in PlayerManager.Instance.GetAllStats())
        {
            StatUpgradeSlot statUpgradeSlot = Instantiate(_statUpgradeSlotPrefab, _slotParent);
            statUpgradeSlot.Init(stat.StatID);
        }

        OnStatEventArgs args = new OnStatEventArgs() { TotalCombatPower = PlayerManager.Instance.GetTotalCombatPower() };
        Update_TotalCombatPowerText(args);
    }

    /// <summary>
    /// ���� ������ �ؽ�Ʈ ������Ʈ
    /// </summary>
    private void Update_TotalCombatPowerText(OnStatEventArgs args)
    {
        Debug.Log("3 - 2. Update_TotalCombatPowerText!");
        _totalCombatPowerText.text = AlphabetNumConverter.Convert(args.TotalCombatPower);
    }

    private void OnDisable()
    {
        if (PlayerManager.Instance != null)
            PlayerManager.Instance.OnStatChanged -= Update_TotalCombatPowerText;
    }
}
