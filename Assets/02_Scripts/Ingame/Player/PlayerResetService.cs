using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾� ���� ���� �� ó��
/// </summary>
public class PlayerResetService
{
    public static event Action OnReset; // �÷��̾� ������Ʈ�� �� ���� �̺�Ʈ

    /// <summary>
    /// �÷��̾� ������Ʈ�� �� ���� (�̺�Ʈ �˸�)
    /// </summary>
    public static void PlayerReset()
    {
        OnReset?.Invoke();
    }
}
