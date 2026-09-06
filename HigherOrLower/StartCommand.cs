using GoofyAhh.Common.Commands;

namespace GoofyAhh.HigherOrLower;

public class StartCommand : ICommand
{
    private readonly HigherOrLower _higherOrLower;

    public string Name => "Higher or Lower";

    public StartCommand(
        HigherOrLower higherOrLower)
    {
        _higherOrLower = higherOrLower;
    }

    public void Execute()
    {
        _higherOrLower.Start();
    }
}