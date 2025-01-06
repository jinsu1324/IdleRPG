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
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        BottomTab.OnOpenButtonClicked -= Update_CurrentOpenTab;
        BottomTab.OnCloseButtonClicked -= Clear_CurrentOpenTab;
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
}
