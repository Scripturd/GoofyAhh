using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette.Items;

public class Saw : IItem
{
    private readonly Context _context;
    private readonly UiService _uiService;
    private readonly Shotgun _shotgun;

    public string Name => "Hand Saw";

    public Saw(Context context, UiService uiService, Shotgun shotgun)
    {
        _context = context;
        _uiService = uiService;
        _shotgun = shotgun;
    }

    public void PlayerUse()
    {
        _uiService.PrintPause( "You saw the barrel off");
        _shotgun.SawOff();
        _context.TransitionTo<PlayerTurnState>();
    }
    public void DealerUse()
    {
        _uiService.PrintPause( "The dealer saws the barrel off");
        _shotgun.SawOff();
        _context.TransitionTo<DealerTurnState>();
    }
}