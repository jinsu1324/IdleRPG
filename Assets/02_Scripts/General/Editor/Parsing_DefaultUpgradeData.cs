using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Parsing_DefaultUpgradeData : Parsing_Base
{
    // ��Ʈ �̸�
    private readonly string _sheetName_DefaultUpgrade = "DefaultUpgrade";

    /// <summary>
    /// �޴�
    /// </summary>
    [MenuItem("My Menu/Fetch DefaultUpgradeData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_DefaultUpgradeData>().Show();
    }

    /// <summary>
    /// ��ư
    /// </summary>
    [Button("Fetch DefaultUpgradeData", ButtonSizes.Large)]
    public void Fetch_StageData()
    {
        Request_DataSheet(_sheetName_DefaultUpgrade);
    }

    /// <summary>
    /// �Ľ�
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseDefaultUpgradeData(json, sheetName);
    }

    /// <summary>
    /// ����Ʈ ���׷��̵� ������ �Ľ�
    /// </summary>
    private void ParseDefaultUpgradeData(string json, string sheetName)
    {
        // Json �����͸� JsonFormat ��ü�� ��ø���������� (���ڿ����� ��ü�� ��ȯ)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // �����
        var headers = jsonData.values[1];

        DefaultUpgradeDatasSO defaultUpgradeDatasSO = CreateInstance<DefaultUpgradeDatasSO>();

        // ��� �� �κк��� ��ü �ݺ�
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1���� �� �����͵�
            var row = jsonData.values[i];

            // ������ �Ҵ��� �ű� Upgrade ������ְ�
            Upgrade upgrade = new Upgrade();

            // ������ �Ҵ�
            upgrade.UpgradeStatType = row[0];
            upgrade.Name = row[1];
            upgrade.Level = int.Parse(row[2]);
            upgrade.Value = float.Parse(row[3]);
            upgrade.ValueIncrease = float.Parse(row[4]);
            upgrade.Cost = int.Parse(row[5]);
            upgrade.CostIncrease = int.Parse(row[6]);

            // ����Ʈ�� �߰�
            defaultUpgradeDatasSO.DefaultUpgradeDataList.Add(upgrade);
        }

        // ��ũ���ͺ� ������Ʈ�� ����
        SaveScriptableObjects(defaultUpgradeDatasSO);
    }


    /// <summary>
    /// ��ũ���ͺ� ������Ʈ�� ����
    /// </summary>
    private void SaveScriptableObjects(DefaultUpgradeDatasSO defaultUpgradeDatasSO)
    {
        // ������ ������ ���� ����
        string folderPath = $"Assets/Resources/Data/DefaultUpgrade/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        // ScriptableObject ��� ����
        string path = $"{folderPath}{_sheetName_DefaultUpgrade}.asset";

        // ScriptableObject�� �̹� �ִ��� Ȯ��
        DefaultUpgradeDatasSO exist = AssetDatabase.LoadAssetAtPath<DefaultUpgradeDatasSO>(path);

        // ScriptableObject�� ������ ���� ����, ������ �� Load�� �����͸� �״�� ���
        if (exist == null)
            exist = CreateInstance<DefaultUpgradeDatasSO>();
        else
            Debug.Log($"{_sheetName_DefaultUpgrade} �����Ͱ� �����ؼ� ������Ʈ�߽��ϴ�.");

        // ScriptableObject ������ �Ҵ� (or �����)
        exist.DefaultUpgradeDataList = defaultUpgradeDatasSO.DefaultUpgradeDataList;

        // ������ �������� ScriptableObject�� ��ο� ����
        if (AssetDatabase.Contains(exist) == false)
            AssetDatabase.CreateAsset(exist, path);

        // ����� ������ ����
        EditorUtility.SetDirty(exist);

        // ���� ����
        AssetDatabase.SaveAssets();
    }

}
