using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBlockManager : MonoBehaviour
{
    [Title("��ġ �Ұ����ϰ� ���� CanvasGroup", bold: false)]
    [SerializeField] private CanvasGroup _disableTarget;

    [Title("��ġ �����ϰ� �� CanvasGroup��", bold: false)]
    [SerializeField] private CanvasGroup[] _enableTargetArr;

    private void OnEnable()
    {
        EquipSkillManager.OnEquipSwapStarted += DisableRaycast;
        EquipSkillManager.OnEquipSwapFinished += ResetRaycast;
    }

    private void OnDisable()
    {
        EquipSkillManager.OnEquipSwapStarted -= DisableRaycast;
        EquipSkillManager.OnEquipSwapFinished -= ResetRaycast;

    }

    private void DisableRaycast()
    {
        _disableTarget.blocksRaycasts = false; // �ش� CanvasGroup �Ʒ� ��� UI ��� ��ġ �Ұ�
        _disableTarget.interactable = false; // ��ȣ�ۿ� ����


        foreach (CanvasGroup canvasGroup in _enableTargetArr)
        {
            canvasGroup.ignoreParentGroups = true; // �θ� ���� ����
            canvasGroup.blocksRaycasts = true; // �ش� CanvasGroup �Ʒ� ��� UI ��� ��ġ ����
            canvasGroup.interactable = true; // ��ȣ�ۿ� �����ϵ��� ����
        }
    }

    private void ResetRaycast()
    {
        _disableTarget.blocksRaycasts = true; // ��ġ ����
        _disableTarget.interactable = true; // ��ȣ�ۿ� ����


        foreach (CanvasGroup canvasGroup in _enableTargetArr)
        {
            canvasGroup.ignoreParentGroups = true; // �θ� ���� ����
            canvasGroup.blocksRaycasts = true; // ��ġ ����
            canvasGroup.interactable = true; // ��ȣ�ۿ� �����ϵ��� ����

        }
    }

}
