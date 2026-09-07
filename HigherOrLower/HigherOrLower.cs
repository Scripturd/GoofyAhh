using GoofyAhh.Common;

namespace GoofyAhh.HigherOrLower;

public class HigherOrLower
{
    private readonly UiService _uiService;
    private readonly Random _random;

    public HigherOrLower(
        UiService uiService,
        Random random)
    {
        _uiService = uiService;
        _random = random;
    }

    public void Start()
    {
        int initialGuesses = _uiService.SelectInt("How many guesses do you want?", 1, 100);
        int minValue = 1;
        int maxValue = _uiService.SelectInt("Select the range", min: 2);

        int correctAnswer = _random.Next(minValue, maxValue + 1);
        bool hasUserWon = false;

        int remainingGuesses = initialGuesses;
        while (remainingGuesses > 0 && !hasUserWon)
        {
            string remainingGuessesText = remainingGuesses switch
            {
                1 => "Last guess",
                _ => $"{remainingGuesses} remaining guesses"
            };

            int userAnswer = _uiService.SelectInt(
                $"Guess a number between {minValue} and {maxValue}, ({remainingGuessesText})", minValue, maxValue);

            remainingGuesses--;

            if (userAnswer < correctAnswer)
            {
                _uiService.Print("Higher");
                minValue = userAnswer;
                continue;
            }

            if (userAnswer > correctAnswer)
            {
                _uiService.Print("Lower");
                maxValue = userAnswer;
                continue;
            }

            hasUserWon = true;
            break;
        }

        if (hasUserWon)
        {
            string remainingGuessesText = remainingGuesses switch
            {
                0 => "and last guess",
                1 => "guess, leaving just 1 guess to spare",
                _ => $"guess, leaving {remainingGuesses} guesses to spare"
            };

            int guessesUsed = initialGuesses - remainingGuesses;
            _uiService.Print($"You guessed correctly on the {guessesUsed.ToOrdinal()} {remainingGuessesText}");
        }
        else
        {
            _uiService.Print($"You lost, the correct answer was {correctAnswer}");
        }

        if (_uiService.Confirm("Play again?"))
            Start();
    }
}