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
    public static string FinanceCreditIntoWallet = "Finance.CreditIntoWallet";
    public static string FinanceDebitIntoWallet = "Finance.DebitIntoWallet";
    public static string FinanceCreditIntoBonds = "Finance.CreditIntoBonds";
    public static string FinanceDebitIntoBonds = "Finance.DebitIntoBonds";
    public static string FinanceCreditIntoStocks = "Finance.CreditIntoStocks";
    public static string FinanceDebitIntoStocks = "Finance.DebitIntoStocks";
}
