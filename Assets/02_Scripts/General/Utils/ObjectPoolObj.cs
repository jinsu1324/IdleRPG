using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolObj : SerializedMonoBehaviour
{
    private Transform _parent; // ���̾��Ű���� ������ �θ�
    private Vector3 _originalLocalScale; // ���� ���ý����� ���� (�θ� �ٲ����� ������ ����������)
    private Vector3 _originalPos;

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(Transform trans)
    {
        _parent = trans;
        _originalLocalScale = transform.localScale;
        _originalPos = transform.localPosition;
    }

    /// <summary>
    /// ���� (����ϱ� ���� Ȱ��ȭ)
    /// </summary>
    public virtual void Spawn()
    {
        // �ѱ�
        gameObject.SetActive(true);
    }

    /// <summary>
    /// �ٽ� Ǯ�� ����������
    /// </summary>
    public virtual void BackTrans()
    {
        // ����
        gameObject.SetActive(false);

        // �θ� ���� �θ�� ����
        if (_parent != null)
            transform.SetParent(_parent);

        // �� �θ���, �ڽ� ����Ʈ���� �� ù��° ��ġ�� �̵�
        transform.SetAsFirstSibling();

        // �����ϵ� ���� �����ϴ�� ����
        transform.localScale = _originalLocalScale;

        transform.localPosition = _originalPos;
    }

    /// <summary>
    /// �����ð� �Ŀ� �ٽ� Ǯ�� ����������
    /// </summary>
    public virtual void BackTrans_AfterTime(float time)
    {
        Invoke("BackTrans", time);
    }

    /// <summary>
    /// �������� �������� �����Ϸ� ����
    /// </summary>
    public void SetScale_ToOriginalScale()
    {
        transform.localScale = _originalLocalScale;
    }
}
