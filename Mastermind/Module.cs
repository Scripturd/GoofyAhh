using GoofyAhh.Common;
using GoofyAhh.Common.Commands;

namespace GoofyAhh.Mastermind;

public class Module
{
    public Module(
        MainMenuCommandRegistry commandRegistry,
        UiService uiService,
        Random random)
    {
        GameUi gameUi = new(uiService);
        Game game = new(uiService, random, gameUi);
        StartCommand startCommand = new(game);

        commandRegistry.AddMainMenuCommand(startCommand);
    }
}