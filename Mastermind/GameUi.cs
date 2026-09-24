using GoofyAhh.Common;

namespace GoofyAhh.Mastermind;

public class GameUi
{
    private readonly UiService _uiService;

    private string _reminder = string.Empty;

    public GameUi(UiService uiService)
    {
        _uiService = uiService;
    }

    public void Reset()
    {
        _reminder = string.Empty;
    }

    public char[] AskCode(int itemTypeCount, int itemCount)
    {
        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        _uiService.Print($"Each item in the code is a character between A and {alphabet[itemTypeCount - 1]}");
        _uiService.Print($"The code is {itemCount} items long");
        _uiService.Print("Guess the secret code.");

        if (_reminder == string.Empty)
            for (int i = 0; i < itemCount; i++)
                _reminder += '_';

        return ReadCode(itemTypeCount, itemCount);
    }
    private char[] ReadCode(int itemTypeCount, int itemCount)
    {
        _uiService.Print(_reminder);

        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        string userAnswer = _uiService.ReadLine();

        if (userAnswer.Equals("reminder", StringComparison.OrdinalIgnoreCase))
        {
            _uiService.Print("Set reminder");
            _reminder = _uiService.ReadLine();
            return ReadCode(itemTypeCount, itemCount);
        }

        if (userAnswer.Length != itemCount)
        {
            _uiService.Print($"You wrote {userAnswer.Length} characters compared to the expected {itemCount} characters");
            return AskCode(itemTypeCount, itemCount);
        }

        for (int i = 0; i < userAnswer.Length; i++)
        {
            bool isValid = false;
            for (int j = 0; j < itemTypeCount; j++)
            {
                if (userAnswer[i] == alphabet[j])
                {
                    isValid = true;
                    break;
                }
            }

            if (isValid)
                continue;

            _uiService.Print($"Item nr {i} [{userAnswer[i]}] is not a valid item");
            return ReadCode(itemTypeCount, itemCount);
        }

        return userAnswer.ToCharArray();
    }


}