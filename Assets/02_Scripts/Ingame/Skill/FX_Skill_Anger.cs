using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX_Skill_Anger : ObjectPoolObj
{
    public void Init(float duration)
    {
        StartCoroutine(StartFX_AfterBackPool(duration));
    }

    private IEnumerator StartFX_AfterBackPool(float duration)
    {
        yield return new WaitForSeconds(duration);

        BackTrans();
    }
}
