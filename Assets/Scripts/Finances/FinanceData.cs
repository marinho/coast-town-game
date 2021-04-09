using System.Collections.Generic;
using UnityEngine;

public class FinanceData : MonoBehaviour
{
    private List<StocksFluctuation> stocksFluctuationEvents;

    private void Start()
    {
        LoadStocksFluctuationHistory();
    }

    public void CreditIntoWallet(float value, string description)
    {
        var dataStore = GetComponent<DataStore>();
        var timeHandler = GetComponent<TimeHandler>();
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(timeHandler.currentTimestamp, EventType.FinanceCreditIntoWallet, body));
        UpdateWalletBalance(value);
    }

    public void DebitIntoWallet(float value, string description)
    {
        var dataStore = GetComponent<DataStore>();
        var timeHandler = GetComponent<TimeHandler>();
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(timeHandler.currentTimestamp, EventType.FinanceDebitIntoWallet, "{\"value\": 0, \"description\": \"Initial credit.\"}"));
        UpdateWalletBalance(value * -1);
    }

    private void UpdateWalletBalance(float differenceValue)
    {
        var walletBalance = PlayerPrefs.GetFloat(FinancePrefKeys.WalletBalance, 0);
        PlayerPrefs.SetFloat(FinancePrefKeys.WalletBalance, walletBalance + differenceValue);
    }

    public void CreditIntoBonds(float value, string description)
    {
        var dataStore = GetComponent<DataStore>();
        var timeHandler = GetComponent<TimeHandler>();
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(timeHandler.currentTimestamp, EventType.FinanceCreditIntoBonds, body));
        UpdateBondsBalance(value);
    }

    public void DebitIntoBonds(float value, string description)
    {
        var dataStore = GetComponent<DataStore>();
        var timeHandler = GetComponent<TimeHandler>();
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(timeHandler.currentTimestamp, EventType.FinanceDebitIntoBonds, "{\"value\": 0, \"description\": \"Initial credit.\"}"));
        UpdateBondsBalance(value * -1);
    }

    private void UpdateBondsBalance(float differenceValue)
    {
        var bondsBalance = PlayerPrefs.GetFloat(FinancePrefKeys.BondsBalance, 0);
        PlayerPrefs.SetFloat(FinancePrefKeys.BondsBalance, bondsBalance + differenceValue);
    }

    public List<float> GetRatesFromEvents()
    {
        var result = new List<float>();
        foreach (StocksFluctuation eventInstance in stocksFluctuationEvents)
        {
            result.Add(eventInstance.rate);
        }
        return result;
    }

    private void LoadStocksFluctuationHistory()
    {
        var dataStore = GetComponent<DataStore>();
        var allEvents = dataStore.eventStore.GetEventsAsList();
        var fluctuationEvents = allEvents.FindAll(FindStockFluctuation);

        var fluctuations = new List<StocksFluctuation>();
        foreach (EventInstance eventInstance in fluctuationEvents)
        {
            var fluctuation = JsonUtility.FromJson<StocksFluctuation>(eventInstance.eventBody);
            fluctuations.Add(fluctuation);
        }
        stocksFluctuationEvents = fluctuations;

        //string json = JsonUtility.ToJson(myObject);
    }

    private static bool FindStockFluctuation(EventInstance eventInstance)
    {
        if (eventInstance.eventType == EventType.FinanceFluctuateStocks)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float FluctuateStocks(float rate, float previousStocksBalance)
    {
        var dataStore = GetComponent<DataStore>();
        var timeHandler = GetComponent<TimeHandler>();

        string description = FinanceConsts.StocksDailyDescription;
        float value = previousStocksBalance * rate;

        string body = JsonBrace(string.Format("\"value\": {0}, \"rate\": {1}, \"description\": \"{2}\"", value, rate, description));
        dataStore.SaveEvent(new EventInstance(timeHandler.currentTimestamp, EventType.FinanceFluctuateStocks, body));

        UpdateStocksBalance(value);
        LoadStocksFluctuationHistory();

        return value;
    }

    public void CreditIntoStocks(float value, string description)
    {
        var dataStore = GetComponent<DataStore>();
        var timeHandler = GetComponent<TimeHandler>();
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(timeHandler.currentTimestamp, EventType.FinanceCreditIntoStocks, body));
        UpdateStocksBalance(value);
    }

    public void DebitIntoStocks(float value, string description)
    {
        var dataStore = GetComponent<DataStore>();
        var timeHandler = GetComponent<TimeHandler>();
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(timeHandler.currentTimestamp, EventType.FinanceDebitIntoStocks, "{\"value\": 0, \"description\": \"Initial credit.\"}"));
        UpdateStocksBalance(value * -1);
    }

    private void UpdateStocksBalance(float differenceValue)
    {
        var stocksBalance = PlayerPrefs.GetFloat(FinancePrefKeys.StocksBalance, 0);
        PlayerPrefs.SetFloat(FinancePrefKeys.StocksBalance, stocksBalance + differenceValue);
    }

    private string JsonBrace(string value)
    {
        return "{" + value + "}";
    }
}
