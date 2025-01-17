using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkOnHit : MonoBehaviour
{
    [Title("������ �޾��� �� ������ ��������Ʈ�� �������ּ���.", Bold = false)]
    [SerializeField] private List<SpriteRenderer> _spriteList; // ������ ��������Ʈ ������ ����Ʈ

    private void OnEnable()
    {
        HPComponent hpComponent = GetComponent<HPComponent>();
        if (hpComponent != null)
            hpComponent.OnTakeDamage += SpriteBlink;   // ������ �޾��� ��, ��������Ʈ �����̱�
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        HPComponent hpComponent = GetComponent<HPComponent>();
        if (hpComponent != null)
            hpComponent.OnTakeDamage -= SpriteBlink;
    }

    /// <summary>
    /// ��������Ʈ �����̱�
    /// </summary>
    private void SpriteBlink(TakeDamageArgs args)
    {
        foreach (SpriteRenderer sprite in _spriteList)
            sprite.DOColor(Color.red, 0.1f).OnComplete(() => sprite.DOColor(Color.white, 0.1f));
    }
}
