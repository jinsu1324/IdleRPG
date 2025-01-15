using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    float CurrentTime { get; }      // ������ üũ�� �ð�

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
