using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 장비 데이터 파싱 에디터 윈도우
/// </summary>
public class Parsing_GearData : Parsing_Base
{
    // 시트 이름
    private readonly string _sheetName_Gear = "Gear";

    /// <summary>
    /// 메뉴
    /// </summary>
    [MenuItem("My Menu/Fetch GearData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_GearData>().Show();
    }

    /// <summary>
    /// 버튼
    /// </summary>
    [Button("Fetch GearData", ButtonSizes.Large)]
    public void Fetch_GearData()
    {
        Request_DataSheet(_sheetName_Gear);
    }

    /// <summary>
    /// 파싱
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseGearData(json, sheetName);
    }

    /// <summary>
    /// 장비 데이터 파싱
    /// </summary>
    private void ParseGearData(string json, string sheetName)
    {
        // Json 데이터를 JsonFormat 객체로 디시리얼라이즈함 (문자열에서 객체로 변환)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // 헤더들
        var headers = jsonData.values[1];

        // ID별로 데이터를 그룹화하기 위한 Dictionary
        Dictionary<string, GearDataSO> gearDataSODict = new Dictionary<string, GearDataSO>();

        // 헤더 밑 부분부터 전체 반복
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1개의 열 데이터들
            var row = jsonData.values[i];

            // ID
            string id = row[0];

            // 딕셔너리에 ID로 id가 없으면 새로 데이터를어서 딕셔너리에 넣고나서 gearData로 out
            // 딕셔너리에 ID로 id가 이미 있으면, 그대로 value를 gearData에 넣어서 out 
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

            // 강화 정보들
            EnhanceCountInfo enhanceCountInfo = new EnhanceCountInfo()
            {
                Level = int.Parse(row[5]),
                EnhanceCount = int.Parse(row[6]),
            };
            gearDataSO.EnhanceCountInfoList.Add(enhanceCountInfo); // 추가


            // 레벨별 장비 정보들
            LevelGearStats levelGearStats = new LevelGearStats()
            {
                Level = int.Parse(row[5]),
                GearStatList = new List<GearStat>()
            };

            // 한 행에 있는 (한 레벨의) 어빌리티 정보들 모두 아이템 레벨별 정보의 리스트에 추가
            for (int k = 8; k < row.Length; k += 2)
            {
                if (string.IsNullOrEmpty(row[k]) == false) // 셀이 비어있지 않다면
                {
                    GearStat gearStat = new GearStat()
                    {
                        Type = row[k],
                        Value = row[k + 1]
                    };
                    levelGearStats.GearStatList.Add(gearStat);
                }
            }

            // gearData의 업그레이드 정보리스트에 한 행의 업그레이드 정보 추가
            gearDataSO.LevelGearStatsList.Add(levelGearStats);
        }

        // 스크립터블 오브젝트로 저장
        SaveScriptableObjects(gearDataSODict);
    }


    /// <summary>
    /// 스크립터블 오브젝트로 저장
    /// </summary>
    private void SaveScriptableObjects(Dictionary<string, GearDataSO> gearDataSODict)
    {
        // 폴더가 없으면 새로 생성
        string folderPath = $"Assets/Resources_moved/Data/Gear/"; // 어드레서블 사용 폴더로!
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        foreach (var kvp in gearDataSODict)
        {
            string id = kvp.Key;
            GearDataSO data = kvp.Value;

            // ScriptableObject 경로 설정
            string path = $"{folderPath}{id}.asset";

            // ScriptableObject가 이미 있는지 확인
            GearDataSO gearDataSO = AssetDatabase.LoadAssetAtPath<GearDataSO>(path);

            // ScriptableObject가 없으면 새로 생성, 있으면 그 Load한 데이터를 그대로 사용
            if (gearDataSO == null)
                gearDataSO = CreateInstance<GearDataSO>(); 
            else
                Debug.Log($"{id} 데이터가 존재해서 업데이트했습니다.");

            // ScriptableObject 생성
            gearDataSO.ID = data.ID;
            gearDataSO.ItemType = data.ItemType;
            gearDataSO.Name = data.Name;
            gearDataSO.Grade = data.Grade;
            gearDataSO.Desc = data.Desc;
            gearDataSO.EnhanceCountInfoList = data.EnhanceCountInfoList;
            gearDataSO.AttackAnimType = data.AttackAnimType;
            gearDataSO.LevelGearStatsList = data.LevelGearStatsList;

            // 에셋이 없을때만 ScriptableObject를 경로에 저장
            if (AssetDatabase.Contains(gearDataSO) == false) 
                AssetDatabase.CreateAsset(gearDataSO, path);

            // 변경된 데이터 저장
            EditorUtility.SetDirty(gearDataSO); 
        }

        // 에셋 저장
        AssetDatabase.SaveAssets();
    }
}
