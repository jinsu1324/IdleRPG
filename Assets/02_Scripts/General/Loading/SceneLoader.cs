using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static string LoadTargetSceneName; // 최종적으로 이동할 씬 이름을 저장

    public static void LoadScene(string sceneName)
    {
        LoadTargetSceneName = sceneName;
        SceneManager.LoadScene("02_LoadingScene"); // 로딩씬으로 이동
    }
}
