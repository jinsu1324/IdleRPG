using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Parsing_DefaultUpgradeData : Parsing_Base
{
    // 시트 이름
    private readonly string _sheetName_DefaultUpgrade = "DefaultUpgrade";

    /// <summary>
    /// 메뉴
    /// </summary>
    [MenuItem("My Menu/Fetch DefaultUpgradeData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_DefaultUpgradeData>().Show();
    }

    /// <summary>
    /// 버튼
    /// </summary>
    [Button("Fetch DefaultUpgradeData", ButtonSizes.Large)]
    public void Fetch_StageData()
    {
        Request_DataSheet(_sheetName_DefaultUpgrade);
    }

    /// <summary>
    /// 파싱
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseDefaultUpgradeData(json, sheetName);
    }

    /// <summary>
    /// 디폴트 업그레이드 데이터 파싱
    /// </summary>
    private void ParseDefaultUpgradeData(string json, string sheetName)
    {
        // Json 데이터를 JsonFormat 객체로 디시리얼라이즈함 (문자열에서 객체로 변환)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // 헤더들
        var headers = jsonData.values[1];

        DefaultUpgradeDatasSO defaultUpgradeDatasSO = CreateInstance<DefaultUpgradeDatasSO>();

        // 헤더 밑 부분부터 전체 반복
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1개의 열 데이터들
            var row = jsonData.values[i];

            // 데이터 할당할 신규 Upgrade 만들어주고
            Upgrade upgrade = new Upgrade();

            // 데이터 할당
            upgrade.UpgradeStatType = row[0];
            upgrade.Name = row[1];
            upgrade.Level = int.Parse(row[2]);
            upgrade.Value = float.Parse(row[3]);
            upgrade.ValueIncrease = float.Parse(row[4]);
            upgrade.Cost = int.Parse(row[5]);
            upgrade.CostIncrease = int.Parse(row[6]);

            // 리스트에 추가
            defaultUpgradeDatasSO.DefaultUpgradeDataList.Add(upgrade);
        }

        // 스크립터블 오브젝트로 저장
        SaveScriptableObjects(defaultUpgradeDatasSO);
    }


    /// <summary>
    /// 스크립터블 오브젝트로 저장
    /// </summary>
    private void SaveScriptableObjects(DefaultUpgradeDatasSO defaultUpgradeDatasSO)
    {
        // 폴더가 없으면 새로 생성
        string folderPath = $"Assets/Resources/Data/DefaultUpgrade/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        // ScriptableObject 경로 설정
        string path = $"{folderPath}{_sheetName_DefaultUpgrade}.asset";

        // ScriptableObject가 이미 있는지 확인
        DefaultUpgradeDatasSO exist = AssetDatabase.LoadAssetAtPath<DefaultUpgradeDatasSO>(path);

        // ScriptableObject가 없으면 새로 생성, 있으면 그 Load한 데이터를 그대로 사용
        if (exist == null)
            exist = CreateInstance<DefaultUpgradeDatasSO>();
        else
            Debug.Log($"{_sheetName_DefaultUpgrade} 데이터가 존재해서 업데이트했습니다.");

        // ScriptableObject 데이터 할당 (or 덮어쓰기)
        exist.DefaultUpgradeDataList = defaultUpgradeDatasSO.DefaultUpgradeDataList;

        // 에셋이 없을때만 ScriptableObject를 경로에 저장
        if (AssetDatabase.Contains(exist) == false)
            AssetDatabase.CreateAsset(exist, path);

        // 변경된 데이터 저장
        EditorUtility.SetDirty(exist);

        // 에셋 저장
        AssetDatabase.SaveAssets();
    }

}
