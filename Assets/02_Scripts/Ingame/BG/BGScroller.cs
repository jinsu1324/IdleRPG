using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    private float _speed = 20.0f;    // 배경 이동 속도
    private float _resetX;          // 원래 위치로 돌아갈 x좌표
    private Vector3 _startPos;      // 스프라이트의 원래 시작 위치
    private Coroutine _scrollCoroutine; // 현재 실행 중인 코루틴 저장

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageBuildStart += StartScrolling;   // 스테이지 빌딩 시작시 -> 배경 스크롤링
        StageManager.OnStageBuildFinish += StopScrolling;   // 스테이지 빌딩 종료시 -> 배경 스크롤링 멈추기
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageBuildStart -= StartScrolling;
        StageManager.OnStageBuildFinish -= StopScrolling;
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
    /// 배경을 무한으로 스크롤하는 코루틴
    /// </summary>
    private IEnumerator ScrollContinuously()
    {
        while (true)
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;

            // 왼쪽 끝에 도달하면 오른쪽으로 이동하여 반복
            if (transform.position.x <= _resetX)
            {
                transform.position = _startPos;
            }

            yield return null; // 한 프레임 대기 (Update 역할)
        }
    }

    /// <summary>
    /// 배경 스크롤링 시작
    /// </summary>
    public void StartScrolling(StageBuildArgs args)
    {
        if (_scrollCoroutine == null) // 이미 실행 중이면 중복 실행 방지
        {
            _scrollCoroutine = StartCoroutine(ScrollContinuously());
        }
    }

    /// <summary>
    /// 배경 스크롤링 정지
    /// </summary>
    public void StopScrolling(StageBuildArgs args)
    {
        if (_scrollCoroutine != null)
        {
            StopCoroutine(_scrollCoroutine);
            _scrollCoroutine = null;
        }
    }
}
