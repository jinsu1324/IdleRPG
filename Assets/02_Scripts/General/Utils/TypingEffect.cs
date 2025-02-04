using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingEffect : MonoBehaviour
{
    [Title("타이핑 효과 쓸 텍스트", bold: false)]
    [SerializeField] private TextMeshProUGUI _text;             // 텍스트

    [Title("타이핑 속도 (숫자 낮을수록 빨라짐)", bold: false)]
    [SerializeField] private float _typingSpeed;                // 각 글자 간의 딜레이 설정

    private string _fullText;                                    // 텍스트 전체를 저장할 변수

    /// <summary>
    /// 초기화
    /// </summary>
    public void Init()
    {
        _fullText = _text.text;  // 전체 텍스트 저장
        _text.text = "";        // 처음엔 빈 텍스트로 시작
        StartCoroutine(TypeText());
    }

    /// <summary>
    /// 타이핑 효과 코루틴
    /// </summary>
    private IEnumerator TypeText()
    {
        for (int i = 0; i <= _fullText.Length; i++)
        {
            _text.text = _fullText.Substring(0, i);  // 글자를 한글자씩 text에 넣어줌
            yield return new WaitForSeconds(_typingSpeed); // 타이핑 속도만큼 대기
        }
    }
}
