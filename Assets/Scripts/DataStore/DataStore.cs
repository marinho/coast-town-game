using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataStore : MonoBehaviour
{
    public string eventFilePath = "/events.data";
    public string staticFilePath = "/static.data";
    public StaticStore staticStore;

    // Start is called before the first frame update
    void Start()
    {
        InitializeEventDataFile();

        staticStore = new StaticStore();
        InitiliazeStaticDataFile();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Event Store
    private void InitializeEventDataFile()
    {
        string fullFilePath = GetFullEventFilePath();
        if (File.Exists(fullFilePath))
        {
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(fullFilePath, FileMode.Create);

        var eventStore = new EventStore();

        formatter.Serialize(stream, eventStore);
        stream.Close();
    }

    public void SaveEvent(EventInstance eventInstance)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string fullFilePath = GetFullEventFilePath();

        EventStore eventStore;

        var exists = File.Exists(fullFilePath);
        if (exists)
        {
            var readStream = new FileStream(fullFilePath, FileMode.Open);
            eventStore = (EventStore)formatter.Deserialize(readStream);
            readStream.Close();
        }
        else
        {
            eventStore = new EventStore();
        }

        eventStore.AddEvent(eventInstance);

        FileStream writeStream = new FileStream(fullFilePath, FileMode.Create);
        formatter.Serialize(writeStream, eventStore);
        writeStream.Close();
    }

    private string GetFullEventFilePath()
    {
        return Application.persistentDataPath + eventFilePath;
    }

    // Static Store - STILL NOT USED
    private void InitiliazeStaticDataFile()
    {
        string fullFilePath = GetFullStaticFilePath();
        if (File.Exists(fullFilePath))
        {
            LoadStaticDataFile();
        }
        else
        {
            SaveStaticDataFile();
        }

    }

    private void LoadStaticDataFile()
    {
        string fullFilePath = GetFullStaticFilePath();
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(fullFilePath, FileMode.Open);
        staticStore = (StaticStore)formatter.Deserialize(stream);
        stream.Close();
    }

    private void SaveStaticDataFile()
    {
        string fullFilePath = GetFullStaticFilePath();
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(fullFilePath, FileMode.Create);

        formatter.Serialize(stream, staticStore);
        stream.Close();
    }

    private string GetFullStaticFilePath()
    {
        return Application.persistentDataPath + staticFilePath;
    }

}
