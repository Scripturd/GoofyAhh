using GoofyAhh.Common;

namespace GoofyAhh.MathTest;

public class MathTest
{
    private readonly UiService _uiService;
    private readonly Random _random;

    public MathTest(
        UiService uiService,
        Random random)
    {
        _uiService = uiService;
        _random = random;
    }

    public void Start()
    {
        int amountQuestions = _uiService.SelectInt("How many questions should I ask?");

        int amountCorrect = 0;
        for (int i = 0; i < amountQuestions; i++)
        {
            _uiService.Space();
            _uiService.Print($"Question {i + 1} out of {amountQuestions}");
            AskRandomQuestion(out int userAnswer, out int correctAnswer);

            if (userAnswer == correctAnswer)
            {
                amountCorrect++;
                _uiService.Print("Correct");
            }
            else
            {
                _uiService.Print($"Wrong, The answer was {correctAnswer}.");
            }
        }

        _uiService.Print($"You answered {amountCorrect} out of {amountQuestions} questions correctly");

        if (_uiService.SelectYesNo("Do you want to play again?"))
            Start();
    }

    private void AskRandomQuestion(
        out int userAnswer,
        out int correctAnswer)
    {
        switch (RandomOperation(_random))
        {
            case Operation.Addition:
                AskAdditionQuestion(_random, out userAnswer, out correctAnswer);
                return;
            case Operation.Substraction:
                AskSubstractionQuestion(_random, out userAnswer, out correctAnswer);
                return;
            case Operation.Multiplication:
                AskMultiplicationQuestion(_random, out userAnswer, out correctAnswer);
                return;
            case Operation.Division:
                AskDivisionQuestion(_random, out userAnswer, out correctAnswer);
                return;
            default:
                throw new NotSupportedException();
        }
    }

    private void AskAdditionQuestion(
        Random random,
        out int userAnswer,
        out int correctAnswer)
    {
        int int0 = random.Next(10, 100);
        int int1 = random.Next(10, 100);

        userAnswer = _uiService.SelectInt($"{int0} + {int1} = ?");
        correctAnswer = int0 + int1;
    }
    private void AskSubstractionQuestion(
        Random random,
        out int userAnswer,
        out int correctAnswer)
    {
        int int0 = random.Next(20, 100);
        int int1 = random.Next(10, int0);

        userAnswer = _uiService.SelectInt($"{int0} - {int1} = ?");
        correctAnswer = int0 - int1;
    }
    private void AskMultiplicationQuestion(
        Random random,
        out int userAnswer,
        out int correctAnswer)
    {
        int int0 = random.Next(2, 10);
        int int1 = random.Next(2, 10);

        userAnswer = _uiService.SelectInt($"{int0} * {int1} = ?");
        correctAnswer = int0 * int1;
    }
    private void AskDivisionQuestion(
        Random random,
        out int userAnswer,
        out int correctAnswer)
    {
        int divisor = random.Next(2, 10);
        correctAnswer = random.Next(2, 10);
        int dividend = divisor * correctAnswer;

        userAnswer = _uiService.SelectInt($"{dividend} / {divisor} = ?");
    }

    private static Operation RandomOperation(Random random)
        => (Operation)random.Next(Enum.GetValues<Operation>().Length);
}