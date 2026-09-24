namespace GoofyAhh.Common.Commands;

public class MainMenuCommandRegistry
{
    private readonly List<ICommand> _mainMenuCommands = [];

    public IReadOnlyList<ICommand> MainMenuCommands => _mainMenuCommands;

    public void AddMainMenuCommand(ICommand command)
        => _mainMenuCommands.Add(command);
}