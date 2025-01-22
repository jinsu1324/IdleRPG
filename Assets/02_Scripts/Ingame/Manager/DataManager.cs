using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{


    /// <summary>
    /// Awake �ϴ�...
    /// </summary>
    protected override void Awake()
    {
        // �̱��� ����
        base.Awake(); 

        UpgradeManager upgradeManager = new UpgradeManager();
        upgradeManager.Init();
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // �ʱ� ���� �� ����
        GoldManager.AddGold(10000);
        GemManager.AddGem(100);
    }

}
