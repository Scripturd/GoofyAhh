using GoofyAhh.Common.Commands;

namespace GoofyAhh.MathTest;

public class StartCommand : ICommand
{
    private readonly MathTest _mathTest;

    public string Name => "Math Test";

    public StartCommand(
        MathTest mathTest)
    {
        _mathTest = mathTest;
    }
    
    public void Execute()
    {
        _mathTest.Start();
    }
}