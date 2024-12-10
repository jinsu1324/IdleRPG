using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoadManager
{
    // 저장 경로
    private static string _filePath = Application.persistentDataPath + "/SaveData.json";

    /// <summary>
    /// 데이터 저장
    /// </summary>
    public static void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data); // JSON 직렬화
        File.WriteAllText(_filePath, json); // 파일로 저장
        Debug.Log($"데이터 저장");
    }

    /// <summary>
    /// 데이터 불러오기
    /// </summary>
    public static PlayerData LoadPlayerData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);  // 파일 읽기
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);   // JSON 역직렬화
            Debug.Log($"데이터 불러오기");
            return data;
        }
        else
        {
            Debug.Log("저장된 파일이 없습니다.");
            return null;
        }
    }
}
