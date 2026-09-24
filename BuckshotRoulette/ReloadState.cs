using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette;

public class ReloadState : IState
{
    private readonly Context _context;
    private readonly UiService _uiService;
    private readonly Shotgun _shotgun;

    public ReloadState(
        Context context,
        UiService uiService,
        Shotgun shotgun)
    {
        _context = context;
        _uiService = uiService;
        _shotgun = shotgun;
    }

    public void Start()
    {
        _shotgun.Reload();

        _uiService.PrintPause("A new round begins. Press any key to continue.");
        _uiService.WaitForKeyPress();
        _uiService.Clear();

        string liveText = _shotgun.LiveShellAmount == 1 ? "A single live" : $"{_shotgun.LiveShellAmount} lives";
        string blankText = _shotgun.BlankShellAmount == 1 ? "a single blank" : $"{_shotgun.BlankShellAmount} blanks";
        _uiService.PrintPause($"{liveText} and {blankText} enter in an unknown order");

        _context.TransitionTo<DealItemsState>();
    }
}