using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Parsing_StageData : Parsing_Base
{
    // 시트 이름
    private readonly string _sheetName_Stage = "Stage";

    /// <summary>
    /// 메뉴
    /// </summary>
    [MenuItem("My Menu/Fetch StageData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_StageData>().Show();
    }

    /// <summary>
    /// 버튼
    /// </summary>
    [Button("Fetch StageData", ButtonSizes.Large)]
    public void Fetch_StageData()
    {
        Request_DataSheet(_sheetName_Stage);
    }

    /// <summary>
    /// 파싱
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseStageData(json, sheetName);
    }

    /// <summary>
    /// 스테이지 데이터 파싱
    /// </summary>
    private void ParseStageData(string json, string sheetName)
    {
        // Json 데이터를 JsonFormat 객체로 디시리얼라이즈함 (문자열에서 객체로 변환)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // 헤더들
        var headers = jsonData.values[1];

        
        StageDatasSO stageDatasSO = CreateInstance<StageDatasSO>();

        // 헤더 밑 부분부터 전체 반복
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1개의 열 데이터들
            var row = jsonData.values[i];

            // ID
            string id = row[0];

            // 데이터 할당할 신규 stageData 만들어주고
            StageData stageData = new StageData();

            // 데이터 할당
            stageData.Stage = int.Parse(row[0]);
            stageData.AppearEnemy = row[1];
            stageData.Count = int.Parse(row[2]);
            stageData.StatPercentage = float.Parse(row[3]);

            // 리스트에 추가
            stageDatasSO.StageDataList.Add(stageData);
        }

        // 스크립터블 오브젝트로 저장
        SaveScriptableObjects(stageDatasSO);
    }


    /// <summary>
    /// 스크립터블 오브젝트로 저장
    /// </summary>
    private void SaveScriptableObjects(StageDatasSO stageDatasSO)
    {
        // 폴더가 없으면 새로 생성
        string folderPath = $"Assets/Resources/Data/Stage/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        // ScriptableObject 경로 설정
        string path = $"{folderPath}{_sheetName_Stage}.asset";

        // ScriptableObject가 이미 있는지 확인
        StageDatasSO existStageDatasSO = AssetDatabase.LoadAssetAtPath<StageDatasSO>(path);

        // ScriptableObject가 없으면 새로 생성, 있으면 그 Load한 데이터를 그대로 사용
        if (existStageDatasSO == null)
            existStageDatasSO = CreateInstance<StageDatasSO>();
        else
            Debug.Log($"{_sheetName_Stage} 데이터가 존재해서 업데이트했습니다.");

        // ScriptableObject 데이터 할당 (or 덮어쓰기)
        existStageDatasSO.StageDataList = stageDatasSO.StageDataList;

        // 에셋이 없을때만 ScriptableObject를 경로에 저장
        if (AssetDatabase.Contains(existStageDatasSO) == false)
            AssetDatabase.CreateAsset(existStageDatasSO, path);

        // 변경된 데이터 저장
        EditorUtility.SetDirty(existStageDatasSO);

        // 에셋 저장
        AssetDatabase.SaveAssets();
    }
}
