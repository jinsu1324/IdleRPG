using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 게임 저장
    /// </summary>
    public void SaveGame()
    {
        PlayerManager.Instance.SavePlayerData();
    }

    /// <summary>
    /// 게임 종료 시 저장 호출
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}