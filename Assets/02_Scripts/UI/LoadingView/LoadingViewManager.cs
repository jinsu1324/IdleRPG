using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingViewManager : MonoBehaviour
{
    [SerializeField] private LoadingView _loadingView;  // 로딩뷰

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        GameManager.OnLoadDataStart += _loadingView.Show;    // 데이터 로드 시작했을때 -> 로딩뷰 켜기
        GameManager.OnLoadDataComplete += _loadingView.Hide; // 데이터 로드 끝났을때 -> 로딩뷰 끄기
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
