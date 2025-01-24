using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingViewManager : MonoBehaviour
{
    [SerializeField] private LoadingView _loadingView;  // �ε���

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        GameManager.OnLoadDataStart += _loadingView.Show;    // ������ �ε� ���������� -> �ε��� �ѱ�
        GameManager.OnLoadDataComplete += _loadingView.Hide; // ������ �ε� �������� -> �ε��� ����
    }                                                             

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        GameManager.OnLoadDataStart -= _loadingView.Show;
        GameManager.OnLoadDataComplete -= _loadingView.Hide;
    }
}
