using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoUI : MonoBehaviour
{
    // LayoutGroup�� Unity�� UI �ý��ۿ��� OnEnable ���� LateUpdate �ܰ迡�� ���̾ƿ��� ������Ʈ�ϱ� ������
    // Start ������ UI ��ġ�� �ùٸ��� ���� �� �ֽ��ϴ�.
    [InfoBox("StagePos�� ���ִ� �θ��� LayoutGroup�� ���ּ���.", InfoMessageType.Info)]

    [SerializeField] private TextMeshProUGUI _currentText;       // ���� é�� �������� �ؽ�Ʈ
    [SerializeField] private RectTransform _currentPosArrow;     // ���� �� ��ġ ȭ��ǥ
    [SerializeField] private List<RectTransform> _stagePosList;  // ���������� ��ġ
    [SerializeField] private GameObject _infiniteAnimIcon;       // ���ѹݺ� �ִϸ��̼� ������
    [SerializeField] private Button _challangeButton;            // ���� ��ư

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        StageManager.OnStageChanged += UpdateStageInfoUI;   // �������� ����� �� -> �������� ���� UI ������Ʈ

        _challangeButton.onClick.AddListener(StageManager.Instance.ChallangeRestartGame);
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        StageManager.OnStageChanged -= UpdateStageInfoUI;

        _challangeButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Init();
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    private void Init()
    {
        // Todo �ӽõ�����
        OnStageChangedArgs args = new OnStageChangedArgs() { CurrentChapter = 1, CurrentStage = 1 };
        UpdateStageInfoUI(args);
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    private void UpdateStageInfoUI(OnStageChangedArgs args)
    {
        int currentChapter = args.CurrentChapter;
        int currentStage = args.CurrentStage;

        _currentPosArrow.localPosition = _stagePosList[currentStage - 1].localPosition + new Vector3(0, -30, 0);
        _currentText.text = $"{currentChapter}-{currentStage}";

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
