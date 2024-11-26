using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class DataTableToScriptableObject : OdinEditorWindow
{
    // Google ���������Ʈ�� ���� ID. URL���� ã�� �� ����
    private readonly string _sheetId = "1vtg-eMmm15WSI_PVxF6kgvT839luUkL5D7g6Xsvsv1Q";

    // Google Cloud Console���� �߱޹��� API Ű. �� Ű�� ���� Google Sheets API�� ������ �� ����.
    private readonly string _apiKey = "AIzaSyATyhPBwN65Vbkg9ppq6NBOo3nLwHuqkJU";

    // ��Ʈ �̸���
    private readonly string _sheetName_weaponData = "Weapon";
    private readonly string _sheetName_armorData = "Armor";
    private readonly string _sheetName_enemyData = "Enemy";
    private readonly string _sheetName_stageData = "Stage";
    private readonly string _sheetName_skillData = "Skill";

    /// <summary>
    /// �޴� ����
    /// </summary>
    [MenuItem("My Menu/������ �ֽ�ȭ")]
    public static void OpenWindow()
    {
        GetWindow<DataTableToScriptableObject>().Show();
    }

    // �ȳ� �����ڽ�
    [InfoBox("�����͸� �ֽ�ȭ�Ϸ��� �Ʒ� ��ư�� ������� �����ּ���", InfoMessageType.Info)]

    /// <summary>
    /// ������ ��ũ���ͺ� ������Ʈ ��ġ ��ư
    /// </summary>
    [Button("1. ������ ��ũ���ͺ� ������Ʈ �ֽ�ȭ �ϱ�!", ButtonSizes.Large)]
    public void DataFetch()
    {
        FetchAndConvertData<WeaponData, WeaponDatasSO>(_sheetName_weaponData);
        FetchAndConvertData<ArmorData, ArmorDatasSO>(_sheetName_armorData);
        FetchAndConvertData<EnemyData, EnemyDatasSO>(_sheetName_enemyData);
        FetchAndConvertData<StageData, StageDatasSO>(_sheetName_stageData);
        FetchAndConvertData<SkillData, SkillDatasSO>(_sheetName_skillData);
    }

    /// <summary>
    /// ������ �Ŵ����� ��ũ �ֽ�ȭ ��ư
    /// </summary>
    [Button("2. ������ �Ŵ��� ��ũ �ֽ�ȭ �ϱ�!", ButtonSizes.Large)]
    public void ManagerLinkFetch()
    {
        DataManager dataManager = FindObjectOfType<DataManager>();
        if (dataManager == null)
        {
            Debug.LogError("DataManager�� ���� �����ϴ�.");
            return;
        }


        dataManager.SetWeaponData(Resources.Load<WeaponDatasSO>($"Data/{_sheetName_weaponData}"));
        dataManager.SetArmorData(Resources.Load<ArmorDatasSO>($"Data/{_sheetName_armorData}"));
        dataManager.SetEnemyData(Resources.Load<EnemyDatasSO>($"Data/{_sheetName_enemyData}"));
        dataManager.SetStageData(Resources.Load<StageDatasSO>($"Data/{_sheetName_stageData}"));
        dataManager.SetSkillData(Resources.Load<SkillDatasSO>($"Data/{_sheetName_skillData}"));

        EditorUtility.SetDirty(dataManager);
        AssetDatabase.SaveAssets();

        // ���� �޽��� �ڽ�
        EditorUtility.DisplayDialog("����!", $"������ �Ŵ��� ��ũ�� �ֽ�ȭ�Ǿ����ϴ�!", "Ȯ��");
    }

    /// <summary>
    /// ������ �������� ��ũ���ͺ� ������Ʈ�� ��ȯ
    /// </summary>
    public async void FetchAndConvertData<Data, DatasSO>(string sheetName) where Data : BaseData, new() where DatasSO : BaseDatasSO<Data>
    {
        // Google Sheets API�� �����͸� ��û�� URL. ���⼭ sheetId, range, apiKey�� ����Ͽ� API ��û�� ���� URL�� �ϼ�.
        string url = $"https://sheets.googleapis.com/v4/spreadsheets/{_sheetId}/values/{sheetName}?key={_apiKey}";

        // HttpClient�� HTTP ��û�� ������ ������ ���� �� ����ϴ� Ŭ����.
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Google Sheets API�� GET ��û�� �񵿱������� �����ϰ�, ������ ������ ������ ��ٸ�.
                HttpResponseMessage response = await client.GetAsync(url);

                // ���� ������ ���������� ���θ� Ȯ��.
                if (response.IsSuccessStatusCode)
                {
                    // ���� ������ ���ڿ��� �о��. ���⿡�� JSON ������ ���������Ʈ �����Ͱ� �������.
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // ���� ����� �ֿܼ� ����� Ȯ��.
                    //Debug.Log(responseBody);

                    // Scriptable������Ʈ�� ����
                    CreateScriptableObject<Data, DatasSO>(responseBody, sheetName);
                }
                else
                {
                    // ���� ���� �� ���� �޽����� ���.
                    Debug.LogError($"Failed to fetch data. Error Status Code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                // HTTP ��û �� ���ܰ� �߻��ϸ� ���� �޽����� ���.
                Debug.LogError($"Failed to fetch data. Request Error : {e.Message}");
            }
        }
    }

    /// <summary>
    /// // JSON �����͸� ScriptableObject�� ��ȯ�ϴ� �޼���.
    /// </summary>
    private void CreateScriptableObject<Data, DatasSO>(string json, string sheetName) where Data : BaseData, new() where DatasSO : BaseDatasSO<Data>
    {
        // Json �����͸� JsonFormat ��ü�� ��ø���������� (���ڿ����� ��ü�� ��ȯ)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // ���ο� ScriptableObject�� ����. ���⿡ �Ľ̵� �����͸� ������ ����
        DatasSO datasSO = CreateInstance<DatasSO>();
        datasSO.DataList = new List<Data>();

        // �����
        var headers = jsonData.values[1];

        // �Ľ̵� Json �������� ������ �ϳ��� ScriptableObject�� ä�� (2��° �ε������� ������ ����)
        for (int i = 2; i < jsonData.values.Length; i++) 
        { 
            // i���� �����͵�
            var row = jsonData.values[i];

            // ���÷��� �غ�
            Type type = typeof(Data);
            Data data = new Data();

            // ����� �����͸� �ϳ��� ����
            for (int h = 0; h < headers.Length; h++)
            {
                // row �迭 ������ �ʰ��ϴ� ��츦 ���� (���� ������ �κ��� ��������� ""�� �ƴ϶� �ƿ� �迭���̰� �پ��� ������ ����ó�� �ʿ�)
                if (h >= row.Length || string.IsNullOrEmpty(row[h]))
                {
                    continue; // �ش� �� �ǳʶٱ�
                }

                // ���� headers[h](��: "ID")�� �̸��� ���� �ʵ带 EquipmentDataSO���� �˻� + �ۺ� �ʵ�� �ν��Ͻ� �ʵ常 �˻��ϵ��� ����
                FieldInfo fieldInfo = type.GetField(headers[h], BindingFlags.Public | BindingFlags.Instance);

                if (fieldInfo != null)
                {
                    // Reflection�� ����Ͽ� �ʵ忡 ���� ���� + row[h] ���� �ʵ�Ÿ������ ������ Ÿ�� ��ȯ
                    fieldInfo.SetValue(data, Convert.ChangeType(row[h], fieldInfo.FieldType));
                }
            }

            // ���������̺� �ִ� ������ ������ �� �� data�� -> datasSO DataList�� �߰�
            datasSO.DataList.Add(data);

            // ���� ������ ����Ƽ�� �˸�
            EditorUtility.SetDirty(datasSO);
        }

        // ScriptableObject�� ����
        string savePath = $"Assets/Resources/Data/{sheetName}.asset";
        DatasSO existingAsset = AssetDatabase.LoadAssetAtPath<DatasSO>(savePath);

        if (existingAsset == null)
        {
            // ������ ������ ���� ����
            AssetDatabase.CreateAsset(datasSO, savePath);

            // ���� ������ ����Ƽ�� �˸�
            EditorUtility.SetDirty(datasSO);
        }
        else
        {
            // ������ �̹� �����ϸ�, Reflection�� ����Ͽ� ���� ���¿� ������ ����
            Type type = typeof(DatasSO);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                // ���� ������ �ʵ忡 �� ������ ����
                object newValue = field.GetValue(datasSO);
                field.SetValue(existingAsset, newValue);
            }

            // ���� ������ ����Ƽ�� �˸�
            EditorUtility.SetDirty(existingAsset);
        }

        // ���� ����
        AssetDatabase.SaveAssets();

        // ������ ���������� ����Ǿ����� �˸��� �޽��� �ڽ��� ���
        EditorUtility.DisplayDialog("����!", $"��ũ���ͺ� ������Ʈ�� ���� or ������Ʈ �Ǿ����ϴ�! {savePath}", "Ȯ��");
    }
}
