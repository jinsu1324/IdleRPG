using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXPrefab : ObjectPoolObject
{
    /// <summary>
    /// ��ƼŬ ������ �� �ݹ��Լ� (Particle Stop Action - Callback)
    /// </summary>
    private void OnParticleSystemStopped()
    {
        BackTrans(); // Ǯ�� ����������
    }
}
