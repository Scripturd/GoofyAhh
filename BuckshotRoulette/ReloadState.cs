using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette;

internal class ReloadState : IState
{
    private readonly Context _context;
    private readonly GameUi _gameUi;
    private readonly UiService _uiService;
    private readonly Shotgun _shotgun;

    public ReloadState(
        Context context,
        GameUi gameUi,
        UiService uiService,
        Shotgun shotgun)
    {
        _context = context;
        _gameUi = gameUi;
        _uiService = uiService;
        _shotgun = shotgun;
    }

    public void Start()
    {
        _shotgun.Reload();

        _gameUi.Clear();

        _uiService.LoadingAnimation(3000, "You reload the gun");

        _gameUi.Clear();

        string liveText = _shotgun.LiveShellAmount == 1 ? "A single live" : $"{_shotgun.LiveShellAmount} lives";
        string blankText = _shotgun.BlankShellAmount == 1 ? "a single blank" : $"{_shotgun.BlankShellAmount} blanks";
        _uiService.LoadingAnimation(6000, $"{liveText} and {blankText} enter in an unknown order");

        _gameUi.UpdateAmmoText();

        _gameUi.Clear();

        _context.TransitionTo<PlayerTurnState>();
    }
}