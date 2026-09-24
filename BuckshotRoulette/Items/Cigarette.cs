using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette.Items;

public class Cigarette : IItem
{
    private readonly Context _context;
    private readonly UiService _uiService;
    private readonly Health _playerHealth;
    private readonly Health _dealerHealth;

    public string Name => "Cigarette Pack";

    public Cigarette(Context context, UiService uiService, Health playerHealth, Health dealerHealth)
    {
        _context = context;
        _uiService = uiService;
        _playerHealth = playerHealth;
        _dealerHealth = dealerHealth;
    }

    public void PlayerUse()
    {
        _uiService.PrintPause("You gained 1 health");
        _playerHealth.Heal();
        _context.TransitionTo<PlayerTurnState>();
    }
    public void DealerUse()
    {
        _uiService.PrintPause("The dealer gained 1 health");
        _dealerHealth.Heal();
        _context.TransitionTo<DealerTurnState>();
    }
}