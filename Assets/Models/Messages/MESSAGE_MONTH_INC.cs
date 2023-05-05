public class MESSAGE_MONTH_INC
{
    public MESSAGE_MONTH_INC(int year, int month)
    {
        Year = year;
        Month = month;
    }

    public int Year { get; }
    public int Month { get; }
}

public class MESSAGE_DAY_INC
{
    public MESSAGE_DAY_INC(int year, int month, int day)
    {
        Year = year;
        Month = month;
        DAY = day;
    }

    public int Year { get; }
    public int Month { get; }
    public int DAY { get; }
}
