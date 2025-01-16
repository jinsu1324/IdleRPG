using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

/// <summary>
/// �÷��̾� �ھ�
/// </summary>
public class Player : SerializedMonoBehaviour
{
    /// <summary>
    /// OnEnable
    /// </summary>
    private void OnEnable()
    {
        GetComponent<HPComponent_Player>().OnDeadPlayer += PlayerDeadTask;  // �÷��̾� �׾�����, �÷��̾� ������ �ʿ��� �½�ũ�� ó��
    }

    /// <summary>
    /// OnDisable
    /// </summary>
    private void OnDisable()
    {
        GetComponent<HPComponent_Player>().OnDeadPlayer -= PlayerDeadTask;
    }

    /// <summary>
    /// �ʱ�ȭ
    /// </summary>
    public void Init()
    {
        PlayerStatArgs args = PlayerStats.GetCurrentPlayerStatArgs(); // ���� ���ȵ� ��������
        GetComponent<HPComponent_Player>().Init(args.MaxHp); // HP ������Ʈ �ʱⰪ ����
        GetComponent<AttackComponent_Player>().Init(args.AttackPower, args.AttackSpeed); // ���� ������Ʈ �ʱⰪ ����
    }

    /// <summary>
    /// �÷��̾� �׾��� �� ó���� �͵�
    /// </summary>
    private void PlayerDeadTask()
    {
        StageManager.Instance.DefeatRestartGame(); // �������� �й�� �����
    }
}
