public class TimeStructure
{
    public int timestamp;
    public int minutes;
    public int hours;
    public int days;
    public int years;

    public static int OneHour = 60;
    public static int OneDay = 60 * 24;
    public static int OneYear = 60 * 24 * 365;

    public TimeStructure(int _timestamp)
    {
        timestamp = _timestamp;
        UpdateParts();
    }

    private void UpdateParts()
    {
        int remainder = timestamp;

        // 1 = 1 minute
        minutes = remainder % 60;
        remainder -= minutes;

        // 60 = 1 hour
        hours = remainder / 60 % 24;
        remainder -= hours * OneHour;

        // 24 * 60 = 1 day
        days = remainder / 60 / 24 % 365;
        remainder -= days * OneDay;

        // 24 * 60 * 365 = 1 year
        years = remainder / 60 / 24 / 365;
    }
}
