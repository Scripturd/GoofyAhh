using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette;

internal class LoseState : IState
{
    private readonly UiService _uiService;

    public LoseState(UiService uiService)
    {
        _uiService = uiService;
    }

    public void Start()
    {
        _uiService.PrintSlowly("You've been killed");
        _uiService.PrintSlowly("You lose.");
    }
}