using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class Parsing_ItemData : Parsing_Base
{
    // ��Ʈ �̸�
    private readonly string _sheetName_Item = "Item";

    /// <summary>
    /// �޴�
    /// </summary>
    [MenuItem("My Menu/Fetch ItemData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_ItemData>().Show();
    }

    /// <summary>
    /// ��ư
    /// </summary>
    [Button("Fetch ItemData", ButtonSizes.Large)]
    public void Fetch_ItemData()
    {
        Request_DataSheet(_sheetName_Item);
    }

    /// <summary>
    /// �Ľ�
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseItemData(json, sheetName);
    }

    /// <summary>
    /// ��� ������ �Ľ�
    /// </summary>
    private void ParseItemData(string json, string sheetName)
    {
        // Json �����͸� JsonFormat ��ü�� ��ø���������� (���ڿ����� ��ü�� ��ȯ)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // �����
        var headers = jsonData.values[1];

        // ID���� �����͸� �׷�ȭ�ϱ� ���� Dictionary
        Dictionary<string, ItemDataSO> itemDataSODict = new Dictionary<string, ItemDataSO>();

        // ��� �� �κк��� ��ü �ݺ�
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1���� �� �����͵�
            var row = jsonData.values[i];

            // ID
            string id = row[0];

            // ��ųʸ��� ID�� id�� ������ ���� �����͸�� ��ųʸ��� �ְ��� equipmentData�� out
            // ��ųʸ��� ID�� id�� �̹� ������, �״�� value�� equipmentData�� �־ out 
            if (itemDataSODict.TryGetValue(id, out ItemDataSO itemDataSO) == false)
            {
                itemDataSO = CreateInstance<ItemDataSO>();
                itemDataSO.ID = row[0];
                itemDataSO.ItemType = row[1];
                itemDataSO.Name = row[2];
                itemDataSO.Grade = row[3];
                itemDataSO.AttackAnimType = row[4];
                itemDataSO.UpgradeInfoList = new List<UpgradeInfo>();
                
                itemDataSODict[id] = itemDataSO;
            }

            // ���׷��̵� ���� �߰�
            UpgradeInfo upgradeInfo = new UpgradeInfo()
            {
                Level = row[5],
                ItemStatList = new List<ItemStat>()
            };

            // �� �࿡ �ִ� (�� ������) ���� ������ ��� ���׷��̵� ������ ���ȸ���Ʈ�� �߰�
            for (int k = 6; k < row.Length; k += 2)
            {
                if (string.IsNullOrEmpty(row[k]) == false) // ���� ������� �ʴٸ�
                {
                    ItemStat stat = new ItemStat()
                    {
                        StatType = row[k],
                        StatValue = row[k + 1]
                    };
                    upgradeInfo.ItemStatList.Add(stat);
                }
            }

            // itemData�� ���׷��̵� ��������Ʈ�� �� ���� ���׷��̵� ���� �߰�
            itemDataSO.UpgradeInfoList.Add(upgradeInfo);
        }

        // ��ũ���ͺ� ������Ʈ�� ����
        SaveScriptableObjects(itemDataSODict);
    }


    /// <summary>
    /// ��ũ���ͺ� ������Ʈ�� ����
    /// </summary>
    private void SaveScriptableObjects(Dictionary<string, ItemDataSO> itemDataSODict)
    {
        // ������ ������ ���� ����
        string folderPath = $"Assets/Resources/Data/Item/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        foreach (var kvp in itemDataSODict)
        {
            string id = kvp.Key;
            ItemDataSO data = kvp.Value;

            // ScriptableObject ��� ����
            string path = $"{folderPath}{id}.asset";

            // ScriptableObject�� �̹� �ִ��� Ȯ��
            ItemDataSO itemDataSO = AssetDatabase.LoadAssetAtPath<ItemDataSO>(path);

            // ScriptableObject�� ������ ���� ����, ������ �� Load�� �����͸� �״�� ���
            if (itemDataSO == null)
                itemDataSO = CreateInstance<ItemDataSO>(); 
            else
                Debug.Log($"{id} �����Ͱ� �����ؼ� ������Ʈ�߽��ϴ�.");

            // ScriptableObject ����
            itemDataSO.ID = data.ID;
            itemDataSO.ItemType = data.ItemType;
            itemDataSO.Name = data.Name;
            itemDataSO.Grade = data.Grade;
            itemDataSO.AttackAnimType = data.AttackAnimType;
            itemDataSO.UpgradeInfoList = data.UpgradeInfoList;

            // ������ �������� ScriptableObject�� ��ο� ����
            if (AssetDatabase.Contains(itemDataSO) == false) 
                AssetDatabase.CreateAsset(itemDataSO, path);

            // ����� ������ ����
            EditorUtility.SetDirty(itemDataSO); 
        }

        // ���� ����
        AssetDatabase.SaveAssets();
    }
}
