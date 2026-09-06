namespace GoofyAhh.Common.Commands;

public class CommandRegistry
{
    private readonly List<ICommand> _mainMenuCommands = [];
    private readonly List<ICommand> _utilityCommands = [];

    public IReadOnlyList<ICommand> MainMenuCommands => _mainMenuCommands;
    public IReadOnlyList<ICommand> UtilityCommands => _mainMenuCommands;

    public void AddMainMenuCommand(ICommand command)
        => _mainMenuCommands.Add(command);

    public void AddUtilityCommand(ICommand command)
        => _utilityCommands.Add(command);

    public void RemoveUtilityCommand(ICommand command)
        => _utilityCommands.Remove(command);
}