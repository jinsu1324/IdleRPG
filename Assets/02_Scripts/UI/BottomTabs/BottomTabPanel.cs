using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BottomTabType
{
    PlayerDetail,
    Shop
}


public class BottomTabPanel : SerializedMonoBehaviour
{
    [SerializeField] private Dictionary<BottomTabType, GameObject> _tabPopupDict;
    [SerializeField] private List<BottomTab> _bottomBatList;
    private GameObject _currentPopup;


    // ��ư Ŭ���ϸ�,
        // �˾� ������
        // x��ư ������
    // �ٸ���ư Ŭ���ϸ�
        // �����˾� ������
        // x��ư�� ������
        // �ٸ���ư�� �˾� ������
        // �ٸ���ư�� x��ư ������


    private void OnEnable()
    {
        BottomTab.OnButtonClicked += ShowPopup;
    }

    






    public void ShowPopup(BottomTabType bottomTabType, BottomTab bottomTab)
    {
        if (_tabPopupDict.TryGetValue(bottomTabType, out GameObject popup))
        {
            // ��ü â ����
            foreach (var kvp in _tabPopupDict)
                kvp.Value.SetActive(false);

            // x�����ܵ� �� ����
            foreach (BottomTab tab in _bottomBatList)
                tab.CloseGO_OFF();




            // �̹̿����ִ� â�̸� �������� �׳� ������
            if (popup == _currentPopup)
            {
                Debug.Log("�̹� �����ִ� â��");
                _currentPopup = null;
                bottomTab.CloseGO_OFF();
                return;
            }


            // â ����
            _currentPopup = popup;
            popup.SetActive(true);
            bottomTab.CloseGO_ON();
        }
    }

    private void OnDisable()
    {
        BottomTab.OnButtonClicked -= ShowPopup;

    }



    


}
