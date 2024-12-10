using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoadManager
{
    // ���� ���
    private static string _filePath = Application.persistentDataPath + "/SaveData.json";

    /// <summary>
    /// ������ ����
    /// </summary>
    public static void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data); // JSON ����ȭ
        File.WriteAllText(_filePath, json); // ���Ϸ� ����
        Debug.Log($"������ ����");
    }

    /// <summary>
    /// ������ �ҷ�����
    /// </summary>
    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);  // ���� �б�
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);   // JSON ������ȭ
            Debug.Log($"������ �ҷ�����");
            return data;
        }
        else
        {
            Debug.Log("����� ������ �����ϴ�.");
            return null;
        }
    }
}
