using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEditor;
using UnityEngine;

public abstract class Parsing_Base : OdinEditorWindow
{
    // Google ���������Ʈ�� ���� ID. URL���� ã�� �� ����
    protected readonly string _sheetId = "1vtg-eMmm15WSI_PVxF6kgvT839luUkL5D7g6Xsvsv1Q";

    // Google Cloud Console���� �߱޹��� API Ű. �� Ű�� ���� Google Sheets API�� ������ �� ����.
    protected readonly string _apiKey = "AIzaSyATyhPBwN65Vbkg9ppq6NBOo3nLwHuqkJU";

    /// <summary>
    /// ���������Ʈ api�� ��û
    /// </summary>
    public async void Request_DataSheet(string sheetName)
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

                    // ���� ���� �ϸ� ������ �Ľ� (���� �������̵�)
                    Parsing(responseBody, sheetName); 
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
    /// ������ �Ľ�
    /// </summary>
    public abstract void Parsing(string json, string sheetName);
}
