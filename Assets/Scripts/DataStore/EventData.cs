using System;
using System.Collections.Generic;

[System.Serializable]
public class EventStore
{
    public EventInstance[] events; // why not a List<EventInstance> instead?

    public EventStore()
    {
        events = new EventInstance[0];
    }

    public void AddEvent(EventInstance _event) {
        Array.Resize(ref events, events.Length + 1);
        events[events.Length - 1] = _event;
    }

    public List<EventInstance> GetEventsAsList()
    {
        return new List<EventInstance>(events);
    }
}

[System.Serializable]
public class EventInstance
{
    public int timestamp;
    public string eventType;
    public string eventBody; // JSON

    public EventInstance(int _timestamp, string _eventType, string _eventBody)
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
    public static string FinanceFluctuateStocks = "Finance.FluctuateStocks";
}
