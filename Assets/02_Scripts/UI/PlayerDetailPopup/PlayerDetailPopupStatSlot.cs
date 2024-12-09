using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetailPopupStatSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statNameText;         // 스탯 이름 텍스트 
    [SerializeField] private TextMeshProUGUI _valueText;            // 실제 값 텍스트
    [SerializeField] private Image _statIcon;                       // 스탯 아이콘

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init(StatID id)
    {
        UpdateSlotUI(id);
    }

    /// <summary>
    /// 슬롯 UI 업데이트
    /// </summary>
    private void UpdateSlotUI(StatID id)
    {
        // 이 슬롯의 스탯ID에 맞게 스탯 가져오기
        Stat stat = PlayerStatContainer.Instance.GetStat(id);

        // UI 요소들 업데이트
        if (stat != null)
        {
            _statNameText.text = stat.Name;
            _valueText.text = AlphabetNumConverter.Convert(stat.Value);
            _statIcon.sprite = ResourceManager.Instance.GetIcon(stat.StatID.ToString());
        }
    }
}
