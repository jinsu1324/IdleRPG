using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 데이터들 초기화 클래스
/// </summary>
public class DataInitializer : MonoBehaviour
{
    private async void Start()
    {
        Debug.Log("데이터 초기화 시작...");
        
        // 모든 데이터 로드 대기
        await LoadAllDataAsync();

        Debug.Log("모든 데이터 로드 완료! 게임을 시작합니다!");
        StartGame();
    }

    /// <summary>
    /// 모든 데이터 로드
    /// </summary>
    private async Task LoadAllDataAsync()
    {
        // 모든 데이터 매니저 로드 작업 추가
        var tasks = new List<Task>
        {
            ItemManager.LoadItemDataAsync(),
            // 여기에 데이터 로드 추가
        };

        // 모든 비동기 작업 대기
        await Task.WhenAll(tasks);
    }

    /// <summary>
    /// 게임 시작
    /// </summary>
    private void StartGame()
    {
        Debug.Log("게임 시작!");

        // 게임 시작 로직...
    }
}
