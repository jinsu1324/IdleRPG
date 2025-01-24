using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    // 1. ������ �Ŵ����� Init 
    // 2. �������� ������ �޾Ƽ� �ѷ��ֱ�
    // 3. �ʿ��� ���͵� �ʱ�ȭ ����

    //public static event Action OnGameInit;

    private async void Start()
    {
        bool existUserID = await SaveLoadManager.ExistUserID();

        if (existUserID == false)
        {
            Debug.Log("A ---- ID ��� �ʱⰪ���� ����!");

            UpgradeManager.SetUpgrades_ByDefualt();
            GoldManager.SetDefaultGold();
            GemManager.SetDefaultGem();

            Debug.Log("A ---- �ʱⰪ ���� �Ϸ�!");
        }
        else
        {
            Debug.Log("B ---- ID �־ ��������Ȱɷ� �ҷ�����!");

            await SaveLoadManager.Instance.LoadAll();

            Debug.Log("B ---- ���� �ҷ����� �Ϸ�!!");
        }

        GameInitialize();
    }

    private void GameInitialize()
    {
        Debug.Log("���� �ʱ�ȭ ����...");


        StageManager.Instance.StageBuildAndStart();


        Debug.Log("���� �ʱ�ȭ �Ϸ�!");
    }
}
