namespace GoofyAhh.Common;

public class UiService
{
    private readonly Random _random;
    private readonly string[] _yesOrNoResponses = [
        "Please say yes or no", 
        "Say yes or no", 
        "for the third time say yes or no human!", 
        "IT WAS A SIMPLE YES OR NO QUESTION!", 
        "Listen here stupid, if you fail again, I'm coming for you", 
        "..."];

    public UiService(Random random)
    {
        _random = random;
    }

    public void ClearLastLine()
    {
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, Console.CursorTop);
    }
    public void ReplaceLastLine(string text)
    {
        int line = Console.CursorTop - 1;

        Console.SetCursorPosition(0, line);
        Console.Write(text.PadRight(Console.WindowWidth - 1));
        Console.SetCursorPosition(0, line + 1);
    }
    public void ReplaceLine(int top, string text)
    {
        int currentTop = Console.CursorTop;
        int currentLeft = Console.CursorLeft;

        Console.SetCursorPosition(0, top);

        int width = Console.WindowWidth;

        if (text.Length > width)
            text = text[..width];

        Console.Write(text.PadRight(width));

        Console.SetCursorPosition(currentLeft, currentTop);
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
    public void PrintSlowly(string text)
    {
        int minDelay = 25;
        int maxDelay = 150;

        foreach (char character in text)
        {
            Console.Write(character);

            if (character == ' ')
                continue;

            int delay = _random.Next(minDelay, maxDelay + 1);
            Thread.Sleep(delay);
        }

        Console.WriteLine();
    }
    public void LoadingAnimation(int milliseconds, string text = "")
    {
        int animationTime = 1000;
        int remainingTime = milliseconds;

        int animationPhase = 0;

        Print(text + ".");

        while (remainingTime > 0)
        {
            int sleepTime = Math.Min(animationTime / 3, remainingTime);
            Thread.Sleep(sleepTime);
            remainingTime -= sleepTime;

            animationPhase = (animationPhase + 1) % 3;

            string dots = animationPhase switch
            {
                0 => ".",
                1 => "..",
                _ => "..."
            };

            ReplaceLastLine(text + dots);
        }

        ReplaceLastLine(text);
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

    public bool SelectYesNo(string question)
        => SelectYesNo(question, angerLevel: 0);
    private bool SelectYesNo(string question, int angerLevel)
    {
        Print(question);
        string input = ReadLine();

        if (input == "yes")
            return true;

        if (input == "no")
            return false;

        angerLevel = int.Min(angerLevel, _yesOrNoResponses.Length - 1);
        Print(_yesOrNoResponses[angerLevel]);

        return SelectYesNo(question, angerLevel + 1);
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

    public int SelectString(string question, string[] options)
    {
        Print(question);

        for (int i = 0; i < options.Length; i++)
            Console.WriteLine($"({i}): {options[i]}");

        int selectedIndex = SelectInt(min: 0, max: options.Length - 1);

        Print($"You selected ({options[selectedIndex]})");
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