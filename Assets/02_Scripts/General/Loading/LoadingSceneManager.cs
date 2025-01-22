using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField] private Slider _progressBar;               // ���α׷��� ��
    [SerializeField] private TextMeshProUGUI _progressText;     // ���α׷��� �ؽ�Ʈ
    [SerializeField] private float _fakeProgressSpeed = 0.5f;   // ���� ���α׷��� �ӵ�

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        StartCoroutine(LoadSceneWithProgress());
    }

    /// <summary>
    /// �� �ε� �� ���α׷��� ǥ�� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadSceneWithProgress()
    {
        // �� �񵿱� �ε� ����
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneLoader.LoadTargetSceneName);

        // �ڵ� �� Ȱ��ȭ���θ� ��Ȱ��ȭ��
        asyncOperation.allowSceneActivation = false;

        // ���� ���α׷��� �ʱ�ȭ
        float fakeProgress = 0.0f;

        // �ε� ���α׷��� ������Ʈ
        while (asyncOperation.isDone == false)
        {
            // ���� ���α׷����� �ö󰡴µ��� ���� ���α׷��� õõ�� ����
            if (fakeProgress < asyncOperation.progress)
            {
                fakeProgress += _fakeProgressSpeed * Time.deltaTime;

                // ���� ���α׷��� �̻� �Ѿ�� ����
                fakeProgress = Mathf.Min(fakeProgress, asyncOperation.progress);
            }

            // ���α׷��� �� ������Ʈ
            _progressBar.value = fakeProgress;
            _progressText.text = $"{Mathf.FloorToInt(fakeProgress * 100)}%";

            // �ε��� ���� �Ϸ�� ���¿���
            if (asyncOperation.progress >= 0.9f && fakeProgress >= 0.9f)
            {
                // �ణ�� ����
                //yield return new WaitForSeconds(0.1f);

                // �ٸ� 100%�� ������Ʈ
                _progressBar.value = 1.0f;
                _progressText.text = "100%";

                // �� Ȱ��ȭ
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
