using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    float CoolTime { get; }     // ��ų ��Ÿ��
    float CurrentTime { get; }  // ��Ÿ�� üũ�� �ð�

    /// <summary>
    /// ��Ÿ�� üũ
    /// </summary>
    bool CheckCoolTime();

    /// <summary>
    /// ��ų ����
    /// </summary>
    void ExecuteSkill();

    /// <summary>
    /// ���� ��Ÿ�� �����Ȳ ��������
    /// </summary>
    float GetCurrentCoolTimeProgress();
}
