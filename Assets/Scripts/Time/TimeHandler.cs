using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
    public int currentTimestamp = 0; // 1 = 1 hour in the game = 1 minute in real time
    public Text timestampDisplayText;
    private float timerCounter = 0;
    private static int timeToUpdateTimerInSeconds = 1;

    // Start is called before the first frame update
    void Start()
    {
        LoadCurrentTimestamp();
    }

    private void Update()
    {
        UpdateUICurrentTimestamp();
        UpdateTimer();
    }

    public event Action onNewMonth;

    // Update is called once per frame
    public void SleepUntilNextHour(int hour)
    {
        // TODO: smooth fast forward
        int timeInMinutes = hour * TimeStructure.OneHour;
        var t = GetTimestampAsTimeStructure();
        int minutesToForward = t.hours < hour
            ? timeInMinutes - (t.hours * TimeStructure.OneHour + t.minutes)
            : TimeStructure.OneDay - ((t.hours * TimeStructure.OneHour + t.minutes) - timeInMinutes);

        UpdateCurrentTimestamp(t.timestamp + minutesToForward);
    }

    private void LoadCurrentTimestamp()
    {
        if (PlayerPrefs.HasKey(FinancePrefKeys.CurrentTimestamp))
        {
            currentTimestamp = PlayerPrefs.GetInt(FinancePrefKeys.CurrentTimestamp);
        }
        else
        {
            UpdateCurrentTimestamp(FinanceConsts.InitialTimestamp);
        }
    }

    public void UpdateCurrentTimestamp(int timestamp)
    {
        currentTimestamp = timestamp;
        PlayerPrefs.SetInt(FinancePrefKeys.CurrentTimestamp, currentTimestamp);

        var dayNightCycle = GetComponent<DayNightCycle>();
        if (dayNightCycle != null)
        {
            dayNightCycle.UpdateCurrentTime(timestamp);
        }
    }

    public TimeStructure GetTimestampAsTimeStructure()
    {
        return new TimeStructure(currentTimestamp);
    }

    private void UpdateTimer()
    {
        timerCounter += Time.deltaTime;
        if (timerCounter >= timeToUpdateTimerInSeconds)
        {
            timerCounter = timerCounter % timeToUpdateTimerInSeconds;
            UpdateCurrentTimestamp(currentTimestamp + timeToUpdateTimerInSeconds);
        }
    }

    private void UpdateUICurrentTimestamp()
    {
        if (timestampDisplayText != null)
        {
            timestampDisplayText.text = FormattingHelpers.FormatHumanTime(currentTimestamp);
        }
    }

}
