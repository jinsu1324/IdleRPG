using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroller : MonoBehaviour
{
    private float _speed = 20.0f;    // ��� �̵� �ӵ�
    private float _resetX;          // ���� ��ġ�� ���ư� x��ǥ
    private Vector3 _startPos;      // ��������Ʈ�� ���� ���� ��ġ

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageBuilding += BGScrolling; // �������� ������ -> ��� ��ũ�Ѹ�
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
        _resetX = _startPos.x - GetComponent<SpriteRenderer>().bounds.size.x; // �� ���� ��� ����
    }

    /// <summary>
    /// ��� �̵� �ڷ�ƾ
    /// </summary>
    private IEnumerator BGScrolling_Coroutine()
    {
        float elapsedTime = 0f; // ��� �ð� ����

        while (elapsedTime < 2.0f) // 2�� ���� �ݺ�
        {
            transform.position += Vector3.left * _speed * Time.deltaTime;

            // ���� ���� �����ϸ� ���������� �̵��Ͽ� �ݺ�
            if (transform.position.x <= _resetX)
                transform.position = _startPos;

            elapsedTime += Time.deltaTime; // �ð� ����
            
            yield return null; // �� ������ ��� (Update�� ���� ȿ��)
        }
    }

    /// <summary>
    /// �������� ���� �� ��� �̵� ����
    /// </summary>
    public void BGScrolling()
    {
        StartCoroutine(BGScrolling_Coroutine());
    }
}
