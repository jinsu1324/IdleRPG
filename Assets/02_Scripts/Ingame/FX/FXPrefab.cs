using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ʈ �����տ� �ٿ��� �ݹ��Լ� �޾��� Ŭ����
/// </summary>
public class FXPrefab : ObjectPoolObj
{
    /// <summary>
    /// ��ƼŬ ������ �� �ݹ��Լ� (Particle Stop Action - Callback)
    /// </summary>
    private void OnParticleSystemStopped()
    {
        BackTrans(); // Ǯ�� ����������
    }
}
