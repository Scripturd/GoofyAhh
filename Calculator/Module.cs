using GoofyAhh.Common;
using GoofyAhh.Common.Commands;

namespace GoofyAhh.Calculator;

internal class Module
{
    public Module(
        CommandRegistry commandRegistry,
        UiService uiService)
    {
        Calculator calculator = new(uiService);
        StartCommand startCommand = new(calculator);

        commandRegistry.AddMainMenuCommand(startCommand);
    }
}