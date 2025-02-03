using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 재화 타입 정의
/// </summary>
public enum CurrencyType
{
    Gold,
    Gem
}

/// <summary>
/// 재화 이동을 처리
/// </summary>
public class CurrencyIconMover : SingletonBase<CurrencyIconMover>
{
    [SerializeField] private Canvas _spawnCanvas;           // 아이콘을 스폰할 캔버스
    [SerializeField] private Camera _mainCamera;            // 월드 좌표 변환용 MainCamera
    [SerializeField] private Camera _uiCamera;              // UI좌표 변환용 UICamera

    [Title("아이콘 프리팹", bold: false)]
    [SerializeField] private GameObject _goldIconPrefab;     // 골드 아이콘 프리팹
    [SerializeField] private GameObject _gemIconPrefab;      // 보석 아이콘 프리팹

    [Title("목적지", bold: false)]
    [SerializeField] private Transform _goldDestination;      // 골드 이동 목적지
    [SerializeField] private Transform _gemDestination;       // 보석 이동 목적지

    /// <summary>
    /// 재화이동 다수
    /// </summary>
    public void MoveCurrency_Multi(CurrencyType currencyType, Vector3 startPos)
    {
         StartCoroutine(MoveMultiCoroutine(currencyType, startPos));
    }

    /// <summary>
    /// 재화이동 다수 코루틴
    /// </summary>
    private IEnumerator MoveMultiCoroutine(CurrencyType currencyType, Vector3 startPos)
    {
        int randomCount = Random.Range(5, 10);

        for (int i = 0; i < randomCount; i++)
        {
            MoveCurrency(currencyType, startPos);
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }

    /// <summary>
    /// 재화 이동 싱글
    /// </summary>
    public void MoveCurrency(CurrencyType currencyType, Vector3 startPos)
    {
        // 재화타입에 따라 최종 목적지 가져오기
        Vector3 endPos = GetDestination(currencyType);

        // 재화 프리팹 스폰
        GameObject currencyPrefab = SpawnCurrencyPrefab(currencyType, startPos);

        // 생성된 재화 프리팹을 목적지로 이동
        StartCoroutine(MoveToTarget(currencyPrefab, startPos, endPos));
    }

    /// <summary>
    /// 재화 프리팹 생성
    /// </summary>
    private GameObject SpawnCurrencyPrefab(CurrencyType currencyType, Vector3 spawnPos)
    {
        // 재화 타입에 따라 프리팹 선택
        GameObject prefab = GetPrefab(currencyType);

        // 프리팹 생성 및 스폰위치 설정
        GameObject spawnedPrefab = Instantiate(prefab, _spawnCanvas.transform);
        spawnedPrefab.transform.position = spawnPos;

        return spawnedPrefab;
    }

    /// <summary>
    /// 재화를 목적지로 이동하는 코루틴
    /// </summary>
    private IEnumerator MoveToTarget(GameObject currencyPrefab, Vector3 startPos, Vector3 endPos)
    {
        // 이동시간
        float duration = 1f;
        float height = Random.Range(-1.0f, 1.0f); // 랜덤한 최대 높이

        // 일정 시간 동안 위치를 선형보간하며 이동
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, progress);

            // 포물선 효과 추가 (sin 곡선을 이용한 부드러운 곡선 이동)
            float arc = Mathf.Sin(progress * Mathf.PI) * height;

            if (endPos == _goldDestination.position)
                currentPos.y += arc; // 골드면 y축에 포물선 추가
            else
                currentPos.x += arc; // 젬이면 x 축에 포물선 추가

            currencyPrefab.transform.position = currentPos;

            // Time.unscaledDeltaTime 사용하여 Time.timeScale 영향을 받지 않도록 함
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        // 목적지로 위치 정확하게 설정
        currencyPrefab.transform.position = endPos;

        // 이동 완료 후 재화 아이콘 삭제
        Destroy(currencyPrefab.gameObject);
    }
   
    /// <summary>
    /// 재화타입에 따른 프리팹 반환
    /// </summary>
    private GameObject GetPrefab(CurrencyType currencyType)
    {
        return currencyType switch
        {
            CurrencyType.Gold   => _goldIconPrefab,
            CurrencyType.Gem    => _gemIconPrefab,
            _                   => null
        };
    }

    /// <summary>
    /// 재화타입에 따른 이동 목적지 반환
    /// </summary>
    private Vector3 GetDestination(CurrencyType currencyType)
    {
        return currencyType switch
        {
            CurrencyType.Gold   => _goldDestination.position,
            CurrencyType.Gem    => _gemDestination.position,
            _                   => new Vector3(0, 0, 0)
        };
    }
}
