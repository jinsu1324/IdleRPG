using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Parsing_EnemyData : Parsing_Base
{
    // ��Ʈ �̸�
    private readonly string _sheetName_Enemy = "Enemy";

    /// <summary>
    /// �޴�
    /// </summary>
    [MenuItem("My Menu/Fetch EnemyData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_EnemyData>().Show();
    }

    /// <summary>
    /// ��ư
    /// </summary>
    [Button("Fetch EnemyData", ButtonSizes.Large)]
    public void Fetch_StageData()
    {
        Request_DataSheet(_sheetName_Enemy);
    }

    /// <summary>
    /// �Ľ�
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseEnemyData(json, sheetName);
    }

    /// <summary>
    /// �������� ������ �Ľ�
    /// </summary>
    private void ParseEnemyData(string json, string sheetName)
    {
        // Json �����͸� JsonFormat ��ü�� ��ø���������� (���ڿ����� ��ü�� ��ȯ)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // �����
        var headers = jsonData.values[1];


        EnemyDatasSO enemyDatasSO = CreateInstance<EnemyDatasSO>();

        // ��� �� �κк��� ��ü �ݺ�
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1���� �� �����͵�
            var row = jsonData.values[i];

            // ������ �Ҵ��� �ű� stageData ������ְ�
            EnemyData enemyData = new EnemyData();

            // ������ �Ҵ�
            enemyData.ID = row[0];
            enemyData.Name = row[1];
            enemyData.MaxHp = float.Parse(row[2]);
            enemyData.MoveSpeed = float.Parse(row[3]);
            enemyData.AttackPower = float.Parse(row[4]);
            enemyData.AttackSpeed = float.Parse(row[5]);
            enemyData.DropGold = int.Parse(row[6]);

            // ����Ʈ�� �߰�
            enemyDatasSO.EnemyDataList.Add(enemyData);
        }

        // ��ũ���ͺ� ������Ʈ�� ����
        SaveScriptableObjects(enemyDatasSO);
    }


    /// <summary>
    /// ��ũ���ͺ� ������Ʈ�� ����
    /// </summary>
    private void SaveScriptableObjects(EnemyDatasSO enemyDatasSO)
    {
        // ������ ������ ���� ����
        string folderPath = $"Assets/Resources/Data/Enemy/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        // ScriptableObject ��� ����
        string path = $"{folderPath}{_sheetName_Enemy}.asset";

        // ScriptableObject�� �̹� �ִ��� Ȯ��
        EnemyDatasSO existEnemyDatasSO = AssetDatabase.LoadAssetAtPath<EnemyDatasSO>(path);

        // ScriptableObject�� ������ ���� ����, ������ �� Load�� �����͸� �״�� ���
        if (existEnemyDatasSO == null)
            existEnemyDatasSO = CreateInstance<EnemyDatasSO>();
        else
            Debug.Log($"{_sheetName_Enemy} �����Ͱ� �����ؼ� ������Ʈ�߽��ϴ�.");

        // ScriptableObject ������ �Ҵ� (or �����)
        existEnemyDatasSO.EnemyDataList = enemyDatasSO.EnemyDataList;

        // ������ �������� ScriptableObject�� ��ο� ����
        if (AssetDatabase.Contains(existEnemyDatasSO) == false)
            AssetDatabase.CreateAsset(existEnemyDatasSO, path);

        // ����� ������ ����
        EditorUtility.SetDirty(existEnemyDatasSO);

        // ���� ����
        AssetDatabase.SaveAssets();
    }
}
