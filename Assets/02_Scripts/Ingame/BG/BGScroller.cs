using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    private float _speed = 20.0f;    // 배경 이동 속도
    private float _resetX;          // 원래 위치로 돌아갈 x좌표
    private Vector3 _startPos;      // 스프라이트의 원래 시작 위치

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageBuilding += BGScrolling; // 스테이지 빌딩시 -> 배경 스크롤링
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageBuilding += BGScrolling;
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _startPos = transform.position;
        _resetX = _startPos.x - GetComponent<SpriteRenderer>().bounds.size.x; // 한 장의 배경 길이
    }

    /// <summary>
    /// 배경 이동 코루틴
    /// </summary>
    private IEnumerator BGScrolling_Coroutine()
    {
        float elapsedTime = 0f; // 경과 시간 저장

        while (elapsedTime < 2.0f) // 2초 동안 반복
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;

            // 왼쪽 끝에 도달하면 오른쪽으로 이동하여 반복
            if (transform.position.x <= _resetX)
                transform.position = _startPos;

            elapsedTime += Time.deltaTime; // 시간 누적
            
            yield return null; // 한 프레임 대기 (Update와 같은 효과)
        }
    }

    /// <summary>
    /// 스테이지 종료 시 배경 이동 실행
    /// </summary>
    public void BGScrolling()
    {
        StartCoroutine(BGScrolling_Coroutine());
    }
}
