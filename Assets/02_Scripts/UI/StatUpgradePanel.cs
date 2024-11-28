using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgradePanel : MonoBehaviour
{
    [SerializeField] private StatUpgradeButton _statUpgradeButtonPrefab;
    [SerializeField] private RectTransform _buttonContainer;
    private PlayerManager _playerManager;

    public void Initialize(PlayerManager playerManager)
    {
        _playerManager = playerManager;
        InitializeStatButtons();
    }

    private void InitializeStatButtons()
    {
        foreach (StatData stat in _playerManager.GetAllStat())
        {
            StatUpgradeButton statUpgradeButton = Instantiate(_statUpgradeButtonPrefab, _buttonContainer);
            statUpgradeButton.Initialize(stat.ID, _playerManager);
        }
    }
}
