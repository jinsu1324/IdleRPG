using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogViewManager : MonoBehaviour
{
    [SerializeField] private DialogView _dialogView;    // 다이얼로그 뷰

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        GameManager.OnNewGame += _dialogView.Show; // 새로 시작하는 게임일때 -> 다이얼로그 켜기
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        GameManager.OnNewGame -= _dialogView.Show;
    }
}
