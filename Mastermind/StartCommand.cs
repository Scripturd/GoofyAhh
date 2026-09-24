using GoofyAhh.Common.Commands;

namespace GoofyAhh.Mastermind;

public class StartCommand : ICommand
{
    private readonly Game _game;

    public string Name => "MasterMind";

    public StartCommand(Game game)
    {
        _game = game;
    }

    public void Execute()
    {
        _game.Start();
    }
}
