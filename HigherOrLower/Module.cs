using GoofyAhh.Common;
using GoofyAhh.Common.Commands;

namespace GoofyAhh.HigherOrLower;

public class Module
{
    public Module(
        CommandRegistry commandRegistry,
        UiService uiService,
        Random random)
    {
        HigherOrLower higherOrLower = new(
            uiService, 
            random);

        StartCommand startCommand = new(higherOrLower);

        commandRegistry.AddMainMenuCommand(startCommand);
    }
}