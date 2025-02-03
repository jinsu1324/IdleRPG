using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    private float _speed = 20.0f;    // ��� �̵� �ӵ�
    private float _resetX;          // ���� ��ġ�� ���ư� x��ǥ
    private Vector3 _startPos;      // ��������Ʈ�� ���� ���� ��ġ
    private Coroutine _scrollCoroutine; // ���� ���� ���� �ڷ�ƾ ����

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageBuildStart += StartScrolling;   // �������� ���� ���۽� -> ��� ��ũ�Ѹ�
        StageManager.OnStageBuildFinish += StopScrolling;   // �������� ���� ����� -> ��� ��ũ�Ѹ� ���߱�
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
        _resetX = _startPos.x - GetComponent<SpriteRenderer>().bounds.size.x; // �� ���� ��� ����
    }

    /// <summary>
    /// ����� �������� ��ũ���ϴ� �ڷ�ƾ
    /// </summary>
    private IEnumerator ScrollContinuously()
    {
        while (true)
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;

            // ���� ���� �����ϸ� ���������� �̵��Ͽ� �ݺ�
            if (transform.position.x <= _resetX)
            {
                transform.position = _startPos;
            }

            yield return null; // �� ������ ��� (Update ����)
        }
    }

    /// <summary>
    /// ��� ��ũ�Ѹ� ����
    /// </summary>
    public void StartScrolling(StageBuildArgs args)
    {
        if (_scrollCoroutine == null) // �̹� ���� ���̸� �ߺ� ���� ����
        {
            _scrollCoroutine = StartCoroutine(ScrollContinuously());
        }
    }

    /// <summary>
    /// ��� ��ũ�Ѹ� ����
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
