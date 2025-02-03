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
    /// ��ȭ�̵� �ټ�
    /// </summary>
    public void MoveCurrency_Multi(CurrencyType currencyType, Vector3 startPos)
    {
         StartCoroutine(MoveMultiCoroutine(currencyType, startPos));
    }

    /// <summary>
    /// ��ȭ�̵� �ټ� �ڷ�ƾ
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
    /// ��ȭ �̵� �̱�
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
        float height = Random.Range(-1.0f, 1.0f); // ������ �ִ� ����

        // ���� �ð� ���� ��ġ�� ���������ϸ� �̵�
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, progress);

            // ������ ȿ�� �߰� (sin ��� �̿��� �ε巯�� � �̵�)
            float arc = Mathf.Sin(progress * Mathf.PI) * height;

            if (endPos == _goldDestination.position)
                currentPos.y += arc; // ���� y�࿡ ������ �߰�
            else
                currentPos.x += arc; // ���̸� x �࿡ ������ �߰�

            currencyPrefab.transform.position = currentPos;

            // Time.unscaledDeltaTime ����Ͽ� Time.timeScale ������ ���� �ʵ��� ��
            elapsedTime += Time.unscaledDeltaTime;
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
