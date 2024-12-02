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

    private int _currentChapter;                                 // ���� é��
    private int _currentStage;                                   // ���� ��������
    private PlayerManager _playerManager;                        // �÷��̾� �Ŵ��� ĳ�̿�

    [SerializeField] private RectTransform _currentPosArrow;
    [SerializeField] private List<RectTransform> _stagePosList;

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _playerManager = PlayerManager.Instance;
        
        _playerManager.PlayerData.OnStageChange += UpdateUI;    // �������� ���� �̺�Ʈ ����

        UpdateUI();
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _playerManager.PlayerData.OnStageChange -= UpdateUI;    // �������� ���� �̺�Ʈ ��������
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    private void UpdateUI()
    {
        _currentStage = _playerManager.GetCurrentStage();
        _currentPosArrow.localPosition = _stagePosList[_currentStage - 1].localPosition + new Vector3(0, -30, 0);

        _currentText.text = $"{_playerManager.GetCurrentChapter()}-{_playerManager.GetCurrentStage()}";
    }
}
