using GoofyAhh.Common;

namespace GoofyAhh.Common.Commands;

public static class CommandUiExtensions
{
    public static ICommand SelectCommand(
        this UiService uiService,
        IReadOnlyList<ICommand> commands,
        string question)
    {
        string[] commandNames = commands.Select(command => command.Name).ToArray();

        int index = uiService.SelectString(question, commandNames);

        return commands[index];
    }
}