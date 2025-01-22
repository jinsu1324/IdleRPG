using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    KillEnemy,
    CollectGold,
    UpgradeAttackPower,
    UpgradeAttackSpeed,
    UpgradeMaxHp,
    UpgradeCriticalRate,
    UpgradeCriticalMultiple,
    ReachStage
}

[System.Serializable]
public class QuestData
{
    public int Index;
    public QuestType QuestType;
    public string Desc;
    public int TargetValue;
    public int RewardGem;
}
