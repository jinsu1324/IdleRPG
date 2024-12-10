using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveLoad
{
    string GetFilePath();
    void SaveData();
    void LoadData();
}
