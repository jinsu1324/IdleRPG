using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetailPopupStatSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statNameText;         // ���� �̸� �ؽ�Ʈ 
    [SerializeField] private TextMeshProUGUI _valueText;            // ���� �� �ؽ�Ʈ
    [SerializeField] private Image _statIcon;                       // ���� ������

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(StatID id)
    {
        UpdateSlotUI(id);
    }

    /// <summary>
    /// ���� UI ������Ʈ
    /// </summary>
    private void UpdateSlotUI(StatID id)
    {
        // �� ������ ����ID�� �°� ���� ��������
        Stat stat = PlayerStatContainer.Instance.GetStat(id);

        // UI ��ҵ� ������Ʈ
        if (stat != null)
        {
            _statNameText.text = stat.Name;
            _valueText.text = AlphabetNumConverter.Convert(stat.Value);
            _statIcon.sprite = ResourceManager.Instance.GetIcon(stat.StatID.ToString());
        }
    }
}
