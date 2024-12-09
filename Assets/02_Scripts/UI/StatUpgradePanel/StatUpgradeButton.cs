using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatUpgradeButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float _upgradeInterval = 0.1f;  // ���׷��̵� ����
    private bool _isPressed = false;        // ��ư�� ���� ���� Ȯ��
    private Coroutine _upgradeCoroutine;    // ���� �������� Coroutine ����
    private string _id;                     // ������ ���� ID

    private Vector3 _originalScale;         // ���� ������ ����

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(string id)
    {
        _id = id;

        // �ʱ� ������ ����
        _originalScale = transform.localScale;
    }

    /// <summary>
    /// �������� 1ȸ ȣ��
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        if (_upgradeCoroutine == null)
        {
            _upgradeCoroutine = StartCoroutine(UgradeRepeat());
        }
    }

    /// <summary>
    /// �� �� 1ȸ ȣ��
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;

        // ������ �� ������ �ִϸ��̼� ����
        PlayScaleAnimation();

        if (_upgradeCoroutine != null) // Coroutine�� ���� ���̸� ����
        {
            StopCoroutine(_upgradeCoroutine);
            _upgradeCoroutine = null;
        }
    }

    /// <summary>
    /// ���׷��̵� �ݺ� �ڷ�ƾ
    /// </summary>
    private IEnumerator UgradeRepeat()
    {
        while (_isPressed)
        {
            Upgrade();
            yield return new WaitForSeconds(_upgradeInterval);
        }
    }

    /// <summary>
    /// ���׷��̵�
    /// </summary>
    private void Upgrade()
    {
        // ������ �����ϸ� ������ ���������ϰ� true ��ȯ (TryLevelUpStatByID ���ο� �����Ǿ�����)
        if (PlayerStatManager.TryStatLevelUp(_id))
        {
            // ������ �ִϸ��̼� ����
            PlayScaleAnimation();
        }
        else
        {
            Debug.Log("�� ������ ���׷��̵� �� ��尡 ������� �ʽ��ϴ�!");
        }
    }

    /// <summary>
    /// ��ư ������ �ִϸ��̼�
    /// </summary>
    private void PlayScaleAnimation()
    {
        transform.DOKill(); // ���� �ִϸ��̼� �ߴ�
        transform.localScale = _originalScale;  // ������ �ʱ�ȭ

        // �۾����� Ŀ���� �ִϸ��̼�
        transform.DOScale(_originalScale * 0.8f, 0.05f) // �۾�����
            .OnComplete(() => transform.DOScale(_originalScale, 0.05f)); // ���� ũ��� ����
            
    }
}
