using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageInfoUI : MonoBehaviour
{
    // LayoutGroup�� Unity�� UI �ý��ۿ��� OnEnable ���� LateUpdate �ܰ迡�� ���̾ƿ��� ������Ʈ�ϱ� ������
    // Start ������ UI ��ġ�� �ùٸ��� ���� �� �ֽ��ϴ�.
    [InfoBox("StagePos�� ���ִ� �θ��� LayoutGroup�� ���ּ���.", InfoMessageType.Info)]

    [SerializeField] private TextMeshProUGUI _currentText;       // ���� é�� �������� �ؽ�Ʈ

    [SerializeField] private RectTransform _currentPosArrow;
    [SerializeField] private List<RectTransform> _stagePosList;

    private void OnEnable()
    {
        StageManager.Instance.OnStageChanged += UpdateUI;
        Init();
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    private void Init()
    {
        // Todo �ӽõ�����
        OnStageChangedArgs args = new OnStageChangedArgs() { CurrentChapter = 1, CurrentStage = 1 };
        UpdateUI(args);
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    private void UpdateUI(OnStageChangedArgs args)
    {
        int currentChapter = args.CurrentChapter;
        int currentStage = args.CurrentStage;

        _currentPosArrow.localPosition = _stagePosList[currentStage - 1].localPosition + new Vector3(0, -30, 0);
        _currentText.text = $"{currentChapter}-{currentStage}";
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.Instance.OnStageChanged -= UpdateUI;
    }
}
