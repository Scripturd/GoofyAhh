using GoofyAhh.Common;
using GoofyAhh.Common.Commands;

namespace GoofyAhh.Calculator;

public class Module
{
    public Module(
        MainMenuCommandRegistry commandRegistry,
        UiService uiService)
    {
        Calculator calculator = new(uiService);
        StartCommand startCommand = new(calculator);

        commandRegistry.AddMainMenuCommand(startCommand);
    }
}