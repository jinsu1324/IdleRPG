using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static string LoadTargetSceneName; // ���������� �̵��� �� �̸��� ����

    public static void LoadScene(string sceneName)
    {
        LoadTargetSceneName = sceneName;
        SceneManager.LoadScene("02_LoadingScene"); // �ε������� �̵�
    }
}
