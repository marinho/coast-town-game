using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimeHandler : MonoBehaviour
{
    public int currentTimestamp = 0; // 1 = 1 hour in the game = 1 minute in real time
    public int previousTimestamp;
    public Text timestampDisplayText;

    public UnityEvent onMinuteChange;
    public UnityEvent onHourChange;
    public UnityEvent onDayChange;
    public UnityEvent onMonthChange;
    public UnityEvent onYearChange;

    private float timerCounter = 0;
    private static int timeToUpdateTimerInSeconds = 1;
    public static int InitialTimestamp = 0; // before first day and hour passed

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
            UpdateCurrentTimestamp(InitialTimestamp);
        }
    }

    public void UpdateCurrentTimestamp(int timestamp)
    {
        previousTimestamp = currentTimestamp;
        currentTimestamp = timestamp;
        PlayerPrefs.SetInt(FinancePrefKeys.CurrentTimestamp, currentTimestamp);

        var dayNightCycle = GetComponent<DayNightCycle>();
        if (dayNightCycle != null)
        {
            dayNightCycle.UpdateCurrentTime(timestamp);
        }

        InvokeChangeEvents();
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

    private void InvokeChangeEvents()
    {
        var previous = new TimeStructure(previousTimestamp);
        var current = GetTimestampAsTimeStructure();

        bool hourHasChanged = previous.GetHour() != current.GetHour();
        bool dayHasChanged = previous.GetDayOfYear() != current.GetDayOfYear();
        bool monthHasChanged = previous.GetMonth() != current.GetMonth();
        bool yearHasChanged = previous.GetYear() != current.GetYear();

        onMinuteChange.Invoke();

        if (hourHasChanged)
        {
            onHourChange.Invoke();
        }

        if (dayHasChanged)
        {
            onDayChange.Invoke();
        }

        if (monthHasChanged)
        {
            onMonthChange.Invoke();
        }

        if (yearHasChanged)
        {
            onYearChange.Invoke();
        }
    }

}
