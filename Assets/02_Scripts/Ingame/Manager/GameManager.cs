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
            Debug.Log("ID ��� �ʱⰪ���� ����!");

            UpgradeManager.SetUpgrades_ByDefualt();
            GoldManager.SetDefaultGold();
            GemManager.SetDefaultGem();
        }
        else
        {
            Debug.Log("ID �־ ��������Ȱɷ� �ҷ�����!");

            await SaveLoadManager.Instance.LoadAll();
        }

        Init_Game();
    }

    private void Init_Game()
    {
        Debug.Log("Init_Game");


        

        // �ʱ� ���� �� ����
        //GoldManager.AddGold(10000);
        //GemManager.AddGem(100);
    }
}
