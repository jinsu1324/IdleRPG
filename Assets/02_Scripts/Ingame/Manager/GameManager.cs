using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    // 1. 데이터 매니저들 Init 
    // 2. 서버에서 데이터 받아서 뿌려주기
    // 3. 필요한 모든것들 초기화 시작

    //public static event Action OnGameInit;

    //private void Start()
    //{
    //    UpgradeManager.SetUpgrades_ByDefualt(); // 업그레이드 초기값
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
            UpgradeManager.SetUpgrades_ByDefualt(); // 업그레이드 초기값

        }
        else
        {
            Debug.Log("서버저장된거 가져올게");
            // 아니면 저장된 서버에서 데이터 지정
            await LoadDataFromServer();
        }


        Init_Game();
    }



    /// <summary>
    /// 서버데이터들 불러오기
    /// </summary>
    private async Task LoadDataFromServer()
    {
        await SaveLoadManager.Instance.LoadAll();
    }

    private void Init_Game()
    {
        Debug.Log("Init_Game");


        

        // 초기 골드와 젬 설정
        //GoldManager.AddGold(10000);
        //GemManager.AddGem(100);
    }
}
