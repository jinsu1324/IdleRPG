using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentText;       // ���� é�� �������� �ؽ�Ʈ
    [SerializeField] private GameObject _infiniteAnimIcon;       // ���ѹݺ� �ִϸ��̼� ������
    [SerializeField] private Button _challangeButton;            // ���� ��ư

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageBuildStart += UpdateStageInfoUI;   // �������� ���� ���۵ɶ� -> �������� ���� UI ������Ʈ

        _challangeButton.onClick.AddListener(StageManager.Instance.ChallangeRestartGame);
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageBuildStart -= UpdateStageInfoUI;

        _challangeButton.onClick.RemoveAllListeners();
    }
   
    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    private void UpdateStageInfoUI(StageBuildArgs args)
    {
        int currentStage = args.CurrentStage;
        _currentText.text = $"Stage {currentStage}";

        InfiniteGOs_OnOff(); // ���� �������� �ִϸ��̼� �ѱ� / ����
    }

    /// <summary>
    /// ���� ������������ ���ӿ�����Ʈ�� �ѱ� / ����
    /// </summary>
    private void InfiniteGOs_OnOff()
    {
        _infiniteAnimIcon.gameObject.SetActive(StageManager.Instance.IsInfiniteStage());
        _challangeButton.gameObject.SetActive(StageManager.Instance.IsInfiniteStage());
    }
}
