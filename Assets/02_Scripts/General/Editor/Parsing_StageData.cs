using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Parsing_StageData : Parsing_Base
{
    // ��Ʈ �̸�
    private readonly string _sheetName_Stage = "Stage";

    /// <summary>
    /// �޴�
    /// </summary>
    [MenuItem("My Menu/Fetch StageData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_StageData>().Show();
    }

    /// <summary>
    /// ��ư
    /// </summary>
    [Button("Fetch StageData", ButtonSizes.Large)]
    public void Fetch_StageData()
    {
        Request_DataSheet(_sheetName_Stage);
    }

    /// <summary>
    /// �Ľ�
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseStageData(json, sheetName);
    }

    /// <summary>
    /// �������� ������ �Ľ�
    /// </summary>
    private void ParseStageData(string json, string sheetName)
    {
        // Json �����͸� JsonFormat ��ü�� ��ø���������� (���ڿ����� ��ü�� ��ȯ)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // �����
        var headers = jsonData.values[1];

        
        StageDatasSO stageDatasSO = CreateInstance<StageDatasSO>();

        // ��� �� �κк��� ��ü �ݺ�
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1���� �� �����͵�
            var row = jsonData.values[i];

            // ID
            string id = row[0];

            // ������ �Ҵ��� �ű� stageData ������ְ�
            StageData stageData = new StageData();

            // ������ �Ҵ�
            stageData.Stage = int.Parse(row[0]);
            stageData.AppearEnemy = row[1];
            stageData.Count = int.Parse(row[2]);
            stageData.StatPercentage = float.Parse(row[3]);

            // ����Ʈ�� �߰�
            stageDatasSO.StageDataList.Add(stageData);
        }

        // ��ũ���ͺ� ������Ʈ�� ����
        SaveScriptableObjects(stageDatasSO);
    }


    /// <summary>
    /// ��ũ���ͺ� ������Ʈ�� ����
    /// </summary>
    private void SaveScriptableObjects(StageDatasSO stageDatasSO)
    {
        // ������ ������ ���� ����
        string folderPath = $"Assets/Resources/Data/Stage/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        // ScriptableObject ��� ����
        string path = $"{folderPath}{_sheetName_Stage}.asset";

        // ScriptableObject�� �̹� �ִ��� Ȯ��
        StageDatasSO existStageDatasSO = AssetDatabase.LoadAssetAtPath<StageDatasSO>(path);

        // ScriptableObject�� ������ ���� ����, ������ �� Load�� �����͸� �״�� ���
        if (existStageDatasSO == null)
            existStageDatasSO = CreateInstance<StageDatasSO>();
        else
            Debug.Log($"{_sheetName_Stage} �����Ͱ� �����ؼ� ������Ʈ�߽��ϴ�.");

        // ScriptableObject ������ �Ҵ� (or �����)
        existStageDatasSO.StageDataList = stageDatasSO.StageDataList;

        // ������ �������� ScriptableObject�� ��ο� ����
        if (AssetDatabase.Contains(existStageDatasSO) == false)
            AssetDatabase.CreateAsset(existStageDatasSO, path);

        // ����� ������ ����
        EditorUtility.SetDirty(existStageDatasSO);

        // ���� ����
        AssetDatabase.SaveAssets();
    }
}
