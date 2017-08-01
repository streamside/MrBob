using System;

public static class Settings
{
    public static int StartingMoney = 10000;

    public static string Currency = "$";

    public static string DateTimeFormat = "yyyy-MM-dd\nHH:mm";

    public static class HUD
    {
        public static string DateUIName = "Date";
        public static string MoneyUIName = "Money";
    }

    public static class Time
    {
        public static DateTime DefaultStartDate = new DateTime(2017, 1, 1, 0, 0, 0);

        public static Speed DefaultSpeed = Speed.Normal;
    }
}
