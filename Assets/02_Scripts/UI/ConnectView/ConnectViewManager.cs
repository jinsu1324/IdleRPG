using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectViewManager : MonoBehaviour
{
    [SerializeField] private ConnectView _connectView;  // Ŀ���� ��

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        SaveLoadManager.OnSaveStart += _connectView.Show; // ���̺� �����Ҷ� -> Ŀ���ú� �ѱ�
        SaveLoadManager.OnSaveComplete += _connectView.Hide; // ���̺� �������� -> Ŀ���ú� ����
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        SaveLoadManager.OnSaveStart -= _connectView.Show;
        SaveLoadManager.OnSaveComplete -= _connectView.Hide;
    }
}
