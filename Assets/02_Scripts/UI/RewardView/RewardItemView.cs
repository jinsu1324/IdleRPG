using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ������ ȹ�� ��
/// </summary>
public class RewardItemView : MonoBehaviour
{
    [SerializeField] private List<RewardItemSlot> _rewardItemSlotList;  // ȹ�� ������ ���� ����Ʈ
    [SerializeField] private Button _exitButton;                        // ������ ��ư
    [SerializeField] private GameObject _exitGuideTextGO;               // ���Ͽ� ����ϱ� �ؽ�Ʈ GO

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        _exitButton.onClick.AddListener(Hide);  // �������ư(����) ������ ������
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        _exitButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// �ѱ�
    /// </summary>
    public void Show(List<Item> dropItemList)
    {
        gameObject.SetActive(true);
        OFF_ExitButtonAndGuide();
        StartCoroutine(Init_RewardItemSlotList(dropItemList));
    }

    /// <summary>
    /// ����
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// ȹ�� ������ ���� ����Ʈ �ʱ�ȭ
    /// </summary>
    private IEnumerator Init_RewardItemSlotList(List<Item> dropItemList)
    {
        // �ϴ� �� ����
        foreach (RewardItemSlot rewardItemSlot in _rewardItemSlotList)
            rewardItemSlot.Hide();

        for (int i = 0; i < _rewardItemSlotList.Count; i++)
        {
            if (i < dropItemList.Count)  
            {
                _rewardItemSlotList[i].Init(dropItemList[i]); // ��� �����۸���Ʈ ������ŭ�� �����ְ�, �������� ������
                yield return new WaitForSecondsRealtime(0.1f);
            }
            else
                _rewardItemSlotList[i].Hide();
        }

        ON_ExitButtonAndGuide();
    }

    /// <summary>
    /// �������ư�� ���̵� ����
    /// </summary>
    private void OFF_ExitButtonAndGuide()
    {
        _exitButton.interactable = false;
        _exitGuideTextGO.gameObject.SetActive(false);
    }

    /// <summary>
    /// �������ư�� ���̵� �ѱ�
    /// </summary>
    private void ON_ExitButtonAndGuide()
    {
        _exitButton.interactable = true;
        _exitGuideTextGO.gameObject.SetActive(true);
    }
}
