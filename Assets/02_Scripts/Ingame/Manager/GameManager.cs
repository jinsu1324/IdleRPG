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

        if (existUserID == false)
        {
            Debug.Log("A ---- ID 없어서 초기값으로 설정!");

            UpgradeManager.SetUpgrades_ByDefualt();
            GoldManager.SetDefaultGold();
            GemManager.SetDefaultGem();

            Debug.Log("A ---- 초기값 설정 완료!");
        }
        else
        {
            Debug.Log("B ---- ID 있어서 서버저장된걸로 불러오기!");

            await SaveLoadManager.Instance.LoadAll();

            Debug.Log("B ---- 서버 불러오기 완료!!");
        }

        GameInitialize();
    }

    private void GameInitialize()
    {
        Debug.Log("게임 초기화 시작...");


        StageManager.Instance.StageBuildAndStart();


        Debug.Log("게임 초기화 완료!");
    }
}
