using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class Parsing_QuestData : Parsing_Base
{
    // 시트 이름
    private readonly string _sheetName_Item = "Quest";

    /// <summary>
    /// 메뉴
    /// </summary>
    [MenuItem("My Menu/Fetch QuestData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_QuestData>().Show();
    }

    /// <summary>
    /// 버튼
    /// </summary>
    [Button("Fetch QuestData", ButtonSizes.Large)]
    public void Fetch_QuestData()
    {
        Request_DataSheet(_sheetName_Item);
    }

    /// <summary>
    /// 파싱
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseQuestData(json, sheetName);
    }

    /// <summary>
    /// 퀘스트 데이터 파싱
    /// </summary>
    private void ParseQuestData(string json, string sheetName)
    {
        // Json 데이터를 JsonFormat 객체로 디시리얼라이즈함 (문자열에서 객체로 변환)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // 헤더들
        var headers = jsonData.values[1];

        // 파싱한 데이터를 담을 스크립터블 새로 생성
        QuestDatasSO questDatasSO = CreateInstance<QuestDatasSO>();

        // 헤더 밑 부분부터 전체 반복
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1개의 열 데이터들
            var row = jsonData.values[i];

            // 데이터 할당
            QuestData questData = new QuestData();
            questData.QuestType = (QuestType)Enum.Parse(typeof(QuestType), row[0]);
            questData.Desc = row[1];
            questData.TargetValue = int.Parse(row[2]);
            questData.RewardGold = int.Parse(row[3]);

            // 퀘스트 데이터 리스트에 추가
            questDatasSO.QuestDataList.Add(questData);
        }

        // 데이터 다 들어간 QuestDatasSO를 저장
        SaveScriptableObjects(questDatasSO, sheetName);
    }

    /// <summary>
    /// 스크립터블 오브젝트로 저장
    /// </summary>
    private void SaveScriptableObjects(QuestDatasSO questDatasSO, string sheetName)
    {
        // 폴더가 없으면 새로 생성
        string folderPath = $"Assets/Resources/Data/Quest/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        // ScriptableObject 경로 설정
        string path = $"{folderPath}{sheetName}.asset";

        // ScriptableObject가 이미 있는지 확인
        QuestDatasSO existQuestDatasSO = AssetDatabase.LoadAssetAtPath<QuestDatasSO>(path);

        // ScriptableObject가 없으면 새로 생성, 있으면 그 Load한 데이터를 그대로 사용해서 덮어쓰기
        if (existQuestDatasSO == null)
        {
            existQuestDatasSO = CreateInstance<QuestDatasSO>();
            AssetDatabase.CreateAsset(existQuestDatasSO, path);
        }
        else
        {
            Debug.Log($"{sheetName} 데이터가 존재해서 업데이트했습니다.");
        }

        // QuestDataList가 null이면 초기화
        if (existQuestDatasSO.QuestDataList == null)
            existQuestDatasSO.QuestDataList = new List<QuestData>();

        // 데이터 덮어쓰기
        existQuestDatasSO.QuestDataList = new List<QuestData>(questDatasSO.QuestDataList);

        // ScriptableObject 저장
        EditorUtility.SetDirty(existQuestDatasSO); // 변경된 데이터 저장
        AssetDatabase.SaveAssets(); // 에셋 저장
    }
}
