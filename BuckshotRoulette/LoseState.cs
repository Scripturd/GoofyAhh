using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette;

public class LoseState : IState
{
    private readonly UiService _uiService;

    public LoseState(UiService uiService)
    {
        _uiService = uiService;
    }

    public void Start()
    {
        _uiService.PrintPause("You've been killed");
        _uiService.PrintPause("You lose.");
        _uiService.Print("Press any key to exit.");
        _uiService.WaitForKeyPress();
    }
}