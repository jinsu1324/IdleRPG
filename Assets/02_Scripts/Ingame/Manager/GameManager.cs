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

    private async void Start()
    {
        bool existUserID = await SaveLoadManager.ExistUserID();

        if (existUserID == false) // 처음이면 초기값으로 
        {
            UpgradeManager.SetUpgrades_ByDefualt(); // 업그레이드 초기값

        }
        else // ID 있으면 서버저장된걸로 로드
        {
            Debug.Log("서버저장된거 가져올게");
            await SaveLoadManager.Instance.LoadAll();
        }

        Init_Game();
    }

    private void Init_Game()
    {
        Debug.Log("Init_Game");


        

        // 초기 골드와 젬 설정
        //GoldManager.AddGold(10000);
        //GemManager.AddGem(100);
    }
}
