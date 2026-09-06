using GoofyAhh.Common.Commands;

namespace GoofyAhh.BuckshotRoulette;

public class StartCommand : ICommand
{
    private readonly Game _game;

    public string Name => "Buckshot Roulette";

    public StartCommand(Game game)
    {
        _game = game;
    }

    public void Execute()
    {
        _game.Start();
    }
}