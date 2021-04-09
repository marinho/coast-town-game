using System.Collections.Generic;
using UnityEngine;

public static class StocksCalculator
{
    private static int ballancedPeriodInDays = 365; // ideally 365 days

    // ---- NEW
    public static float CalculateRateForFluctuation(List<float> trackRates)
    {

        List<float> recentTrackRates = ListUtils.GetListTail(trackRates, ballancedPeriodInDays);
        float rate = CalculateBalancedRate(recentTrackRates);
        return rate;
    }

    private static float CalculateBalancedRate(List<float> recentTrackRates)
    {
        float minRate;
        float maxRate;
        float rateOverPeriod = GetRateOverPeriod(recentTrackRates);

        if (rateOverPeriod < FinanceConsts.MinStocksRate)
        {
            minRate = (1 + FinanceConsts.MinStocksRate) / (1 + rateOverPeriod) - 1;
            maxRate = Mathf.Max(minRate, FinanceConsts.MaxStockRatePerDay);
        }
        else if (rateOverPeriod > FinanceConsts.MaxStocksRate)
        {
            minRate = (1 + FinanceConsts.MaxStocksRate) / (1 + rateOverPeriod) - 1;
            maxRate = Mathf.Min(minRate, FinanceConsts.MaxStockRatePerDay);
        } else
        {
            minRate = FinanceConsts.MinStockRatePerDay;
            maxRate = FinanceConsts.MaxStockRatePerDay;
        }

        float randomRate = RandomRate(minRate, maxRate);

        return randomRate;
    }

    private static float GetRateOverPeriod(List<float> recentTrackRates)
    {
        float initialValue = 1f;
        float finalValue = initialValue;
        foreach (float rate in recentTrackRates)
        {
            finalValue += finalValue * rate;
        }
        return finalValue - 1;
    }

    // ---- OLD
    //public static float CalculateRateForFluctuation(List<float> allHistoryValues, float latestValue)
    //{
    //    float minRate = FinanceConsts.MinStockRatePerDay;
    //    float maxRate = FinanceConsts.MaxStockRatePerDay;

    //    List<float> lastPeriod = ListUtils.GetListTail(allHistoryValues, ballancedPeriodInDays);
    //    float rate = CalculateBalancedRate(lastPeriod, latestValue, minRate, maxRate);
    //    return rate;
    //}

    //private static float CalculateBalancedRate(List<float> lastPeriod, float value, float minRate, float maxRate)
    //{
    //    float firstValueInPeriod = lastPeriod.Count >= 1 ? lastPeriod[0] : value;

    //    float rate = RandomRate(minRate, maxRate);
    //    float previousValue = value;

    //    float cappedMin = Mathf.Max(
    //        value * (1 + rate),
    //        firstValueInPeriod * (1 + FinanceConsts.MinStocksRate)
    //        );
    //    value = Mathf.Min(
    //        cappedMin,
    //        firstValueInPeriod * (1 + FinanceConsts.MaxStocksRate)
    //        );

    //    rate = value / previousValue;
    //    return rate - 1;
    //}

    private static float RandomRate(float minRate, float maxRate)
    {
        float difference = maxRate - minRate;
        float fluctuation = Random.Range(0f, 1f) * difference + minRate;
        return fluctuation;
    }
}

public class StocksFluctuation
{
    public int timestamp;
    public float value;
    public float rate;

    public StocksFluctuation(int _timestamp, float _value, float _rate)
    {
        timestamp = _timestamp;
        value = _value;
        rate = _rate;
    }
}

public static class ListUtils
{

    public static List<T> GetListTail<T>(List<T> source, int length)
    {
        int listLength = source.Count;
        int count = Mathf.Min(listLength, length);
        int skip = Mathf.Max(listLength - count, 0);
        return source.GetRange(skip, count);
    }

}