using GoofyAhh.Common;

namespace GoofyAhh.Calculator;

public class Calculator
{
    private readonly UiService _uiService;

    public Calculator(UiService uiService)
    {
        _uiService = uiService;
    }

    public void Start()
    {
        Operation inputOperation = _uiService.SelectEnum<Operation>("Pick an operation.");
        switch (inputOperation)
        {
            case Operation.Addition:
                Addition();
                break;
            case Operation.Substraction:
                Substraction();
                break;
            case Operation.Multiplication:
                Multiplication();
                break;
            case Operation.Division:
                Division();
                break;
        }

        if (_uiService.SelectYesNo("Do you want to continue using the calc?"))
            Start();
    }

    private void Addition()
    {
        int int0 = _uiService.SelectInt("Pick a number");
        int int1 = _uiService.SelectInt("Pick another number");

        _uiService.Print($"{int0} + {int1} = {int0 + int1}");
    }
    private void Substraction()
    {
        int int0 = _uiService.SelectInt("Pick a number");
        int int1 = _uiService.SelectInt("Pick another number");

        _uiService.Print($"{int0} - {int1} = {int0 - int1}");
    }
    private void Multiplication()
    {
        int int0 = _uiService.SelectInt("Pick a number");
        int int1 = _uiService.SelectInt("Pick another number");

        _uiService.Print($"{int0} * {int1} = {(float)int0 * int1}");
    }
    private void Division()
    {
        int int0 = _uiService.SelectInt("Pick a number");
        int int1 = _uiService.SelectInt("Pick another number");

        _uiService.Print($"{int0} / {int1} = {(float)int0 / int1}");
    }
}