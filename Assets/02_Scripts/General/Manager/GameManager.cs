using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// ���� ����
    /// </summary>
    public void SaveGame()
    {
        PlayerManager.Instance.SavePlayerData();
    }

    /// <summary>
    /// ���� ���� �� ���� ȣ��
    /// </summary>
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}