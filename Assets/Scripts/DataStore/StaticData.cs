using System;

// STILL NOT USED - USING PlayerPrefs INSTEAD

[System.Serializable]
public class StaticStore
{
    public string jsonData;

    public StaticStore()
    {
        jsonData = "{}";
    }

    public void UpdateData(string _jsonData)
    {
        jsonData = _jsonData;
    }
}
