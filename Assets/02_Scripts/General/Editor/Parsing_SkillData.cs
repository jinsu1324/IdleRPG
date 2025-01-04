using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class Parsing_SkillData : Parsing_Base
{
    // 시트 이름
    private readonly string _sheetName_Skill = "Skill";

    /// <summary>
    /// 메뉴
    /// </summary>
    [MenuItem("My Menu/Fetch SkillData")]
    public static void OpenWindow()
    {
        GetWindow<Parsing_SkillData>().Show();
    }

    /// <summary>
    /// 버튼
    /// </summary>
    [Button("Fetch SkillData", ButtonSizes.Large)]
    public void Fetch_SkillData()
    {
        Request_DataSheet(_sheetName_Skill);
    }

    /// <summary>
    /// 파싱
    /// </summary>
    public override void Parsing(string json, string sheetName)
    {
        ParseSkillData(json, sheetName);
    }

    /// <summary>
    /// 장비 데이터 파싱
    /// </summary>
    private void ParseSkillData(string json, string sheetName)
    {
        // Json 데이터를 JsonFormat 객체로 디시리얼라이즈함 (문자열에서 객체로 변환)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // 헤더들
        var headers = jsonData.values[1];

        // ID별로 데이터를 그룹화하기 위한 Dictionary
        Dictionary<string, SkillDataSO> gearDataSODict = new Dictionary<string, SkillDataSO>();

        // 헤더 밑 부분부터 전체 반복
        for (int i = 2; i < jsonData.values.Length; i++)
        {
            // 1개의 열 데이터들
            var row = jsonData.values[i];

            // ID
            string id = row[0];

            // 딕셔너리에 ID로 id가 없으면 새로 데이터를어서 딕셔너리에 넣고나서 gearData로 out
            // 딕셔너리에 ID로 id가 이미 있으면, 그대로 value를 gearData에 넣어서 out 
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

            // 아이템 레벨별 정보
            ItemLevelInfo itemLevelInfo = new ItemLevelInfo()
            {
                Level = row[5],
                ItemAbilityList = new List<ItemAbility>()
            };

            // 한 행에 있는 (한 레벨의) 어빌리티 정보들 모두 아이템 레벨별 정보의 리스트에 추가
            for (int k = 6; k < row.Length; k += 2)
            {
                if (string.IsNullOrEmpty(row[k]) == false) // 셀이 비어있지 않다면
                {
                    ItemAbility itemAbility = new ItemAbility()
                    {
                        AbilityType = row[k],
                        AbilityValue = row[k + 1]
                    };
                    itemLevelInfo.ItemAbilityList.Add(itemAbility);
                }
            }

            // gearData의 업그레이드 정보리스트에 한 행의 업그레이드 정보 추가
            skillDataSO.ItemLevelInfoList.Add(itemLevelInfo);
        }

        // 스크립터블 오브젝트로 저장
        SaveScriptableObjects(gearDataSODict);
    }


    /// <summary>
    /// 스크립터블 오브젝트로 저장
    /// </summary>
    private void SaveScriptableObjects(Dictionary<string, SkillDataSO> skillDataSODict)
    {
        // 폴더가 없으면 새로 생성
        string folderPath = $"Assets/Resources/Data/Skill/";
        if (System.IO.Directory.Exists(folderPath) == false)
            System.IO.Directory.CreateDirectory(folderPath);

        foreach (var kvp in skillDataSODict)
        {
            string id = kvp.Key;
            SkillDataSO data = kvp.Value;

            // ScriptableObject 경로 설정
            string path = $"{folderPath}{id}.asset";

            // ScriptableObject가 이미 있는지 확인
            SkillDataSO skillDataSO = AssetDatabase.LoadAssetAtPath<SkillDataSO>(path);

            // ScriptableObject가 없으면 새로 생성, 있으면 그 Load한 데이터를 그대로 사용
            if (skillDataSO == null)
                skillDataSO = CreateInstance<SkillDataSO>();
            else
                Debug.Log($"{id} 데이터가 존재해서 업데이트했습니다.");

            // ScriptableObject 생성
            skillDataSO.ID = data.ID;
            skillDataSO.ItemType = data.ItemType;
            skillDataSO.Name = data.Name;
            skillDataSO.Grade = data.Grade;
            skillDataSO.Desc = data.Desc;
            skillDataSO.ItemLevelInfoList = data.ItemLevelInfoList;

            // 에셋이 없을때만 ScriptableObject를 경로에 저장
            if (AssetDatabase.Contains(skillDataSO) == false)
                AssetDatabase.CreateAsset(skillDataSO, path);

            // 변경된 데이터 저장
            EditorUtility.SetDirty(skillDataSO);
        }

        // 에셋 저장
        AssetDatabase.SaveAssets();
    }
}
