using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    public static event Action OnLoadDataStart;     // ������ �ε� ���������� �̺�Ʈ
    public static event Action OnLoadDataComplete;  // ������ �ε� �������� �̺�Ʈ

    public static event Action OnNewGame;           // ���� �����ϴ� �����϶� �̺�Ʈ

    /// <summary>
    /// Async Start (���۽� ������ �ε� �õ�)
    /// </summary>
    private async void Start()
    {
        Application.targetFrameRate = 60;

        await TryLoadData();

        SoundManager.Instance.PlayBGM(BGMType.BGM_PlayScene_ver2);
        //SoundManager.Instance.SetBGMVolume(0.2f);
    }

    /// <summary>
    /// ������ �ε� �õ� (ID������ �ʱⰪ, ������ �������� ������ �ҷ�����)
    /// </summary>
    public async Task TryLoadData()
    {
        GameTimeController.Pause();

        OnLoadDataStart?.Invoke();

        bool existUserID = await SaveLoadManager.ExistUserID();

        if (existUserID == false)
        {
            Debug.Log("A ------------ ID ��� �ʱⰪ���� ����!");

            GoldManager.SetDefaultGold();
            GemManager.SetDefaultGem();
            UpgradeManager.SetUpgrades_ByDefualt();

            OnNewGame?.Invoke();

            Debug.Log("A ------------ �ʱⰪ ���� �Ϸ�!");
        }
        else
        {
            Debug.Log("B ------------ ID �־ ��������Ȱɷ� �ҷ�����!");

            await SaveLoadManager.Instance.LoadAll();

            Debug.Log("B ------------ ���� �ҷ����� �Ϸ�!!");
        }

        GameInit();

        await Task.Delay(1000); // 1�� ���

        OnLoadDataComplete?.Invoke();

        GameTimeController.Resume();
    }

    /// <summary>
    /// ���� �ʱ�ȭ
    /// </summary>
    private void GameInit()
    {
        Debug.Log("���� �ʱ�ȭ ����...");

        StageManager.Instance.StageBuildAndStart_GameInit();
        QuestManager.SetCurrentQuest();

        Debug.Log("���� �ʱ�ȭ �Ϸ�!");
    }

    /// <summary>
    /// �����˾����� ����� �����ͷε�õ� ��ư
    /// </summary>
    public void TryLoadDataButton() => _ = TryLoadData(); // '_ =' ��ȯ�� ���� (��ī��(discard))
}
