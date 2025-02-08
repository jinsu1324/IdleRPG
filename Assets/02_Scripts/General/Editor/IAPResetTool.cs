using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;

public class IAPResetTool : OdinEditorWindow
{

    /// <summary>
    /// �޴�
    /// </summary>
    [MenuItem("My Menu/IAP Reset")]
    public static void OpenWindow()
    {
        GetWindow<IAPResetTool>().Show();
    }

    /// <summary>
    /// ��ư
    /// </summary>
    [Button("IAP Reset", ButtonSizes.Large)]
    public void ResetIAP()
    {
        EditorPrefs.DeleteKey("UnityPurchasingTestStore");
        Debug.Log("Unity IAP �׽�Ʈ ���� ����� �ʱ�ȭ�Ǿ����ϴ�!");
    }


}

