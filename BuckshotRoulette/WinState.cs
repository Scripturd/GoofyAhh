using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette;

public class WinState : IState
{
    private readonly UiService _uiService;

    public WinState(UiService uiService)
    {
        _uiService = uiService;
    }

    public void Start()
    {
        _uiService.PrintPause("The dealer is dead");
        _uiService.PrintPause("You won, congratulations!");
        _uiService.Print("Press any key to exit.");
        _uiService.WaitForKeyPress();
    }
}