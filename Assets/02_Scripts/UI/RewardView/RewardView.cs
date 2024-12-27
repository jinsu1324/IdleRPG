using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardView : MonoBehaviour
{
    [SerializeField] private List<RewardItemSlot> _rewardItemSlotList;  // ȹ�� ������ ���� ����Ʈ
    [SerializeField] private Button _exitButton;                        // ������ ��ư
    [SerializeField] private GameObject _exitGuideTextGO;               // ���Ͽ� ����ϱ� �ؽ�Ʈ GO

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        _exitButton.onClick.AddListener(Hide);  // �������ư(����) ������ ������
    }

    /// <summary>
    /// �ѱ�
    /// </summary>
    public void Show(List<Item> dropItemList)
    {
        Change_ExitGuideTextGO(false);
        Change_ExitButtonInteractable(false);
        gameObject.SetActive(true);
        StartCoroutine(Init_RewardItemSlotList(dropItemList));
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
            if (i < dropItemList.Count)  // ��� ī��Ʈ��ŭ�� rewardItemSlot �ʱ�ȭ
            {
                Item dropItem = dropItemList[i];
                _rewardItemSlotList[i].Init(dropItem);
                
                yield return new WaitForSecondsRealtime(0.1f);
            }
            else
            {
                _rewardItemSlotList[i].Hide();
            }
        }

        Change_ExitGuideTextGO(true);
        Change_ExitButtonInteractable(true);
    }

    /// <summary>
    /// �������ư(����) Ȱ��ȭ / ��Ȱ��ȭ
    /// </summary>
    private void Change_ExitButtonInteractable(bool flag) => _exitButton.interactable = flag;

    /// <summary>
    /// ���Ͽ� ����ϱ� �ؽ�Ʈ GO Ȱ��ȭ / ��Ȱ��ȭ
    /// </summary>
    private void Change_ExitGuideTextGO(bool flag) => _exitGuideTextGO.gameObject.SetActive(flag);

    /// <summary>
    /// ����
    /// </summary>
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
