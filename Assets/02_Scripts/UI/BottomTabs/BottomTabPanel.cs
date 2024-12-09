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


    // 버튼 클릭하면,
        // 팝업 켜지고
        // x버튼 켜지기
    // 다른버튼 클릭하면
        // 원래팝업 닫히고
        // x버튼도 꺼지고
        // 다른버튼의 팝업 켜지고
        // 다른버튼의 x버튼 켜지기


    private void OnEnable()
    {
        BottomTab.OnButtonClicked += ShowPopup;
    }

    






    public void ShowPopup(BottomTabType bottomTabType, BottomTab bottomTab)
    {
        if (_tabPopupDict.TryGetValue(bottomTabType, out GameObject popup))
        {
            // 전체 창 끄기
            foreach (var kvp in _tabPopupDict)
                kvp.Value.SetActive(false);

            // x아이콘도 다 끄기
            foreach (BottomTab tab in _bottomBatList)
                tab.CloseGO_OFF();




            // 이미열려있는 창이면 열지말고 그냥 나가기
            if (popup == _currentPopup)
            {
                Debug.Log("이미 열려있는 창임");
                _currentPopup = null;
                bottomTab.CloseGO_OFF();
                return;
            }


            // 창 열기
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
