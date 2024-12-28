using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomTabPanel : SerializedMonoBehaviour
{
    [SerializeField] private List<BottomTab> _bottomTabList;    // �ϴ��� ����Ʈ
    private BottomTab _currentOpenTab;                          // ���� �����ִ� �ϴ���

    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        BottomTab.OnOpenButtonClicked += Update_CurrentOpenTab; // �����ư ������ ��, ���翭�� �� ������Ʈ
        BottomTab.OnCloseButtonClicked += Clear_CurrentOpenTab; // �ݱ��ư ������ ��, ���翭�� �� ����

        ItemInven.OnItemInvenChanged += Update_BottomTabReddots; // ������ �ִ� �������� ����Ǿ��� ��, �ϴ��ǵ� ����� ������Ʈ
        SelectItemInfoUI.OnItemStatueChanged += Update_BottomTabReddots; // ������ ���°� �ٲ���� ��, �ϴ��ǵ� ����� ������Ʈ
    }

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Update_BottomTabReddots();
    }

    /// <summary>
    /// ���翭���� ������Ʈ
    /// </summary>
    public void Update_CurrentOpenTab(BottomTab bottomTab)
    {
        // 1. ���� ���� ���� �ϳ��� ������ ���������� 
        if (_currentOpenTab == null)
            _currentOpenTab = bottomTab;

        // 2. ���� ���� ������ �׳� �״��
        if (_currentOpenTab == bottomTab)
            return;

        // 3. �ٸ� ���� ������
        if (_currentOpenTab != bottomTab)
        {
            _currentOpenTab.CloseTabPopup();  // ���� �� ����
            _currentOpenTab = bottomTab;      // ���ο� ���� �������� ��ü
        }
    }

    /// <summary>
    /// ���翭���� ����
    /// </summary>
    public void Clear_CurrentOpenTab()
    {
        _currentOpenTab = null;   // ���� �������� �ƹ��͵� ����
    }

    /// <summary>
    /// �ϴ��ǵ� ����� ������Ʈ
    /// </summary>
    private void Update_BottomTabReddots()
    {
        // �ϴ��� ����� ���ǵ� (���⼭ ����)
        Dictionary<BottomTabType, Func<bool>> redDotConditionDict = new Dictionary<BottomTabType, Func<bool>>
        {
            { BottomTabType.PlayerDetail, () => ItemInven.HasEnhanceableItemAllInven()},
            { BottomTabType.Shop, () => false} // shop�� �ӽ�
        };

        // �ϴ��ǵ� ����忡 ���� ����
        foreach (BottomTab bottomTab in _bottomTabList)
        {
            if (redDotConditionDict.TryGetValue(bottomTab.GetBottomTabType(), out Func<bool> condition))
                bottomTab.SetReddotCondition(condition);
            else
                Debug.Log("����� ������ ã�� �� �����ϴ�");
        }

    }
    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        BottomTab.OnOpenButtonClicked -= Update_CurrentOpenTab;
        BottomTab.OnCloseButtonClicked -= Clear_CurrentOpenTab;

        ItemInven.OnItemInvenChanged -= Update_BottomTabReddots;
        SelectItemInfoUI.OnItemStatueChanged -= Update_BottomTabReddots;
    }
}
