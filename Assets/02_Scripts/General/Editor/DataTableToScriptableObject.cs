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
    // Google 스프레드시트의 고유 ID. URL에서 찾을 수 있음
    private readonly string _sheetId = "1vtg-eMmm15WSI_PVxF6kgvT839luUkL5D7g6Xsvsv1Q";

    // Google Cloud Console에서 발급받은 API 키. 이 키를 통해 Google Sheets API에 접근할 수 있음.
    private readonly string _apiKey = "AIzaSyATyhPBwN65Vbkg9ppq6NBOo3nLwHuqkJU";

    // 시트 이름들
    private readonly string _sheetName_weaponData = "Weapon";
    private readonly string _sheetName_armorData = "Armor";
    private readonly string _sheetName_enemyData = "Enemy";
    private readonly string _sheetName_stageData = "Stage";
    private readonly string _sheetName_skillData = "Skill";

    /// <summary>
    /// 메뉴 생성
    /// </summary>
    [MenuItem("My Menu/데이터 최신화")]
    public static void OpenWindow()
    {
        GetWindow<DataTableToScriptableObject>().Show();
    }

    // 안내 인포박스
    [InfoBox("데이터를 최신화하려면 아래 버튼을 순서대로 눌러주세요", InfoMessageType.Info)]

    /// <summary>
    /// 데이터 스크립터블 오브젝트 패치 버튼
    /// </summary>
    [Button("1. 데이터 스크립터블 오브젝트 최신화 하기!", ButtonSizes.Large)]
    public void DataFetch()
    {
        FetchAndConvertData<WeaponData, WeaponDatasSO>(_sheetName_weaponData);
        FetchAndConvertData<ArmorData, ArmorDatasSO>(_sheetName_armorData);
        FetchAndConvertData<EnemyData, EnemyDatasSO>(_sheetName_enemyData);
        FetchAndConvertData<StageData, StageDatasSO>(_sheetName_stageData);
        FetchAndConvertData<SkillData, SkillDatasSO>(_sheetName_skillData);
    }

    /// <summary>
    /// 데이터 매니저에 링크 최신화 버튼
    /// </summary>
    [Button("2. 데이터 매니저 링크 최신화 하기!", ButtonSizes.Large)]
    public void ManagerLinkFetch()
    {
        DataManager dataManager = FindObjectOfType<DataManager>();
        if (dataManager == null)
        {
            Debug.LogError("DataManager가 씬에 없습니다.");
            return;
        }


        dataManager.SetWeaponData(Resources.Load<WeaponDatasSO>($"Data/{_sheetName_weaponData}"));
        dataManager.SetArmorData(Resources.Load<ArmorDatasSO>($"Data/{_sheetName_armorData}"));
        dataManager.SetEnemyData(Resources.Load<EnemyDatasSO>($"Data/{_sheetName_enemyData}"));
        dataManager.SetStageData(Resources.Load<StageDatasSO>($"Data/{_sheetName_stageData}"));
        dataManager.SetSkillData(Resources.Load<SkillDatasSO>($"Data/{_sheetName_skillData}"));

        EditorUtility.SetDirty(dataManager);
        AssetDatabase.SaveAssets();

        // 성공 메시지 박스
        EditorUtility.DisplayDialog("성공!", $"데이터 매니저 링크가 최신화되었습니다!", "확인");
    }

    /// <summary>
    /// 데이터 가져오고 스크립터블 오브젝트로 변환
    /// </summary>
    public async void FetchAndConvertData<Data, DatasSO>(string sheetName) where Data : BaseData, new() where DatasSO : BaseDatasSO<Data>
    {
        // Google Sheets API에 데이터를 요청할 URL. 여기서 sheetId, range, apiKey를 사용하여 API 요청을 보낼 URL을 완성.
        string url = $"https://sheets.googleapis.com/v4/spreadsheets/{_sheetId}/values/{sheetName}?key={_apiKey}";

        // HttpClient는 HTTP 요청을 보내고 응답을 받을 때 사용하는 클래스.
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Google Sheets API로 GET 요청을 비동기적으로 전송하고, 서버가 응답할 때까지 기다림.
                HttpResponseMessage response = await client.GetAsync(url);

                // 서버 응답이 성공적인지 여부를 확인.
                if (response.IsSuccessStatusCode)
                {
                    // 응답 본문을 문자열로 읽어옴. 여기에는 JSON 형식의 스프레드시트 데이터가 들어있음.
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // 응답 결과를 콘솔에 출력해 확인.
                    //Debug.Log(responseBody);

                    // Scriptable오브젝트로 저장
                    CreateScriptableObject<Data, DatasSO>(responseBody, sheetName);
                }
                else
                {
                    // 응답 실패 시 에러 메시지를 출력.
                    Debug.LogError($"Failed to fetch data. Error Status Code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                // HTTP 요청 중 예외가 발생하면 에러 메시지를 출력.
                Debug.LogError($"Failed to fetch data. Request Error : {e.Message}");
            }
        }
    }

    /// <summary>
    /// // JSON 데이터를 ScriptableObject로 변환하는 메서드.
    /// </summary>
    private void CreateScriptableObject<Data, DatasSO>(string json, string sheetName) where Data : BaseData, new() where DatasSO : BaseDatasSO<Data>
    {
        // Json 데이터를 JsonFormat 객체로 디시리얼라이즈함 (문자열에서 객체로 변환)
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // 새로운 ScriptableObject를 생성. 여기에 파싱된 데이터를 저장할 것임
        DatasSO datasSO = CreateInstance<DatasSO>();
        datasSO.DataList = new List<Data>();

        // 헤더들
        var headers = jsonData.values[1];

        // 파싱된 Json 데이터의 값들을 하나씩 ScriptableObject에 채움 (2번째 인덱스부터 데이터 시작)
        for (int i = 2; i < jsonData.values.Length; i++) 
        { 
            // i행의 데이터들
            var row = jsonData.values[i];

            // 리플렉션 준비
            Type type = typeof(Data);
            Data data = new Data();

            // 헤더와 데이터를 하나씩 매핑
            for (int h = 0; h < headers.Length; h++)
            {
                // row 배열 범위를 초과하는 경우를 방지 (행의 마지막 부분이 비어있으면 ""가 아니라 아예 배열길이가 줄어들기 때문에 예외처리 필요)
                if (h >= row.Length || string.IsNullOrEmpty(row[h]))
                {
                    continue; // 해당 셀 건너뛰기
                }

                // 현재 headers[h](예: "ID")와 이름이 같은 필드를 EquipmentDataSO에서 검색 + 퍼블릭 필드와 인스턴스 필드만 검색하도록 제한
                FieldInfo fieldInfo = type.GetField(headers[h], BindingFlags.Public | BindingFlags.Instance);

                if (fieldInfo != null)
                {
                    // Reflection을 사용하여 필드에 값을 설정 + row[h] 값을 필드타입으로 데이터 타입 변환
                    fieldInfo.SetValue(data, Convert.ChangeType(row[h], fieldInfo.FieldType));
                }
            }

            // 데이터테이블에 있던 값들이 변수에 다 들어간 data를 -> datasSO DataList에 추가
            datasSO.DataList.Add(data);

            // 변경 사항을 유니티에 알림
            EditorUtility.SetDirty(datasSO);
        }

        // ScriptableObject로 저장
        string savePath = $"Assets/Resources/Data/{sheetName}.asset";
        DatasSO existingAsset = AssetDatabase.LoadAssetAtPath<DatasSO>(savePath);

        if (existingAsset == null)
        {
            // 에셋이 없으면 새로 생성
            AssetDatabase.CreateAsset(datasSO, savePath);

            // 변경 사항을 유니티에 알림
            EditorUtility.SetDirty(datasSO);
        }
        else
        {
            // 에셋이 이미 존재하면, Reflection을 사용하여 기존 에셋에 데이터 복사
            Type type = typeof(DatasSO);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                // 기존 에셋의 필드에 새 데이터 복사
                object newValue = field.GetValue(datasSO);
                field.SetValue(existingAsset, newValue);
            }

            // 변경 사항을 유니티에 알림
            EditorUtility.SetDirty(existingAsset);
        }

        // 에셋 저장
        AssetDatabase.SaveAssets();

        // 에셋이 성공적으로 저장되었음을 알리는 메시지 박스를 띄움
        EditorUtility.DisplayDialog("성공!", $"스크립터블 오브젝트가 저장 or 업데이트 되었습니다! {savePath}", "확인");
    }
}
