using System;

[System.Serializable]
public class EventStore
{
    EventInstance[] events;

    public EventStore()
    {
        events = new EventInstance[0];
    }

    public void AddEvent(EventInstance _event) {
        Array.Resize(ref events, events.Length + 1);
        events[events.Length - 1] = _event;
    }
}

[System.Serializable]
public class EventInstance
{
    public float timestamp;
    public string eventType;
    public string eventBody; // JSON

    public EventInstance(float _timestamp, string _eventType, string _eventBody)
    {
        timestamp = _timestamp;
        eventType = _eventType;
        eventBody = _eventBody;
    }
}

public static class EventType
{
    public static string FinanceCreditIntoBalance = "Finance.CreditIntoBalance";
    public static string FinanceDebitIntoBalance = "Finance.DebitIntoBalance";
}
