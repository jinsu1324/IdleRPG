using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;

public class IAPResetTool : OdinEditorWindow
{

    /// <summary>
    /// 메뉴
    /// </summary>
    [MenuItem("My Menu/IAP Reset")]
    public static void OpenWindow()
    {
        GetWindow<IAPResetTool>().Show();
    }

    /// <summary>
    /// 버튼
    /// </summary>
    [Button("IAP Reset", ButtonSizes.Large)]
    public void ResetIAP()
    {
        EditorPrefs.DeleteKey("UnityPurchasingTestStore");
        Debug.Log("Unity IAP 테스트 구매 기록이 초기화되었습니다!");
    }


}

