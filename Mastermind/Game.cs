using GoofyAhh.Common;

namespace GoofyAhh.Mastermind;

public class Game
{
    private readonly UiService _uiService;
    private readonly Random _random;
    private readonly GameUi _gameUi;

    private int _itemTypeCount;
    private int _itemCount;
    private char[] _correctAnswer;

    public Game(
        UiService uiService, 
        Random random,
        GameUi gameUi)
    {
        _uiService = uiService;
        _random = random;
        _gameUi = gameUi;
    }

    public void Start()
    {
        _gameUi.Reset();

        _itemTypeCount = 6;// _uiService.SelectInt("How many item types should there be?", 2, 24);
        _itemCount = 4; // _uiService.SelectInt("How many item slots should there be?", 2, 100);

        char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        _correctAnswer = new char[_itemCount];
        for (int i = 0; i < _itemCount; i++)
        {
            _correctAnswer[i] = alphabet[_random.Next(_itemTypeCount)];
        }

        Guess(out bool win);

        _uiService.HorizontalBar();

        if (win)
            _uiService.Print("You won, congrats!");

        if (_uiService.Confirm("Play again"))
            Start();
    }

    private void Guess(out bool win)
    {
        _uiService.HorizontalBar();
        //_uiService.Print(new string(_correctAnswer));

        char[] userAnswer = _gameUi.AskCode(_itemTypeCount, _itemCount);
        int correctCount = 0;
        for (int i = 0; i < _itemCount; i++)
        {
            if (userAnswer[i] == _correctAnswer[i])
                correctCount++;
        }

        if (correctCount == _itemCount)
        {
            win = true;
            return;
        }

        _uiService.Print($"{correctCount} items are correct");
        Guess(out win);
    }
}