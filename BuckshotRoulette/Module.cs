using GoofyAhh.Common;
using GoofyAhh.Common.Commands;

namespace GoofyAhh.BuckshotRoulette;

public class Module
{
    public Module(
        MainMenuCommandRegistry commandRegistry,
        UiService uiService, 
        Random random)
    {
        Game game = new(uiService, random);
        StartCommand startCommand = new(game);

        commandRegistry.AddMainMenuCommand(startCommand);
    }
}