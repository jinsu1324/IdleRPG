using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class Parsing_QuestData : Parsing_Base
{
    // ��Ʈ �̸�
    private readonly string _sheetName_Item = "Quest";

    /// <summary>
    /// �޴�
    /// </summary>
    [MenuItem("My Menu/Fetch QuestData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_QuestData>().Show();
    }

    /// <summary>
    /// ��ư
    /// </summary>
    [Button("Fetch QuestData", ButtonSizes.Large)]
    public void Fetch_QuestData()
    {
        Request_DataSheet(_sheetName_Item);
    }

    /// <summary>
    /// �Ľ�
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseQuestData(json, sheetName);
    }

    /// <summary>
    /// ����Ʈ ������ �Ľ�
    /// </summary>
    private void ParseQuestData(string json, string sheetName)
    {
        // Json �����͸� JsonFormat ��ü�� ��ø���������� (���ڿ����� ��ü�� ��ȯ)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // �����
        var headers = jsonData.values[1];

        // �Ľ��� �����͸� ���� ��ũ���ͺ� ���� ����
        QuestDatasSO questDatasSO = CreateInstance<QuestDatasSO>();

        // ��� �� �κк��� ��ü �ݺ�
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1���� �� �����͵�
            var row = jsonData.values[i];

            // ������ �Ҵ�
            QuestData questData = new QuestData();
            questData.QuestType = (QuestType)Enum.Parse(typeof(QuestType), row[0]);
            questData.Desc = row[1];
            questData.TargetValue = int.Parse(row[2]);
            questData.RewardGold = int.Parse(row[3]);

            // ����Ʈ ������ ����Ʈ�� �߰�
            questDatasSO.QuestDataList.Add(questData);
        }

        // ������ �� �� QuestDatasSO�� ����
        SaveScriptableObjects(questDatasSO, sheetName);
    }

    /// <summary>
    /// ��ũ���ͺ� ������Ʈ�� ����
    /// </summary>
    private void SaveScriptableObjects(QuestDatasSO questDatasSO, string sheetName)
    {
        // ������ ������ ���� ����
        string folderPath = $"Assets/Resources/Data/Quest/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        // ScriptableObject ��� ����
        string path = $"{folderPath}{sheetName}.asset";

        // ScriptableObject�� �̹� �ִ��� Ȯ��
        QuestDatasSO existQuestDatasSO = AssetDatabase.LoadAssetAtPath<QuestDatasSO>(path);

        // ScriptableObject�� ������ ���� ����, ������ �� Load�� �����͸� �״�� ����ؼ� �����
        if (existQuestDatasSO == null)
        {
            existQuestDatasSO = CreateInstance<QuestDatasSO>();
            AssetDatabase.CreateAsset(existQuestDatasSO, path);
        }
        else
        {
            Debug.Log($"{sheetName} �����Ͱ� �����ؼ� ������Ʈ�߽��ϴ�.");
        }

        // QuestDataList�� null�̸� �ʱ�ȭ
        if (existQuestDatasSO.QuestDataList == null)
            existQuestDatasSO.QuestDataList = new List<QuestData>();

        // ������ �����
        existQuestDatasSO.QuestDataList = new List<QuestData>(questDatasSO.QuestDataList);

        // ScriptableObject ����
        EditorUtility.SetDirty(existQuestDatasSO); // ����� ������ ����
        AssetDatabase.SaveAssets(); // ���� ����
    }
}
