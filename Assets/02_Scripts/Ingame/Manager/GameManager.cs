using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    public static event Action OnLoadDataStart;     // 데이터 로드 시작했을때 이벤트
    public static event Action OnLoadDataComplete;  // 데이터 로드 끝났을때 이벤트

    public static event Action OnNewGame;           // 새로 시작하는 게임일때 이벤트

    /// <summary>
    /// Async Start (시작시 데이터 로드 시도)
    /// </summary>
    private async void Start()
    {
        Application.targetFrameRate = 60;

        await TryLoadData();

        SoundManager.Instance.PlayBGM(BGMType.BGM_PlayScene_ver2);
        //SoundManager.Instance.SetBGMVolume(0.2f);
    }

    /// <summary>
    /// 데이터 로드 시도 (ID없으면 초기값, 있으면 서버에서 데이터 불러오기)
    /// </summary>
    public async Task TryLoadData()
    {
        GameTimeController.Pause();

        OnLoadDataStart?.Invoke();

        bool existUserID = await SaveLoadManager.ExistUserID();

        if (existUserID == false)
        {
            Debug.Log("A ------------ ID 없어서 초기값으로 설정!");

            GoldManager.SetDefaultGold();
            GemManager.SetDefaultGem();
            UpgradeManager.SetUpgrades_ByDefualt();

            OnNewGame?.Invoke();

            Debug.Log("A ------------ 초기값 설정 완료!");
        }
        else
        {
            Debug.Log("B ------------ ID 있어서 서버저장된걸로 불러오기!");

            await SaveLoadManager.Instance.LoadAll();

            Debug.Log("B ------------ 서버 불러오기 완료!!");
        }

        GameInit();

        await Task.Delay(1000); // 1초 대기

        OnLoadDataComplete?.Invoke();

        GameTimeController.Resume();
    }

    /// <summary>
    /// 게임 초기화
    /// </summary>
    private void GameInit()
    {
        Debug.Log("게임 초기화 시작...");

        StageManager.Instance.StageBuildAndStart_GameInit();
        QuestManager.SetCurrentQuest();

        Debug.Log("게임 초기화 완료!");
    }

    /// <summary>
    /// 세팅팝업에서 사용할 데이터로드시도 버튼
    /// </summary>
    public void TryLoadDataButton() => _ = TryLoadData(); // '_ =' 반환값 무시 (디스카드(discard))
}
