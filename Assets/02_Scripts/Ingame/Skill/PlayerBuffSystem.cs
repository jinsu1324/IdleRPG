using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffSystem : SingletonBase<PlayerBuffSystem>
{
    private Coroutine _angerSkillCoroutine; // �г뽺ų �ڷ�ƾ

    /// <summary>
    /// �÷��̾�� ���� ���� ����
    /// </summary>
    public void StartBuffToPlayer(float buffDuration, Dictionary<StatType, float> buffDict, object source)
    {
        // �̹� �ڷ�ƾ �ִٸ� �����ߴ�
        if (_angerSkillCoroutine != null)
            StopCoroutine(_angerSkillCoroutine);

        // ���ο� �ڷ�ƾ����
        _angerSkillCoroutine = StartCoroutine(Buff(buffDuration, buffDict, source));
    }

    /// <summary>
    /// ����
    /// </summary>
    private IEnumerator Buff(float buffDuration, Dictionary<StatType, float> buffDict, object source)
    {
        PlayerStats.UpdateStatModifier(buffDict, source);   // ���ȿ� ����

        yield return new WaitForSeconds(buffDuration);  // �����ð���ŭ ��� ��
            
        PlayerStats.RemoveStatModifier(buffDict, source); // ���ȿ��� ����
    }
}
