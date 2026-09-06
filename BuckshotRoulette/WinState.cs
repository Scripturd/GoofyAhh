using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette;

internal class WinState : IState
{
    private readonly UiService _uiService;

    public WinState(UiService uiService)
    {
        _uiService = uiService;
    }

    public void Start()
    {
        _uiService.PrintSlowly("The dealer is dead");
        _uiService.PrintSlowly("You won, congratulations!");
    }
}