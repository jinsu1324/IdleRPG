using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffSystem : SingletonBase<PlayerBuffSystem>
{
    /// <summary>
    /// �÷��̾�� ���� ���� ����
    /// </summary>
    public void StartBuffToPlayer(float buffDuration, PlayerStatUpdateArgs args)
    {
        StartCoroutine(Buff(buffDuration, args));
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
