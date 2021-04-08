using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinanceData : MonoBehaviour
{
    public void CreditIntoWallet(int value, string description)
    {
        var dataStore = GetComponent<DataStore>();
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(0, EventType.FinanceCreditIntoWallet, body));
        UpdateWalletBalance(value);
    }

    public void DebitIntoWallet(int value, string description)
    {
        var dataStore = GetComponent<DataStore>();
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(0, EventType.FinanceDebitIntoWallet, "{\"value\": 0, \"description\": \"Initial credit.\"}"));
        UpdateWalletBalance(value * -1);
    }

    private void UpdateWalletBalance(int differenceValue)
    {
        var walletBalance = PlayerPrefs.GetInt(FinancePrefKeys.WalletBalance, 0);
        PlayerPrefs.SetInt(FinancePrefKeys.WalletBalance, walletBalance + differenceValue);
    }

    public void CreditIntoBonds(int value, string description)
    {
        var dataStore = GetComponent<DataStore>();
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(0, EventType.FinanceCreditIntoBonds, body));
        UpdateBondsBalance(value);
    }

    public void DebitIntoBonds(int value, string description)
    {
        var dataStore = GetComponent<DataStore>();
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(0, EventType.FinanceDebitIntoBonds, "{\"value\": 0, \"description\": \"Initial credit.\"}"));
        UpdateBondsBalance(value * -1);
    }

    private void UpdateBondsBalance(int differenceValue)
    {
        var bondsBalance = PlayerPrefs.GetInt(FinancePrefKeys.BondsBalance, 0);
        PlayerPrefs.SetInt(FinancePrefKeys.BondsBalance, bondsBalance + differenceValue);
    }

    public void CreditIntoStocks(int value, string description)
    {
        var dataStore = GetComponent<DataStore>();
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(0, EventType.FinanceCreditIntoStocks, body));
        UpdateStocksBalance(value);
    }

    public void DebitIntoStocks(int value, string description)
    {
        var dataStore = GetComponent<DataStore>();
        string body = JsonBrace(string.Format("\"value\": {0}, \"description\": \"{1}\"", value, description));
        dataStore.SaveEvent(new EventInstance(0, EventType.FinanceDebitIntoStocks, "{\"value\": 0, \"description\": \"Initial credit.\"}"));
        UpdateStocksBalance(value * -1);
    }

    private void UpdateStocksBalance(int differenceValue)
    {
        var stocksBalance = PlayerPrefs.GetInt(FinancePrefKeys.StocksBalance, 0);
        PlayerPrefs.SetInt(FinancePrefKeys.StocksBalance, stocksBalance + differenceValue);
    }

    private string JsonBrace(string value)
    {
        return "{" + value + "}";
    }
}
