using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    [Title("Ÿ���� ȿ�� �� �ؽ�Ʈ", bold: false)]
    [SerializeField] private TextMeshProUGUI _text;             // �ؽ�Ʈ

    [Title("Ÿ���� �ӵ� (���� �������� ������)", bold: false)]
    [SerializeField] private float _typingSpeed;                // �� ���� ���� ������ ����

    private string _fullText;                                    // �ؽ�Ʈ ��ü�� ������ ����

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init()
    {
        _fullText = _text.text;  // ��ü �ؽ�Ʈ ����
        _text.text = "";        // ó���� �� �ؽ�Ʈ�� ����
        StartCoroutine(TypeText());
    }

    /// <summary>
    /// Ÿ���� ȿ�� �ڷ�ƾ
    /// </summary>
    private IEnumerator TypeText()
    {
        for (int i = 0; i <= _fullText.Length; i++)
        {
            _text.text = _fullText.Substring(0, i);  // ���ڸ� �ѱ��ھ� text�� �־���
            yield return new WaitForSeconds(_typingSpeed); // Ÿ���� �ӵ���ŭ ���
        }
    }
}
