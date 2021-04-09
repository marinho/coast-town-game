
public static class FinanceConsts
{
    // Rate per year
    public static float BondsRate = 0.02f; // always +
    public static float MinStocksRate = -0.06f;
    public static float MaxStocksRate = 0.10f;

    public static float BondsRatePerDay = .0000545f; // 100 * (1.0000545 ** 365) = 102.0
    public static float MinStockRatePerDay = -.03f;
    public static float MaxStockRatePerDay = +.05f;

    public static int InitialWallet = 50;
    public static int InitialBonds = 400000; // 40% of 1 milion
    public static int InitialStocks = 600000; // 60% of 1 milion

    public static string BondsDailyDescription = "Bonds interest rates";
    public static string StocksDailyDescription = "Stocks variation";
}
