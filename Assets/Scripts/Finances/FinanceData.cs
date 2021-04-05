using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinanceData : MonoBehaviour
{
    private DataStore dataStore;

    void Start()
    {
        dataStore = GetComponent<DataStore>();
    }

    public void CreditIntoBalance(int value, string description)
    {
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(0, EventType.FinanceCreditIntoBalance, body));
        UpdateCurrentBalance(value);
    }

    public void DebitIntoBalance(int value, string description)
    {
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(0, EventType.FinanceDebitIntoBalance, "{\"value\": 0, \"description\": \"Initial credit.\"}"));
        UpdateCurrentBalance(value * -1);
    }

    private void UpdateCurrentBalance(int differenceValue)
    {
        var currentBalance = PlayerPrefs.GetInt(FinancePrefKeys.CurrentBalance, 0);
        PlayerPrefs.SetInt(FinancePrefKeys.CurrentBalance, currentBalance + differenceValue);
    }

    private string JsonBrace(string value)
    {
        return "{" + value + "}";
    }
}
