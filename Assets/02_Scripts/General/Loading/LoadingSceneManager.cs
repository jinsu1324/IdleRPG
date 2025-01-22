using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private Slider _progressBar;               // 프로그레스 바
    [SerializeField] private TextMeshProUGUI _progressText;     // 프로그레스 텍스트
    [SerializeField] private float _fakeProgressSpeed = 0.5f;   // 가상 프로그레스 속도

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        StartCoroutine(LoadSceneWithProgress());
    }

    /// <summary>
    /// 씬 로드 및 프로그레스 표시 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadSceneWithProgress()
    {
        // 씬 비동기 로드 시작
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneLoader.LoadTargetSceneName);

        // 자동 씬 활성화여부를 비활성화로
        asyncOperation.allowSceneActivation = false;

        // 가상 프로그레스 초기화
        float fakeProgress = 0.0f;

        // 로딩 프로그레스 업데이트
        while (asyncOperation.isDone == false)
        {
            // 실제 프로그레스가 올라가는동안 가상 프로그레스 천천히 증가
            if (fakeProgress < asyncOperation.progress)
            {
                fakeProgress += _fakeProgressSpeed * Time.deltaTime;

                // 실제 프로그레스 이상 넘어가지 않음
                fakeProgress = Mathf.Min(fakeProgress, asyncOperation.progress);
            }

            // 프로그레스 바 업데이트
            _progressBar.value = fakeProgress;
            _progressText.text = $"{Mathf.FloorToInt(fakeProgress * 100)}%";

            // 로딩이 거의 완료된 상태에서
            if (asyncOperation.progress >= 0.9f && fakeProgress >= 0.9f)
            {
                // 약간의 지연
                //yield return new WaitForSeconds(0.1f);

                // 바를 100%로 업데이트
                _progressBar.value = 1.0f;
                _progressText.text = "100%";

                // 씬 활성화
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
