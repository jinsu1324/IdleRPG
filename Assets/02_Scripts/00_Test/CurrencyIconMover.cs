using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ȭ Ÿ�� ����
/// </summary>
public enum CurrencyType
{
    Gold,
    Gem
}

/// <summary>
/// ��ȭ �̵��� ó��
/// </summary>
public class CurrencyIconMover : SingletonBase<CurrencyIconMover>
{
    [SerializeField] private Canvas _spawnCanvas;           // �������� ������ ĵ����
    [SerializeField] private Camera _mainCamera;            // ���� ��ǥ ��ȯ�� MainCamera
    [SerializeField] private Camera _uiCamera;              // UI��ǥ ��ȯ�� UICamera

    [Title("������ ������", bold: false)]
    [SerializeField] private GameObject _goldIconPrefab;     // ��� ������ ������
    [SerializeField] private GameObject _gemIconPrefab;      // ���� ������ ������

    [Title("������", bold: false)]
    [SerializeField] private Transform _goldDestination;      // ��� �̵� ������
    [SerializeField] private Transform _gemDestination;       // ���� �̵� ������

    /// <summary>
    /// ��ȭ �̵�
    /// </summary>
    public void MoveCurrency(CurrencyType currencyType, Vector3 startPos)
    {
        // ��ȭŸ�Կ� ���� ���� ������ ��������
        Vector3 endPos = GetDestination(currencyType);

        // ��ȭ ������ ����
        GameObject currencyPrefab = SpawnCurrencyPrefab(currencyType, startPos);

        // ������ ��ȭ �������� �������� �̵�
        StartCoroutine(MoveToTarget(currencyPrefab, startPos, endPos));
    }

    /// <summary>
    /// ��ȭ ������ ����
    /// </summary>
    private GameObject SpawnCurrencyPrefab(CurrencyType currencyType, Vector3 spawnPos)
    {
        // ��ȭ Ÿ�Կ� ���� ������ ����
        GameObject prefab = GetPrefab(currencyType);

        // ������ ���� �� ������ġ ����
        GameObject spawnedPrefab = Instantiate(prefab, _spawnCanvas.transform);
        spawnedPrefab.transform.position = spawnPos;

        return spawnedPrefab;
    }

    /// <summary>
    /// ��ȭ�� �������� �̵��ϴ� �ڷ�ƾ
    /// </summary>
    private IEnumerator MoveToTarget(GameObject currencyPrefab, Vector3 startPos, Vector3 endPos)
    {
        // �̵��ð�
        float duration = 1f; 

        // ���� �ð� ���� ��ġ�� ���������ϸ� �̵�
        for (float t = 0; t < duration; t+= Time.deltaTime)
        {
            currencyPrefab.transform.position = Vector3.Lerp(startPos, endPos, t / duration);
            yield return null;
        }

        // �������� ��ġ ��Ȯ�ϰ� ����
        currencyPrefab.transform.position = endPos;

        // �̵� �Ϸ� �� ��ȭ ������ ����
        Destroy(currencyPrefab.gameObject);
    }
   
    /// <summary>
    /// ��ȭŸ�Կ� ���� ������ ��ȯ
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
    /// ��ȭŸ�Կ� ���� �̵� ������ ��ȯ
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
