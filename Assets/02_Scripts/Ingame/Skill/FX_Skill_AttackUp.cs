using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݷ¾� ��ų ����Ʈ
/// </summary>
public class FX_Skill_AttackUp : ObjectPoolObj
{
    // <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init(float duration)
    {
        StartCoroutine(StartFX_AfterBackPool(duration));
    }

    /// <summary>
    /// ���� ���ӽð���ŭ ��ٸ� �Ŀ� �ٽ� Ǯ�� ������
    /// </summary>
    private IEnumerator StartFX_AfterBackPool(float duration)
    {
        yield return new WaitForSeconds(duration);

        BackTrans();
    }
}
