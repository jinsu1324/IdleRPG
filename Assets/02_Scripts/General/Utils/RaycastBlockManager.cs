using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ʿ� �� UI ����ĳ��Ʈ ���� / �ѱ� ����
/// </summary>
public class RaycastBlockManager : MonoBehaviour
{
    [Title("��ġ �Ұ����ϰ� ���� �θ� CanvasGroup", bold: false)]
    [SerializeField] private CanvasGroup _disableParent;        // ��ġ �Ұ����ϰ� �� �θ� CanvasGroup

    [Title("��ġ �����ϰ� �� CanvasGroup��", bold: false)]
    [SerializeField] private CanvasGroup[] _enableTargetArr;    // ��ġ �����ϰ� �� Ÿ�� CanvasGroup �迭

    [Title("ȭ�� ��ü ���� ���� �̹���", bold: false)]
    [SerializeField] private GameObject _allScreenDimdImage;    // ȭ�� ��ü ���� ���� �̹���

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        EquipSkillManager.OnSkillSwapStarted += SetRaycastEnable_TargetsOnly; // ������ų��ü ������ ��, Ÿ�ٸ� ����ĳ��Ʈ �����ϰ�
        EquipSkillManager.OnSkillSwapFinished += ResetRaycast; // ������ų��ü ������ ��, ����ĳ��Ʈ ����
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
    /// Ÿ�ٸ� ����ĳ��Ʈ �����ϰ� ����
    /// </summary>
    private void SetRaycastEnable_TargetsOnly()
    {
        ParentRaycastOFF(); // �θ� ����
        TargetsRaycastON(); // Ÿ�ٵ鸸 �ѱ�
        DimdON(); // ���� �ѱ�
    }

    /// <summary>
    /// �ٽ� ����ĳ��Ʈ �� �����ϵ��� ����
    /// </summary>
    private void ResetRaycast()
    {
        ParentRacastON();   // �θ� �ѱ�
        TargetsRaycastON(); // Ÿ�ٵ� �ѱ�
        DimdOFF(); // ���� ����
    }

    /// <summary>
    /// Ÿ�ٵ� ����ĳ��Ʈ / ���ͷ��� �ѱ�
    /// </summary>
    private void TargetsRaycastON()
    {
        foreach (CanvasGroup target in _enableTargetArr)
        {
            target.ignoreParentGroups = true; // �θ� ���� ����
            target.blocksRaycasts = true; // ��ġ ����
            target.interactable = true; // ��ȣ�ۿ� �����ϵ��� ���� (��ư �����͵�)
        }
    }

    /// <summary>
    /// �θ� ����ĳ��Ʈ / ���ͷ��� ����
    /// </summary>
    private void ParentRaycastOFF()
    {
        _disableParent.blocksRaycasts = false; // �ش� CanvasGroup �Ʒ� ��� UI ��� ��ġ �Ұ�
        _disableParent.interactable = false; // ��ȣ�ۿ� ����
    }

    /// <summary>
    /// �θ� ����ĳ��Ʈ / ���ͷ��� �ѱ�
    /// </summary>
    private void ParentRacastON()
    {
        _disableParent.blocksRaycasts = true; // �ش� CanvasGroup �Ʒ� ��� UI ��� ��ġ ����
        _disableParent.interactable = true; // ��ȣ�ۿ� ����
    }

    /// <summary>
    /// ȭ�� ��ü ���� �����̹��� �ѱ�
    /// </summary>
    private void DimdON()
    {
        _allScreenDimdImage.SetActive(true);
    }

    /// <summary>
    /// ȭ�� ��ü ���� �����̹��� ����
    /// </summary>
    private void DimdOFF()
    {
        _allScreenDimdImage.SetActive(false);
    }
}
