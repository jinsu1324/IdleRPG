using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class Parsing_SkillData : Parsing_Base
{
    // ��Ʈ �̸�
    private readonly string _sheetName_Skill = "Skill";

    /// <summary>
    /// �޴�
    /// </summary>
    [MenuItem("My Menu/Fetch SkillData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_SkillData>().Show();
    }

    /// <summary>
    /// ��ư
    /// </summary>
    [Button("Fetch SkillData", ButtonSizes.Large)]
    public void Fetch_SkillData()
    {
        Request_DataSheet(_sheetName_Skill);
    }

    /// <summary>
    /// �Ľ�
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseSkillData(json, sheetName);
    }

    /// <summary>
    /// ��� ������ �Ľ�
    /// </summary>
    private void ParseSkillData(string json, string sheetName)
    {
        // Json �����͸� JsonFormat ��ü�� ��ø���������� (���ڿ����� ��ü�� ��ȯ)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // �����
        var headers = jsonData.values[1];

        // ID���� �����͸� �׷�ȭ�ϱ� ���� Dictionary
        Dictionary<string, SkillDataSO> gearDataSODict = new Dictionary<string, SkillDataSO>();

        // ��� �� �κк��� ��ü �ݺ�
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1���� �� �����͵�
            var row = jsonData.values[i];

            // ID
            string id = row[0];

            // ��ųʸ��� ID�� id�� ������ ���� �����͸�� ��ųʸ��� �ְ��� gearData�� out
            // ��ųʸ��� ID�� id�� �̹� ������, �״�� value�� gearData�� �־ out 
            if (gearDataSODict.TryGetValue(id, out SkillDataSO skillDataSO) == false)
            {
                skillDataSO = CreateInstance<SkillDataSO>();
                skillDataSO.ID = row[0];
                skillDataSO.ItemType = row[1];
                skillDataSO.Name = row[2];
                skillDataSO.Grade = row[3];
                skillDataSO.Desc = row[4];
                skillDataSO.ItemLevelInfoList = new List<ItemLevelInfo>();

                gearDataSODict[id] = skillDataSO;
            }

            // ������ ������ ����
            ItemLevelInfo itemLevelInfo = new ItemLevelInfo()
            {
                Level = row[5],
                ItemAbilityList = new List<ItemAbility>()
            };

            // �� �࿡ �ִ� (�� ������) �����Ƽ ������ ��� ������ ������ ������ ����Ʈ�� �߰�
            for (int k = 6; k < row.Length; k += 2)
            {
                if (string.IsNullOrEmpty(row[k]) == false) // ���� ������� �ʴٸ�
                {
                    ItemAbility itemAbility = new ItemAbility()
                    {
                        AbilityType = row[k],
                        AbilityValue = row[k + 1]
                    };
                    itemLevelInfo.ItemAbilityList.Add(itemAbility);
                }
            }

            // gearData�� ���׷��̵� ��������Ʈ�� �� ���� ���׷��̵� ���� �߰�
            skillDataSO.ItemLevelInfoList.Add(itemLevelInfo);
        }

        // ��ũ���ͺ� ������Ʈ�� ����
        SaveScriptableObjects(gearDataSODict);
    }


    /// <summary>
    /// ��ũ���ͺ� ������Ʈ�� ����
    /// </summary>
    private void SaveScriptableObjects(Dictionary<string, SkillDataSO> skillDataSODict)
    {
        // ������ ������ ���� ����
        string folderPath = $"Assets/Resources/Data/Skill/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        foreach (var kvp in skillDataSODict)
        {
            string id = kvp.Key;
            SkillDataSO data = kvp.Value;

            // ScriptableObject ��� ����
            string path = $"{folderPath}{id}.asset";

            // ScriptableObject�� �̹� �ִ��� Ȯ��
            SkillDataSO skillDataSO = AssetDatabase.LoadAssetAtPath<SkillDataSO>(path);

            // ScriptableObject�� ������ ���� ����, ������ �� Load�� �����͸� �״�� ���
            if (skillDataSO == null)
                skillDataSO = CreateInstance<SkillDataSO>();
            else
                Debug.Log($"{id} �����Ͱ� �����ؼ� ������Ʈ�߽��ϴ�.");

            // ScriptableObject ����
            skillDataSO.ID = data.ID;
            skillDataSO.ItemType = data.ItemType;
            skillDataSO.Name = data.Name;
            skillDataSO.Grade = data.Grade;
            skillDataSO.Desc = data.Desc;
            skillDataSO.ItemLevelInfoList = data.ItemLevelInfoList;

            // ������ �������� ScriptableObject�� ��ο� ����
            if (AssetDatabase.Contains(skillDataSO) == false)
                AssetDatabase.CreateAsset(skillDataSO, path);

            // ����� ������ ����
            EditorUtility.SetDirty(skillDataSO);
        }

        // ���� ����
        AssetDatabase.SaveAssets();
    }
}
