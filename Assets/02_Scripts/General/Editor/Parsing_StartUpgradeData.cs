using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Parsing_StartUpgradeData : Parsing_Base
{
    // ��Ʈ �̸�
    private readonly string _sheetName_StartUpgrade = "StartUpgrade";

    /// <summary>
    /// �޴�
    /// </summary>
    [MenuItem("My Menu/Fetch StartUpgradeData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_StartUpgradeData>().Show();
    }


    /// <summary>
    /// ��ư
    /// </summary>
    [Button("Fetch StartUpgradeData", ButtonSizes.Large)]
    public void Fetch_StageData()
    {
        Request_DataSheet(_sheetName_StartUpgrade);
    }

    /// <summary>
    /// �Ľ�
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseStartUpgradeData(json, sheetName);
    }

    /// <summary>
    /// ��Ÿ�� ���׷��̵� ������ �Ľ�
    /// </summary>
    private void ParseStartUpgradeData(string json, string sheetName)
    {
        // Json �����͸� JsonFormat ��ü�� ��ø���������� (���ڿ����� ��ü�� ��ȯ)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // �����
        var headers = jsonData.values[1];


        StartUpgradeDatasSO startUpgradeData = CreateInstance<StartUpgradeDatasSO>();

        // ��� �� �κк��� ��ü �ݺ�
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1���� �� �����͵�
            var row = jsonData.values[i];

            // ������ �Ҵ��� �ű� Upgrade ������ְ�
            Upgrade upgrade = new Upgrade();

            // ������ �Ҵ�
            upgrade.ID = row[0];
            upgrade.Name = row[1];
            upgrade.Level = int.Parse(row[2]);
            upgrade.Value = float.Parse(row[3]);
            upgrade.ValueIncrease = float.Parse(row[4]);
            upgrade.Cost = int.Parse(row[5]);
            upgrade.CostIncrease = int.Parse(row[6]);

            // ����Ʈ�� �߰�
            startUpgradeData.StartUpgradeDataList.Add(upgrade);
        }

        // ��ũ���ͺ� ������Ʈ�� ����
        SaveScriptableObjects(startUpgradeData);
    }


    /// <summary>
    /// ��ũ���ͺ� ������Ʈ�� ����
    /// </summary>
    private void SaveScriptableObjects(StartUpgradeDatasSO startUpgradeDatasSO)
    {
        // ������ ������ ���� ����
        string folderPath = $"Assets/Resources/Data/StartUpgrade/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        // ScriptableObject ��� ����
        string path = $"{folderPath}{_sheetName_StartUpgrade}.asset";

        // ScriptableObject�� �̹� �ִ��� Ȯ��
        StartUpgradeDatasSO exist = AssetDatabase.LoadAssetAtPath<StartUpgradeDatasSO>(path);

        // ScriptableObject�� ������ ���� ����, ������ �� Load�� �����͸� �״�� ���
        if (exist == null)
            exist = CreateInstance<StartUpgradeDatasSO>();
        else
            Debug.Log($"{_sheetName_StartUpgrade} �����Ͱ� �����ؼ� ������Ʈ�߽��ϴ�.");

        // ScriptableObject ������ �Ҵ� (or �����)
        exist.StartUpgradeDataList = startUpgradeDatasSO.StartUpgradeDataList;

        // ������ �������� ScriptableObject�� ��ο� ����
        if (AssetDatabase.Contains(exist) == false)
            AssetDatabase.CreateAsset(exist, path);

        // ����� ������ ����
        EditorUtility.SetDirty(exist);

        // ���� ����
        AssetDatabase.SaveAssets();
    }

}
