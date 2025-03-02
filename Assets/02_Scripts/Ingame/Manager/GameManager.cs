using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : SingletonBase<GameManager>
{
    public static event Action OnLoadDataStart;     // 데이터 로드 시작했을때 이벤트
    public static event Action OnLoadDataComplete;  // 데이터 로드 끝났을때 이벤트

    public static event Action OnNewGame;           // 새로 시작하는 게임일때 이벤트

    [SerializeField] private TextMeshProUGUI _verText;  // 버전텍스트

    /// <summary>
    /// Async Start (시작시 데이터 로드 시도)
    /// </summary>
    private async void Start()
    {
        _verText.text = $"Version: {Application.version}";

        Application.targetFrameRate = 60; // 프레임 60으로
        QualitySettings.vSyncCount = 0; // VSync 비활성화

        await InitDatas();

        SoundManager.Instance.PlayBGM(BGMType.BGM_PlayScene_ver2);
        //SoundManager.Instance.SetBGMVolume(0.2f);
    }

    /// <summary>
    /// 처음 게임 시작할때 초기화
    /// </summary>
    public async Task InitDatas()
    {
        GameTimeController.Pause();

        OnLoadDataStart?.Invoke();

        // 게임 시작하면 무조건 초기화 로직
        GoldManager.SetDefaultGold();
        GemManager.SetDefaultGem();
        UpgradeManager.SetUpgrades_ByDefualt();

        OnNewGame?.Invoke();



        GameInit();

        await Task.Delay(1000); // 1초 대기

        OnLoadDataComplete?.Invoke();

        GameTimeController.Resume();
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
            Debug.Log("B ------------ ID 를 찾을 수 없습니다!!");
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
