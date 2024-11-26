using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// JSON 형식에 맞춘 클래스. Google Sheets API에서 받은 데이터를 이 형식으로 파싱.
/// </summary>
public class JsonFormat
{
    public string range;                // 요청된 데이터 범위 (예: "WaveData!A1:G7").    
    public string majorDimension;       // 데이터의 주요 차원 (ROWS 또는 COLUMNS).
    public string[][] values;           // 실제 데이터를 저장하는 2차원 배열. 각 행은 배열로 표현됨.
}
