using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private DataStore dataStore;
    private TimeHandler timeHandler;

    private void Start()
    {
        dataStore = GetComponent<DataStore>();
        timeHandler = GetComponent<TimeHandler>();
    }

    public void ResetPlayerPrefs()
    {
        // TODO: show confirmation dialog
        timeHandler.timerIsActive = false;
        PlayerPrefs.DeleteAll();
        dataStore.DeleteEventFile();
        dataStore.DeleteStaticFile();
        Application.Quit();
    }
}
