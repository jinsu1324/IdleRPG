using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    KillEnemy,
    CollectGold
}

[System.Serializable]
public class QuestData
{
    public QuestType QuestType;
    public string Desc;
    public int TargetValue;
    public int RewardGem;
}
