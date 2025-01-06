using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastBlockManager : MonoBehaviour
{
    [Title("터치 불가능하게 막을 CanvasGroup", bold: false)]
    [SerializeField] private CanvasGroup _disableTarget;

    [Title("터치 가능하게 할 CanvasGroup들", bold: false)]
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
        _disableTarget.blocksRaycasts = false; // 해당 CanvasGroup 아래 모든 UI 요소 터치 불가
        _disableTarget.interactable = false; // 상호작용 막기


        foreach (CanvasGroup canvasGroup in _enableTargetArr)
        {
            canvasGroup.ignoreParentGroups = true; // 부모 설정 무시
            canvasGroup.blocksRaycasts = true; // 해당 CanvasGroup 아래 모든 UI 요소 터치 가능
            canvasGroup.interactable = true; // 상호작용 가능하도록 설정
        }
    }

    private void ResetRaycast()
    {
        _disableTarget.blocksRaycasts = true; // 터치 가능
        _disableTarget.interactable = true; // 상호작용 가능


        foreach (CanvasGroup canvasGroup in _enableTargetArr)
        {
            canvasGroup.ignoreParentGroups = true; // 부모 설정 무시
            canvasGroup.blocksRaycasts = true; // 터치 가능
            canvasGroup.interactable = true; // 상호작용 가능하도록 설정

        }
    }

}
