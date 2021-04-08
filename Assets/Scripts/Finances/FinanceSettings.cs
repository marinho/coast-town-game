using UnityEngine;
using UnityEngine.UI;

public class FinanceSettings : MonoBehaviour
{
    public int bondsPercentage = 40; // stocks get the rest
    public int walletBalance = 0;
    public int bondsBalance = 0;
    public int stocksBalance = 0;

    public Text bondsDisplayText;
    public Text stocksDisplayText;
    public Text bestScenarioText;
    public Text worstScenarioText;
    public Text riskRateText;
    public Text bondsDisplayValue;
    public Text stocksDisplayValue;
    public Text walletDisplayValue;

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

    public void UpdateInvestmentsDaily()
    {
        Debug.Log("New day!"); // XXX
    }

    private void UpdateUIRiskRate()
    {
        if (riskRateText == null)
        {
            return;
        }

        var percentage = bondsPercentage * FinanceConsts.BondsRate + GetStocksPercentage() * FinanceConsts.MinStocksRate;
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
            var percentage = bondsPercentage * FinanceConsts.BondsRate + GetStocksPercentage() * FinanceConsts.MaxStocksRate;
            bestScenarioText.text = FormattingHelpers.FormatPercentage((int)percentage);
        }

        if (worstScenarioText != null)
        {
            var percentage = bondsPercentage * FinanceConsts.BondsRate + GetStocksPercentage() * FinanceConsts.MinStocksRate;
            worstScenarioText.text = FormattingHelpers.FormatPercentage((int)percentage);
        }

        UpdateUIRiskRate();

        if (walletDisplayValue != null)
        {
            walletDisplayValue.text = FormattingHelpers.FormatCurrency(walletBalance);
        }
        if (bondsDisplayValue != null)
        {
            bondsDisplayValue.text = FormattingHelpers.FormatCurrency(bondsBalance);
        }
        if (stocksDisplayValue != null)
        {
            stocksDisplayValue.text = FormattingHelpers.FormatCurrency(stocksBalance);
        }
    }

    private void LoadWalletBalance()
    {
        if (PlayerPrefs.HasKey(FinancePrefKeys.WalletBalance))
        {
            walletBalance = PlayerPrefs.GetInt(FinancePrefKeys.WalletBalance);
        }
        else
        {
            walletBalance = FinanceConsts.InitialWallet;
            financeData.CreditIntoWallet(walletBalance, "Initial balance");
        }
    }

    private void LoadBondsBalance()
    {
        if (PlayerPrefs.HasKey(FinancePrefKeys.BondsBalance))
        {
            bondsBalance = PlayerPrefs.GetInt(FinancePrefKeys.BondsBalance);
        }
        else
        {
            bondsBalance = FinanceConsts.InitialBonds;
            financeData.CreditIntoBonds(bondsBalance, "Initial bonds");
        }
    }

    private void LoadStocksBalance()
    {
        if (PlayerPrefs.HasKey(FinancePrefKeys.StocksBalance))
        {
            stocksBalance = PlayerPrefs.GetInt(FinancePrefKeys.StocksBalance);
        }
        else
        {
            stocksBalance = FinanceConsts.InitialStocks;
            financeData.CreditIntoStocks(stocksBalance, "Initial stocks");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        financeData = GetComponent<FinanceData>();

        bondsPercentage = PlayerPrefs.GetInt(FinancePrefKeys.BondsPercentage);

        LoadWalletBalance();
        LoadBondsBalance();
        LoadStocksBalance();

        UpdateUIValues();
        isInitialized = true;
    }

}
