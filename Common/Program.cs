using GoofyAhh.Common.Commands;
using Spectre.Console;

namespace GoofyAhh.Common;

public class Program
{
    static void Main(string[] args)
    {
        Random random = new();
        UiService uiService = new(random);
        CommandRegistry commandRegistry = new();

        BuckshotRoulette.Module buckshotRoulette = new(
            commandRegistry,
            uiService,
            random);
        Calculator.Module calculator = new(
            commandRegistry,
            uiService);
        MathTest.Module mathTest = new(
            commandRegistry,
            uiService, 
            random);
        HigherOrLower.Module higherOrLower = new(
            commandRegistry, 
            uiService, 
            random);

        uiService.Clear();
        DrawPanel();

        while (true)
        {
            Console.Title = "Main Menu";
            Console.ResetColor();
            uiService.HorizontalBar();
            ICommand selectedCommand = uiService.SelectCommand(commandRegistry.MainMenuCommands, "Select a command:");
            Console.Title = selectedCommand.Name;
            uiService.Clear();
            DrawPanel();
            selectedCommand.Execute();
        }

        void DrawPanel()
        {
            Panel panel = new("panel");
            AnsiConsole.Write(panel);
        }
    }
}