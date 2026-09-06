using GoofyAhh.Common;
using GoofyAhh.Common.Commands;

namespace GoofyAhh.BuckshotRoulette;

internal class Module
{
    public Module(
        CommandRegistry commandRegistry,
        UiService uiService, 
        Random random)
    {
        Game game = new(uiService, random);
        StartCommand startCommand = new(game);

        commandRegistry.AddMainMenuCommand(startCommand);
    }
}