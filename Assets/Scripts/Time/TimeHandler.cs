using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    private FinanceSettings financeSettings;

    // Start is called before the first frame update
    void Start()
    {
        financeSettings = GetComponent<FinanceSettings>();
    }

    // Update is called once per frame
    public void SleepUntilNextHour(int hour)
    {
        // TODO: smooth fast forward
        int timeInMinutes = hour * TimeStructure.OneHour;
        var t = financeSettings.GetTimestampAsTimeStructure();
        int minutesToForward = t.hours < hour
            ? timeInMinutes - (t.hours * TimeStructure.OneHour + t.minutes)
            : TimeStructure.OneDay - ((t.hours * TimeStructure.OneHour + t.minutes) - timeInMinutes);

        financeSettings.UpdateCurrentTimestamp(t.timestamp + minutesToForward);
    }
}
