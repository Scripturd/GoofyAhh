using GoofyAhh.Common.Commands;

namespace GoofyAhh.Calculator;

public class StartCommand : ICommand
{
    private readonly Calculator _calculator;

    public string Name => "Calc (short for calculator)";

    public StartCommand(Calculator startCommand)
    {
        _calculator = startCommand;
    }

    public void Execute() => _calculator.Start();
}