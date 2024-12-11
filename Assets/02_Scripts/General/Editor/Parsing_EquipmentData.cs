using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class Parsing_EquipmentData : Parsing_Base
{
    // ��Ʈ �̸�
    private readonly string _sheetName_Equipment = "Equipment";

    /// <summary>
    /// �޴�
    /// </summary>
    [MenuItem("My Menu/Fetch EquipmentData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_EquipmentData>().Show();
    }

    /// <summary>
    /// ��ư
    /// </summary>
    [Button("Fetch EquipmentData", ButtonSizes.Large)]
    public void Fetch_EquipmentData()
    {
        Request_DataSheet(_sheetName_Equipment);
    }

    /// <summary>
    /// �Ľ�
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseEquipmentData(json, sheetName);
    }

    /// <summary>
    /// ��� ������ �Ľ�
    /// </summary>
    private void ParseEquipmentData(string json, string sheetName)
    {
        // Json �����͸� JsonFormat ��ü�� ��ø���������� (���ڿ����� ��ü�� ��ȯ)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // �����
        var headers = jsonData.values[1];

        // ID���� �����͸� �׷�ȭ�ϱ� ���� Dictionary
        Dictionary<string, EquipmentDataSO> equipmentDataSODict = new Dictionary<string, EquipmentDataSO>();

        // ��� �� �κк��� ��ü �ݺ�
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1���� �� �����͵�
            var row = jsonData.values[i];

            // ID
            string id = row[0];

            // ��ųʸ��� ID�� id�� ������ ���� �����͸�� ��ųʸ��� �ְ��� equipmentData�� out
            // ��ųʸ��� ID�� id�� �̹� ������, �״�� value�� equipmentData�� �־ out 
            if (equipmentDataSODict.TryGetValue(id, out EquipmentDataSO equipmentDataSO) == false)
            {
                equipmentDataSO = CreateInstance<EquipmentDataSO>();
                equipmentDataSO.ID = row[0];
                equipmentDataSO.EquipmentType = row[1];
                equipmentDataSO.Name = row[2];
                equipmentDataSO.Grade = row[3];
                equipmentDataSO.UpgradeInfoList = new List<UpgradeInfo>();
                
                equipmentDataSODict[id] = equipmentDataSO;
            }

            // ���׷��̵� ���� �߰�
            UpgradeInfo upgradeInfo = new UpgradeInfo()
            {
                Level = row[4],
                EquipmentStatList = new List<EquipmentStat>()
            };

            // �� �࿡ �ִ� (�� ������) ���� ������ ��� ���׷��̵� ������ ���ȸ���Ʈ�� �߰�
            for (int k = 5; k < row.Length; k += 2)
            {
                if (string.IsNullOrEmpty(row[k]) == false) // ���� ������� �ʴٸ�
                {
                    EquipmentStat stat = new EquipmentStat()
                    {
                        StatType = row[k],
                        StatValue = row[k + 1]
                    };
                    upgradeInfo.EquipmentStatList.Add(stat);
                }
            }

            // equipmentData�� ���׷��̵� ��������Ʈ�� �� ���� ���׷��̵� ���� �߰�
            equipmentDataSO.UpgradeInfoList.Add(upgradeInfo);
        }

        // ��ũ���ͺ� ������Ʈ�� ����
        SaveScriptableObjects(equipmentDataSODict);
    }


    /// <summary>
    /// ��ũ���ͺ� ������Ʈ�� ����
    /// </summary>
    private void SaveScriptableObjects(Dictionary<string, EquipmentDataSO> equipmentDataSODict)
    {
        // ������ ������ ���� ����
        string folderPath = $"Assets/Resources/Data/Equipment/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        foreach (var kvp in equipmentDataSODict)
        {
            string id = kvp.Key;
            EquipmentDataSO data = kvp.Value;

            // ScriptableObject ��� ����
            string path = $"{folderPath}{id}.asset";

            // ScriptableObject�� �̹� �ִ��� Ȯ��
            EquipmentDataSO equipmentDataSO = AssetDatabase.LoadAssetAtPath<EquipmentDataSO>(path);

            // ScriptableObject�� ������ ���� ����, ������ �� Load�� �����͸� �״�� ���
            if (equipmentDataSO == null)
                equipmentDataSO = CreateInstance<EquipmentDataSO>(); 
            else
                Debug.Log($"{id} �����Ͱ� �����ؼ� ������Ʈ�߽��ϴ�.");

            // ScriptableObject ����
            equipmentDataSO.ID = data.ID;
            equipmentDataSO.EquipmentType = data.EquipmentType;
            equipmentDataSO.Name = data.Name;
            equipmentDataSO.Grade = data.Grade;
            equipmentDataSO.UpgradeInfoList = data.UpgradeInfoList;

            // ������ �������� ScriptableObject�� ��ο� ����
            if (AssetDatabase.Contains(equipmentDataSO) == false) 
                AssetDatabase.CreateAsset(equipmentDataSO, path);

            // ����� ������ ����
            EditorUtility.SetDirty(equipmentDataSO); 
        }

        // ���� ����
        AssetDatabase.SaveAssets();
    }
}
