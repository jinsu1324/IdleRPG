using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEditor;
using UnityEngine;

public abstract class Parsing_Base : OdinEditorWindow
{
    // Google 스프레드시트의 고유 ID. URL에서 찾을 수 있음
    protected readonly string _sheetId = "1vtg-eMmm15WSI_PVxF6kgvT839luUkL5D7g6Xsvsv1Q";

    // Google Cloud Console에서 발급받은 API 키. 이 키를 통해 Google Sheets API에 접근할 수 있음.
    protected readonly string _apiKey = "AIzaSyATyhPBwN65Vbkg9ppq6NBOo3nLwHuqkJU";

    /// <summary>
    /// 스프레드시트 api로 요청
    /// </summary>
    public async void Request_DataSheet(string sheetName)
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

                    // 응답 성공 하면 데이터 파싱 (각자 오버라이드)
                    Parsing(responseBody, sheetName); 
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
    /// 데이터 파싱
    /// </summary>
    public abstract void Parsing(string json, string sheetName);
}
