using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISavable
{
    string Key { get; } // �������� ���� Ű
    
    void NotifyLoaded(); // �����͸� �ε��� �� ȣ��
}
