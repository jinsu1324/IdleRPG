using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �г뽺ų ����Ʈ
/// </summary>
public class FX_Skill_Anger : ObjectPoolObj
{
    /// <summary>
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
