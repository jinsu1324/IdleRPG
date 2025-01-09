using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʈ Ǯ��
/// </summary>
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private ObjectPoolObj _prefab;  // ������Ʈ Ǯ�� ������ ������
    [SerializeField] private int _count = 10;        // �ѹ��� ������ ����

    /// <summary>
    /// Awake
    /// </summary>
    protected void Awake()
    {
        CreateObjs(); // Ǯ�� ������Ʈ ����
    }

    /// <summary>
    /// Ǯ�� ������Ʈ ����
    /// </summary>
    private void CreateObjs()
    {
        for (int i = 0; i < _count; i++)
        {
            ObjectPoolObj obj = Instantiate(_prefab, transform);
            obj.Init(transform);
            obj.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ������Ʈ ����Ҷ� ��������
    /// </summary>
    public GameObject GetObj()
    {
        // ���� Ǯ�� �ڽ��� ���ٸ�, ���ο� ������Ʈ ����
        if (transform.childCount <= 0)
            CreateObjs();

        // ù ��° �ڽĺ��� Ȯ��
        int count = 0;
        GameObject child = transform.GetChild(count).gameObject;

        // ���� Ȯ���� �ڽ��� Ȱ��ȸ�Ǿ��ٸ�, �����ڽ����� �Ѿ�� ��Ȱ��ȭ�� ������Ʈ�� ã��
        while (child.activeInHierarchy)
        {
            // ���� �ڽ����� �̵�
            count++; 

            // ��� �ڽ��� Ȯ�������� ����� ������Ʈ�� ������, ���ο� ������Ʈ ����
            if (count >= transform.childCount)
                CreateObjs();

            // �ٽ� ���� �ε���(count)�� �ڽ��� ������
            child = transform.GetChild(count).gameObject;
        }

        // ä�õ� �ڽ��� Ȱ��ȭ (����)
        child.GetComponent<ObjectPoolObj>().Spawn();

        // �� �ڽ� ��ȯ
        return child;
    }

    /// <summary>
    /// ������ �̸� ��������
    /// </summary>
    public string GetPrefabName()
    {
        return _prefab.name;
    }
}
