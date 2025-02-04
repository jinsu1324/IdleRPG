using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogViewManager : MonoBehaviour
{
    [SerializeField] private DialogView _dialogView;    // ���̾�α� ��

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        GameManager.OnNewGame += _dialogView.Show; // ���� �����ϴ� �����϶� -> ���̾�α� �ѱ�
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        GameManager.OnNewGame -= _dialogView.Show;
    }
}
