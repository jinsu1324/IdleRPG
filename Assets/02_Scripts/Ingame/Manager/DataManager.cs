using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{


    /// <summary>
    /// Awake ÀÏ´Ü...
    /// </summary>
    protected override void Awake()
    {
        // ½Ì±ÛÅæ ¸ÕÀú
        base.Awake(); 

        UpgradeManager upgradeManager = new UpgradeManager();
        upgradeManager.Init();
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // ÃÊ±â °ñµå¿Í Áª ¼³Á¤
        GoldManager.AddGold(10000);
        GemManager.AddGem(100);
    }

}
