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

    //private void Start()
    //{
    //    UpgradeManager.SetUpgrades_ByDefualt(); // ���׷��̵� �ʱⰪ
    //}

    private void Start()
    {
        InitDatas();
    }

    private async void InitDatas()
    {
        bool existUserID = await SaveLoadManager.ExistUserID();

        if (existUserID == false)
        {
            UpgradeManager.SetUpgrades_ByDefualt(); // ���׷��̵� �ʱⰪ

        }
        else
        {
            Debug.Log("��������Ȱ� �����ð�");
            // �ƴϸ� ����� �������� ������ ����
            await LoadDataFromServer();
        }


        Init_Game();
    }



    /// <summary>
    /// ���������͵� �ҷ�����
    /// </summary>
    private async Task LoadDataFromServer()
    {
        await SaveLoadManager.Instance.LoadAll();
    }

    private void Init_Game()
    {
        Debug.Log("Init_Game");


        

        // �ʱ� ���� �� ����
        //GoldManager.AddGold(10000);
        //GemManager.AddGem(100);
    }
}
