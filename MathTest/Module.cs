using GoofyAhh.Common;
using GoofyAhh.Common.Commands;

namespace GoofyAhh.MathTest
{
    public class Module
    {
        public Module(
            MainMenuCommandRegistry commandRegistry,
            UiService uiService,
            Random random)
        {
            MathTest mathTest = new(uiService, random);
            StartCommand startCommand = new(mathTest);

            commandRegistry.AddMainMenuCommand(startCommand);
        }
    }
}