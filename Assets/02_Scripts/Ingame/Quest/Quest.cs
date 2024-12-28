using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestData QuestData { get; set; }  // ����Ʈ ������
    public int CurrentValue { get; set; }     // ���� ���� ��
    public bool IsCompleted { get; set; }     // �Ϸ� ����

    public Quest(QuestData questData)
    {
        QuestData = questData;
        CurrentValue = 0;
        IsCompleted = false;
    }
}
