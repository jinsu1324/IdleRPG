using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestData QuestData { get; set; }  // 퀘스트 데이터
    public int CurrentValue { get; set; }     // 현재 진행 값
    public bool IsCompleted { get; set; }     // 완료 여부

    public Quest(QuestData questData)
    {
        QuestData = questData;
        CurrentValue = 0;
        IsCompleted = false;
    }
}
