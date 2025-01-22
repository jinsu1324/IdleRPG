using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : SingletonBase<DataManager>
{
    [Title("���ʷ� ������", Bold = false)]
    [SerializeField] public StartingUpgradeDatasSO StartingUpgradeDatasSO { get; private set; }   // ��Ÿ�� ���׷��̵� ������


    /// <summary>
    /// Awake �ϴ�...
    /// </summary>
    protected override void Awake()
    {
        // �̱��� ����
        base.Awake(); 

        // ��Ÿ�� ���׷��̵� ������ ����
        UpgradeManager upgradeManager = new UpgradeManager();
        upgradeManager.Init(StartingUpgradeDatasSO.DataList);

        // �� ��� ��差 ������ ����
        EnemyDropGoldManager enemyDropGoldManager = new EnemyDropGoldManager();
        enemyDropGoldManager.Init();
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


    // �����͵� set �Լ���
    public void SetStartingUpgradeDatasSO(StartingUpgradeDatasSO data) => StartingUpgradeDatasSO = data;
}
