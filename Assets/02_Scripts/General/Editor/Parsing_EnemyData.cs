using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Parsing_EnemyData : Parsing_Base
{
    // 시트 이름
    private readonly string _sheetName_Enemy = "Enemy";

    /// <summary>
    /// 메뉴
    /// </summary>
    [MenuItem("My Menu/Fetch EnemyData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_EnemyData>().Show();
    }

    /// <summary>
    /// 버튼
    /// </summary>
    [Button("Fetch EnemyData", ButtonSizes.Large)]
    public void Fetch_StageData()
    {
        Request_DataSheet(_sheetName_Enemy);
    }

    /// <summary>
    /// 파싱
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseEnemyData(json, sheetName);
    }

    /// <summary>
    /// 스테이지 데이터 파싱
    /// </summary>
    private void ParseEnemyData(string json, string sheetName)
    {
        // Json 데이터를 JsonFormat 객체로 디시리얼라이즈함 (문자열에서 객체로 변환)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // 헤더들
        var headers = jsonData.values[1];


        EnemyDatasSO enemyDatasSO = CreateInstance<EnemyDatasSO>();

        // 헤더 밑 부분부터 전체 반복
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1개의 열 데이터들
            var row = jsonData.values[i];

            // 데이터 할당할 신규 stageData 만들어주고
            EnemyData enemyData = new EnemyData();

            // 데이터 할당
            enemyData.ID = row[0];
            enemyData.Name = row[1];
            enemyData.MaxHp = float.Parse(row[2]);
            enemyData.MoveSpeed = float.Parse(row[3]);
            enemyData.AttackPower = float.Parse(row[4]);
            enemyData.AttackSpeed = float.Parse(row[5]);
            enemyData.DropGold = int.Parse(row[6]);

            // 리스트에 추가
            enemyDatasSO.EnemyDataList.Add(enemyData);
        }

        // 스크립터블 오브젝트로 저장
        SaveScriptableObjects(enemyDatasSO);
    }


    /// <summary>
    /// 스크립터블 오브젝트로 저장
    /// </summary>
    private void SaveScriptableObjects(EnemyDatasSO enemyDatasSO)
    {
        // 폴더가 없으면 새로 생성
        string folderPath = $"Assets/Resources/Data/Enemy/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        // ScriptableObject 경로 설정
        string path = $"{folderPath}{_sheetName_Enemy}.asset";

        // ScriptableObject가 이미 있는지 확인
        EnemyDatasSO existEnemyDatasSO = AssetDatabase.LoadAssetAtPath<EnemyDatasSO>(path);

        // ScriptableObject가 없으면 새로 생성, 있으면 그 Load한 데이터를 그대로 사용
        if (existEnemyDatasSO == null)
            existEnemyDatasSO = CreateInstance<EnemyDatasSO>();
        else
            Debug.Log($"{_sheetName_Enemy} 데이터가 존재해서 업데이트했습니다.");

        // ScriptableObject 데이터 할당 (or 덮어쓰기)
        existEnemyDatasSO.EnemyDataList = enemyDatasSO.EnemyDataList;

        // 에셋이 없을때만 ScriptableObject를 경로에 저장
        if (AssetDatabase.Contains(existEnemyDatasSO) == false)
            AssetDatabase.CreateAsset(existEnemyDatasSO, path);

        // 변경된 데이터 저장
        EditorUtility.SetDirty(existEnemyDatasSO);

        // 에셋 저장
        AssetDatabase.SaveAssets();
    }
}
