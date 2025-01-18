using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISavable
{
    string Key { get; } // 데이터의 고유 키
    
    void NotifyLoaded(); // 데이터를 로드한 후 호출
}
