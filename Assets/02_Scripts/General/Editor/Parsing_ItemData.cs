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
    // 시트 이름
    private readonly string _sheetName_Item = "Item";

    /// <summary>
    /// 메뉴
    /// </summary>
    [MenuItem("My Menu/Fetch ItemData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_ItemData>().Show();
    }

    /// <summary>
    /// 버튼
    /// </summary>
    [Button("Fetch ItemData", ButtonSizes.Large)]
    public void Fetch_ItemData()
    {
        Request_DataSheet(_sheetName_Item);
    }

    /// <summary>
    /// 파싱
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseItemData(json, sheetName);
    }

    /// <summary>
    /// 장비 데이터 파싱
    /// </summary>
    private void ParseItemData(string json, string sheetName)
    {
        // Json 데이터를 JsonFormat 객체로 디시리얼라이즈함 (문자열에서 객체로 변환)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // 헤더들
        var headers = jsonData.values[1];

        // ID별로 데이터를 그룹화하기 위한 Dictionary
        Dictionary<string, ItemDataSO> itemDataSODict = new Dictionary<string, ItemDataSO>();

        // 헤더 밑 부분부터 전체 반복
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1개의 열 데이터들
            var row = jsonData.values[i];

            // ID
            string id = row[0];

            // 딕셔너리에 ID로 id가 없으면 새로 데이터를어서 딕셔너리에 넣고나서 equipmentData로 out
            // 딕셔너리에 ID로 id가 이미 있으면, 그대로 value를 equipmentData에 넣어서 out 
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

            // 업그레이드 인포 추가
            UpgradeInfo upgradeInfo = new UpgradeInfo()
            {
                Level = row[5],
                ItemStatList = new List<ItemStat>()
            };

            // 한 행에 있는 (한 레벨의) 스탯 정보들 모두 업그레이드 인포의 스탯리스트에 추가
            for (int k = 6; k < row.Length; k += 2)
            {
                if (string.IsNullOrEmpty(row[k]) == false) // 셀이 비어있지 않다면
                {
                    ItemStat stat = new ItemStat()
                    {
                        StatType = row[k],
                        StatValue = row[k + 1]
                    };
                    upgradeInfo.ItemStatList.Add(stat);
                }
            }

            // itemData의 업그레이드 정보리스트에 한 행의 업그레이드 정보 추가
            itemDataSO.UpgradeInfoList.Add(upgradeInfo);
        }

        // 스크립터블 오브젝트로 저장
        SaveScriptableObjects(itemDataSODict);
    }


    /// <summary>
    /// 스크립터블 오브젝트로 저장
    /// </summary>
    private void SaveScriptableObjects(Dictionary<string, ItemDataSO> itemDataSODict)
    {
        // 폴더가 없으면 새로 생성
        string folderPath = $"Assets/Resources/Data/Item/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        foreach (var kvp in itemDataSODict)
        {
            string id = kvp.Key;
            ItemDataSO data = kvp.Value;

            // ScriptableObject 경로 설정
            string path = $"{folderPath}{id}.asset";

            // ScriptableObject가 이미 있는지 확인
            ItemDataSO itemDataSO = AssetDatabase.LoadAssetAtPath<ItemDataSO>(path);

            // ScriptableObject가 없으면 새로 생성, 있으면 그 Load한 데이터를 그대로 사용
            if (itemDataSO == null)
                itemDataSO = CreateInstance<ItemDataSO>(); 
            else
                Debug.Log($"{id} 데이터가 존재해서 업데이트했습니다.");

            // ScriptableObject 생성
            itemDataSO.ID = data.ID;
            itemDataSO.ItemType = data.ItemType;
            itemDataSO.Name = data.Name;
            itemDataSO.Grade = data.Grade;
            itemDataSO.AttackAnimType = data.AttackAnimType;
            itemDataSO.UpgradeInfoList = data.UpgradeInfoList;

            // 에셋이 없을때만 ScriptableObject를 경로에 저장
            if (AssetDatabase.Contains(itemDataSO) == false) 
                AssetDatabase.CreateAsset(itemDataSO, path);

            // 변경된 데이터 저장
            EditorUtility.SetDirty(itemDataSO); 
        }

        // 에셋 저장
        AssetDatabase.SaveAssets();
    }
}
