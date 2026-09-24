using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette.Items;

public class CigaretteFactory
{
    private readonly Context _context;
    private readonly UiService _uiService;
    private readonly Health _playerHealth;
    private readonly Health _dealerHealth;

    public CigaretteFactory(Context context, UiService uiService, Health playerHealth, Health dealerHealth)
    {
        _context = context;
        _uiService = uiService;
        _playerHealth = playerHealth;
        _dealerHealth = dealerHealth;
    }

    public Cigarette Create()
    {
        return new Cigarette(_context, _uiService, _playerHealth, _dealerHealth);
    }
}