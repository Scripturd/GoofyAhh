using Spectre.Console;

namespace GoofyAhh.Common.Commands;

public static class CommandUiExtensions
{
    public static ICommand SelectCommand(
        this UiService uiService,
        IReadOnlyList<ICommand> commands,
        string question)
    {
        string[] commandNames = [.. commands.Select(command => command.Name)];

        return AnsiConsole.Prompt(
            new SelectionPrompt<ICommand>()
            .UseConverter((x) => x.Name)
            .AddChoices(commands)
            .Title(question));

        int index = uiService.SelectString(question, commandNames);

        return commands[index];
    }
}