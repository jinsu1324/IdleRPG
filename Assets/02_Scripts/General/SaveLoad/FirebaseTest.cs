using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

[System.Serializable]
public class UserData
{
    public string UserName;
    public int TotalCoin;
    public int CurrentLevel;
    public int HighScore;
}

public class FirebaseTest : MonoBehaviour
{
    [SerializeField] private string _userId;
    [SerializeField] private UserData _userData;

    private DatabaseReference _databaseReference;

    private void Awake()
    {
        _databaseReference = FirebaseDatabase.DefaultInstance.RootReference;


        FirebaseDatabase.DefaultInstance.GetReference(".info/connected")
            .ValueChanged += (sender, args) =>
            {
                if (args.DatabaseError != null)
                {
                    Debug.LogError("Failed to check Firebase connection.");
                    return;
                }

                bool isConnected = args.Snapshot.Value is bool connected && connected;
                Debug.Log($"Firebase connection status: {(isConnected ? "Connected" : "Disconnected")}");
            };
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(_userData);
        _databaseReference.Child("users").Child(_userId).SetRawJsonValueAsync(json);
    }

    public void Load()
    {
        StartCoroutine(LoadData());
    }

    private IEnumerator LoadData()
    {
        var serverData = _databaseReference.Child("users").Child(_userId).GetValueAsync();
        yield return new WaitUntil(() => serverData.IsCompleted);

        Debug.Log("Process is Complete");

        DataSnapshot snapshot = serverData.Result;
        string jsonData = snapshot.GetRawJsonValue();

        if (jsonData != null)
        {
            Debug.Log("server data found");
            _userData = JsonUtility.FromJson<UserData>(jsonData);
        }
        else
        {
            Debug.Log("no data found");
        }
    }
}
