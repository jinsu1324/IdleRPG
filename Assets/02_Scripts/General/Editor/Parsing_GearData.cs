using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

/// <summary>
/// ��� ������ �Ľ� ������ ������
/// </summary>
public class Parsing_GearData : Parsing_Base
{
    // ��Ʈ �̸�
    private readonly string _sheetName_Gear = "Gear";

    /// <summary>
    /// �޴�
    /// </summary>
    [MenuItem("My Menu/Fetch GearData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_GearData>().Show();
    }

    /// <summary>
    /// ��ư
    /// </summary>
    [Button("Fetch GearData", ButtonSizes.Large)]
    public void Fetch_GearData()
    {
        Request_DataSheet(_sheetName_Gear);
    }

    /// <summary>
    /// �Ľ�
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseGearData(json, sheetName);
    }

    /// <summary>
    /// ��� ������ �Ľ�
    /// </summary>
    private void ParseGearData(string json, string sheetName)
    {
        // Json �����͸� JsonFormat ��ü�� ��ø���������� (���ڿ����� ��ü�� ��ȯ)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // �����
        var headers = jsonData.values[1];

        // ID���� �����͸� �׷�ȭ�ϱ� ���� Dictionary
        Dictionary<string, GearDataSO> gearDataSODict = new Dictionary<string, GearDataSO>();

        // ��� �� �κк��� ��ü �ݺ�
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1���� �� �����͵�
            var row = jsonData.values[i];

            // ID
            string id = row[0];

            // ��ųʸ��� ID�� id�� ������ ���� �����͸�� ��ųʸ��� �ְ��� gearData�� out
            // ��ųʸ��� ID�� id�� �̹� ������, �״�� value�� gearData�� �־ out 
            if (gearDataSODict.TryGetValue(id, out GearDataSO gearDataSO) == false)
            {
                gearDataSO = CreateInstance<GearDataSO>();
                gearDataSO.ID = row[0];
                gearDataSO.ItemType = row[1];
                gearDataSO.Name = row[2];
                gearDataSO.Grade = row[3];
                gearDataSO.Desc = row[4];
                gearDataSO.EnhanceCountInfoList = new List<EnhanceCountInfo>();
                gearDataSO.AttackAnimType = row[7];
                gearDataSO.LevelGearStatsList = new List<LevelGearStats>();
                
                gearDataSODict[id] = gearDataSO;
            }

            // ��ȭ ������
            EnhanceCountInfo enhanceCountInfo = new EnhanceCountInfo()
            {
                Level = int.Parse(row[5]),
                EnhanceCount = int.Parse(row[6]),
            };
            gearDataSO.EnhanceCountInfoList.Add(enhanceCountInfo); // �߰�


            // ������ ��� ������
            LevelGearStats levelGearStats = new LevelGearStats()
            {
                Level = int.Parse(row[5]),
                GearStatList = new List<GearStat>()
            };

            // �� �࿡ �ִ� (�� ������) �����Ƽ ������ ��� ������ ������ ������ ����Ʈ�� �߰�
            for (int k = 8; k < row.Length; k += 2)
            {
                if (string.IsNullOrEmpty(row[k]) == false) // ���� ������� �ʴٸ�
                {
                    GearStat gearStat = new GearStat()
                    {
                        Type = row[k],
                        Value = row[k + 1]
                    };
                    levelGearStats.GearStatList.Add(gearStat);
                }
            }

            // gearData�� ���׷��̵� ��������Ʈ�� �� ���� ���׷��̵� ���� �߰�
            gearDataSO.LevelGearStatsList.Add(levelGearStats);
        }

        // ��ũ���ͺ� ������Ʈ�� ����
        SaveScriptableObjects(gearDataSODict);
    }


    /// <summary>
    /// ��ũ���ͺ� ������Ʈ�� ����
    /// </summary>
    private void SaveScriptableObjects(Dictionary<string, GearDataSO> gearDataSODict)
    {
        // ������ ������ ���� ����
        string folderPath = $"Assets/Resources_moved/Data/Gear/"; // ��巹���� ��� ������!
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        foreach (var kvp in gearDataSODict)
        {
            string id = kvp.Key;
            GearDataSO data = kvp.Value;

            // ScriptableObject ��� ����
            string path = $"{folderPath}{id}.asset";

            // ScriptableObject�� �̹� �ִ��� Ȯ��
            GearDataSO gearDataSO = AssetDatabase.LoadAssetAtPath<GearDataSO>(path);

            // ScriptableObject�� ������ ���� ����, ������ �� Load�� �����͸� �״�� ���
            if (gearDataSO == null)
                gearDataSO = CreateInstance<GearDataSO>(); 
            else
                Debug.Log($"{id} �����Ͱ� �����ؼ� ������Ʈ�߽��ϴ�.");

            // ScriptableObject ����
            gearDataSO.ID = data.ID;
            gearDataSO.ItemType = data.ItemType;
            gearDataSO.Name = data.Name;
            gearDataSO.Grade = data.Grade;
            gearDataSO.Desc = data.Desc;
            gearDataSO.EnhanceCountInfoList = data.EnhanceCountInfoList;
            gearDataSO.AttackAnimType = data.AttackAnimType;
            gearDataSO.LevelGearStatsList = data.LevelGearStatsList;

            // ������ �������� ScriptableObject�� ��ο� ����
            if (AssetDatabase.Contains(gearDataSO) == false) 
                AssetDatabase.CreateAsset(gearDataSO, path);

            // ����� ������ ����
            EditorUtility.SetDirty(gearDataSO); 
        }

        // ���� ����
        AssetDatabase.SaveAssets();
    }
}
