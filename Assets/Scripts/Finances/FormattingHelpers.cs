public static class FormattingHelpers
{
    public static int OneMilion = 1000000;
    public static int TenThousand = 10000;
    public static int OneThousand = 1000;

    public static string FormatCurrency(int value)
    {
        if (value >= OneMilion)
        {
            return string.Format("$ {0:0.0}M", value / OneMilion);
        }
        else if (value >= TenThousand)
        {
            return string.Format("$ {0:0.0}k", value / OneThousand);
        }
        else
        {
            return string.Format("$ {0}", value);
        }
    }

    public static string FormatPercentage(int value)
    {
        return string.Format("{0} %", value);
    }

    public static string FormatHumanTime(int timestamp)
    {
        var t = new TimeStructure(timestamp);
        return string.Format("Year: {0} - Day: {1} - {2}h{3:00}", t.years, t.days, t.hours, t.minutes);
    }

}
