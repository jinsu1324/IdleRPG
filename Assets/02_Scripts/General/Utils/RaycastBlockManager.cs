using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 필요 시 UI 레이캐스트 막기 / 켜기 관리
/// </summary>
public class RaycastBlockManager : MonoBehaviour
{
    [Title("터치 불가능하게 막을 부모 CanvasGroup", bold: false)]
    [SerializeField] private CanvasGroup _disableParent;        // 터치 불가능하게 할 부모 CanvasGroup

    [Title("터치 가능하게 할 CanvasGroup들", bold: false)]
    [SerializeField] private CanvasGroup[] _enableTargetArr;    // 터치 가능하게 할 타겟 CanvasGroup 배열

    [Title("화면 전체 막을 딤드 이미지", bold: false)]
    [SerializeField] private GameObject _allScreenDimdImage;    // 화면 전체 막을 딤드 이미지

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipSkillManager.OnSkillSwapStarted += SetRaycastEnable_TargetsOnly; // 장착스킬교체 시작할 때, 타겟만 레이캐스트 가능하게
        EquipSkillManager.OnSkillSwapFinished += ResetRaycast; // 장착스킬교체 끝났을 때, 레이캐스트 리셋
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        EquipSkillManager.OnSkillSwapStarted -= SetRaycastEnable_TargetsOnly;
        EquipSkillManager.OnSkillSwapFinished -= ResetRaycast;
    }

    /// <summary>
    /// 타겟만 레이캐스트 가능하게 셋팅
    /// </summary>
    private void SetRaycastEnable_TargetsOnly()
    {
        ParentRaycastOFF(); // 부모 끄기
        TargetsRaycastON(); // 타겟들만 켜기
        DimdON(); // 딤드 켜기
    }

    /// <summary>
    /// 다시 레이캐스트 다 가능하도록 리셋
    /// </summary>
    private void ResetRaycast()
    {
        ParentRacastON();   // 부모 켜기
        TargetsRaycastON(); // 타겟들 켜기
        DimdOFF(); // 딤드 끄기
    }

    /// <summary>
    /// 타겟들 레이캐스트 / 인터랙션 켜기
    /// </summary>
    private void TargetsRaycastON()
    {
        foreach (CanvasGroup target in _enableTargetArr)
        {
            target.ignoreParentGroups = true; // 부모 설정 무시
            target.blocksRaycasts = true; // 터치 가능
            target.interactable = true; // 상호작용 가능하도록 설정 (버튼 같은것들)
        }
    }

    /// <summary>
    /// 부모 레이캐스트 / 인터랙션 끄기
    /// </summary>
    private void ParentRaycastOFF()
    {
        _disableParent.blocksRaycasts = false; // 해당 CanvasGroup 아래 모든 UI 요소 터치 불가
        _disableParent.interactable = false; // 상호작용 막기
    }

    /// <summary>
    /// 부모 레이캐스트 / 인터랙션 켜기
    /// </summary>
    private void ParentRacastON()
    {
        _disableParent.blocksRaycasts = true; // 해당 CanvasGroup 아래 모든 UI 요소 터치 가능
        _disableParent.interactable = true; // 상호작용 가능
    }

    /// <summary>
    /// 화면 전체 막을 딤드이미지 켜기
    /// </summary>
    private void DimdON()
    {
        _allScreenDimdImage.SetActive(true);
    }

    /// <summary>
    /// 화면 전체 막을 딤드이미지 끄기
    /// </summary>
    private void DimdOFF()
    {
        _allScreenDimdImage.SetActive(false);
    }
}
