using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomTab : MonoBehaviour
{
    [SerializeField] private Button _openButton;
    [SerializeField] private GameObject _closeGO;
    [SerializeField] private BottomTabType _bottomTabType;

    public static event Action<BottomTabType, BottomTab> OnButtonClicked;

    private void Start()
    {
        _openButton.onClick.AddListener(ONClickOpenButton);
    }




    public void ONClickOpenButton()
    {
        OnButtonClicked?.Invoke(_bottomTabType, this); // ÆË¾÷ ¿­¸²
    }






    public void CloseGO_ON()
    {
        _closeGO.SetActive(true);
    }

    public void CloseGO_OFF()
    {
        Debug.Log($"{_bottomTabType} go ´ÝÈû");
        _closeGO.SetActive(false);
    }

}
