using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectViewManager : MonoBehaviour
{
    [SerializeField] private ConnectView _connectView;  // Ä¿³ØÆÃ ºä

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        SaveLoadManager.OnSaveStart += _connectView.Show; // ¼¼ÀÌºê ½ÃÀÛÇÒ¶§ -> Ä¿³ØÆÃºä ÄÑ±â
        SaveLoadManager.OnSaveComplete += _connectView.Hide; // ¼¼ÀÌºê ³¡³µÀ»¶§ -> Ä¿³ØÆÃºä ²ô±â
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
