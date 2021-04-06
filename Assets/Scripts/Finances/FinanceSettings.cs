using UnityEngine;
using UnityEngine.UI;

public class FinanceSettings : MonoBehaviour
{
    public int bondsPercentage = 40; // stocks get the rest
    public int currentBalance = 0;

    public Text bondsDisplayText;
    public Text stocksDisplayText;
    public Text bestScenarioText;
    public Text worstScenarioText;
    public Text riskRateText;
    public Text balanceDisplayText;

    private bool isInitialized = false;
    private FinanceData financeData;

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

    // Start is called before the first frame update
    void Start()
    {
        financeData = GetComponent<FinanceData>();

        bondsPercentage = PlayerPrefs.GetInt(FinancePrefKeys.BondsPercentage);

        LoadCurrentBalance();

        UpdateUIValues();
        isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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
