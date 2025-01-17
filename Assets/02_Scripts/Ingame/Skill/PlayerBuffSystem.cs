using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffSystem : SingletonBase<PlayerBuffSystem>
{
    private Coroutine _angerSkillCoroutine; // �г뽺ų �ڷ�ƾ

    /// <summary>
    /// �÷��̾�� ���� ���� ����
    /// </summary>
    public void StartBuffToPlayer(float buffDuration, PlayerStatUpdateArgs args)
    {
        // �̹� �ڷ�ƾ �ִٸ� �����ߴ�
        if (_angerSkillCoroutine != null)
            StopCoroutine(_angerSkillCoroutine);

        // ���ο� �ڷ�ƾ����
        _angerSkillCoroutine = StartCoroutine(Buff(buffDuration, args));
    }

    /// <summary>
    /// ����
    /// </summary>
    private IEnumerator Buff(float buffDuration, PlayerStatUpdateArgs args)
    {
        PlayerStats.UpdateStatModifier(args);   // ���ȿ� ����

        yield return new WaitForSeconds(buffDuration);  // �����ð���ŭ ��� ��
            
        PlayerStats.RemoveStatModifier(args); // ���ȿ��� ����
    }
}
