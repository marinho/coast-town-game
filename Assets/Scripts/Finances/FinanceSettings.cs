using UnityEngine;
using UnityEngine.UI;

public class FinanceSettings : MonoBehaviour
{
    public int bondsPercentage = 40; // stocks get the rest
    public int currentBalance = 0;
    public int currentTimestamp = 0; // 1 = 1 hour in the game = 1 minute in real time

    public Text timestampDisplayText;
    public Text bondsDisplayText;
    public Text stocksDisplayText;
    public Text bestScenarioText;
    public Text worstScenarioText;
    public Text riskRateText;
    public Text balanceDisplayText;

    private bool isInitialized = false;
    private FinanceData financeData;
    private float timerCounter = 0;

    public int GetBondsPercentage()
    {
        return bondsPercentage;
    }

    public int GetStocksPercentage()
    {
        return 100 - GetBondsPercentage();
    }

    public void SetBondsPercentage(float value)
    {
        if (!isInitialized)
        {
            return;
        }
        bondsPercentage = (int)value;
        PlayerPrefs.SetInt(FinancePrefKeys.BondsPercentage, bondsPercentage);
        UpdateUIValues();
    }

    private void UpdateUICurrentTimestamp()
    {
        if (timestampDisplayText != null)
        {
            timestampDisplayText.text = FormattingHelpers.FormatHumanTime(currentTimestamp);
        }
    }

    private void UpdateUIRiskRate()
    {
        if (riskRateText != null)
        {
            var percentage = bondsPercentage * FinanceConsts.BondsRate + GetStocksPercentage() * FinanceConsts.minStocksRate;
            if (percentage < -4)
            {
                riskRateText.text = "High";
            }
            else if (percentage < -1)
            {
                riskRateText.text = "Medium";
            }
            else
            {
                riskRateText.text = "Low";
            }
        }
    }

    private void UpdateUIValues()
    {
        UpdateUICurrentTimestamp();

        if (bondsDisplayText != null)
        {
            bondsDisplayText.text = FormattingHelpers.FormatPercentage(GetBondsPercentage());
        }

        if (stocksDisplayText != null)
        {
            stocksDisplayText.text = FormattingHelpers.FormatPercentage(GetStocksPercentage());
        }

        if (bestScenarioText != null)
        {
            var percentage = bondsPercentage * FinanceConsts.BondsRate + GetStocksPercentage() * FinanceConsts.maxStocksRate;
            bestScenarioText.text = FormattingHelpers.FormatPercentage((int)percentage);
        }

        if (worstScenarioText != null)
        {
            var percentage = bondsPercentage * FinanceConsts.BondsRate + GetStocksPercentage() * FinanceConsts.minStocksRate;
            worstScenarioText.text = FormattingHelpers.FormatPercentage((int)percentage);
        }

        UpdateUIRiskRate();

        if (balanceDisplayText != null)
        {
            balanceDisplayText.text = FormattingHelpers.FormatCurrency(currentBalance);
        }
    }

    private void LoadCurrentBalance()
    {
        if (PlayerPrefs.HasKey(FinancePrefKeys.CurrentBalance))
        {
            currentBalance = PlayerPrefs.GetInt(FinancePrefKeys.CurrentBalance);
        }
        else
        {
            currentBalance = FinanceConsts.InitialBalance;
            financeData.CreditIntoBalance(currentBalance, "Initial credit");
        }
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
        if (timerCounter >= 1)
        {
            timerCounter = timerCounter % 1;
            UpdateCurrentTimestamp(currentTimestamp + 1);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        financeData = GetComponent<FinanceData>();

        bondsPercentage = PlayerPrefs.GetInt(FinancePrefKeys.BondsPercentage);

        LoadCurrentBalance();
        LoadCurrentTimestamp();

        UpdateUIValues();
        isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUICurrentTimestamp();
        UpdateTimer();
    }

}

public static class FinanceConsts
{
    public static float BondsRate = 0.01f; // always +
    public static float minStocksRate = -0.06f;
    public static float maxStocksRate = 0.10f;
    public static int InitialBalance = 1000000; // 1 milion
    public static int InitialTimestamp = 0; // before first day and hour passed
}
