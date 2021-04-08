using System;
using UnityEngine;

public class TimeStructure
{
    public int timestamp;
    public int minutes;
    public int hours;
    public int days;
    public int years;

    public static int OneHour = 60;
    public static int OneDay = 60 * 24;
    public static int OneMonth = 60 * 24 * 30;
    public static int OneYear = 60 * 24 * 365;
    public static int[] Months = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    public TimeStructure(int _timestamp)
    {
        timestamp = _timestamp;
        UpdateParts();
    }

    public bool IsFirstDayOfAMonth()
    {
        int remainder = days;
        foreach (int monthDays in Months)
        {
            if (remainder == 1)
            {
                return true;
            }
            remainder -= monthDays;
        }
        return false;
    }

    public int GetHour()
    {
        return hours;
    }

    public int GetDayOfYear()
    {
        return days + 1;
    }

    public int GetMonth()
    {
        int month;
        int accumulatedDays = 0;
        for (month = 0; month < 12; month++)
        {
            accumulatedDays += Months[month];
            if (GetDayOfYear() <= accumulatedDays)
            {
                break;
            }
        }
        return month + 1;
    }

    public int GetYear()
    {
        return years + 1;
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
