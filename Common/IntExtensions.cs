namespace GoofyAhh.Common;

public static class IntExtensions
{
    public static string ToOrdinal(this int number)
    {
        int lastTwoDigits = number % 100;

        if (lastTwoDigits is >= 11 and <= 13)
            return $"{number}th";

        return (number % 10) switch
        {
            1 => $"{number}st",
            2 => $"{number}nd",
            3 => $"{number}rd",
            _ => $"{number}th"
        };
    }
}