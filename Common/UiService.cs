namespace GoofyAhh.Common;

public class UiService
{
    private readonly Random _random;

    public UiService(Random random)
    {
        _random = random;
    }

    public void PrintPause(string text)
    {
        Thread.Sleep(2000);
        Print(text);
    }

    public void Clear()
    {
        Console.Clear();
    }
    public void Space(int thickness = 1)
    {
        if (thickness < 1 || thickness > 10)
            throw new ArgumentOutOfRangeException(nameof(thickness));

        for (int i = 0; i < thickness; i++)
            Print("");
    }
    public void HorizontalBar(int thickness = 1)
    {
        if (thickness < 1 || thickness > 10)
            throw new ArgumentOutOfRangeException(nameof(thickness));

        Space();

        for (int i = 0; i < thickness; i++)
            Print("--------------------------------------------------------------");

        Space();
    }
    public void Print(string text)
    {
        Console.WriteLine(text);
    }

    public string ReadLine()
    {
        Console.CursorVisible = true;
        string? input = Console.ReadLine();
        Console.CursorVisible = false;

        if (input == null)
            return string.Empty;

        return input;
    }
    public void WaitForKeyPress()
    {
        Console.ReadKey(intercept: true);
    }

    public bool Confirm(string question)
    {
        Print(question);
        string input = ReadLine();

        if (input == "yes")
            return true;

        if (input == "no")
            return false;

        return Confirm(question);
    }

    public int SelectInt(string question, int min = int.MinValue, int max = int.MaxValue)
    {
        Print(question);
        return SelectInt(min, max);
    }
    private int SelectInt(int min = int.MinValue, int max = int.MaxValue)
    {
        string input = ReadLine();

        bool isValidInteger = int.TryParse(input, out int parsedInput);

        if (parsedInput < min)
        {
            isValidInteger = false;
            Print($"Your number can't be less than {min}");
        }
        else if (parsedInput > max)
        {
            isValidInteger = false;
            Print($"Your number can't be more than {max}");
        }
        else if (!isValidInteger)
            Print("Type a valid integer");

        if (isValidInteger)
            return parsedInput;
        else
            return SelectInt(min, max);
    }

    public int SelectString(string question, string[] choices)
    {
        Print(question);

        for (int i = 0; i < choices.Length; i++)
            Console.WriteLine($"({i}): {choices[i]}");

        int selectedIndex = SelectInt(min: 0, max: choices.Length - 1);

        Print($"You selected ({choices[selectedIndex]})");
        Space();

        return selectedIndex;
    }

    public T SelectEnum<T>(
    string question)
    where T : struct, Enum
    {
        string[] options = Enum.GetNames<T>();
        int index = SelectString(question, options);

        return Enum.GetValues<T>()[index];
    }
}