using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette.Items;

public class SawFactory
{
    private readonly Context _context;
    private readonly UiService _uiService;
    private readonly Shotgun _shotgun;

    public SawFactory(Context context, UiService uiService, Shotgun shotgun)
    {
        _context = context;
        _uiService = uiService;
        _shotgun = shotgun;
    }

    public Saw Create()
    {
        return new Saw(_context, _uiService, _shotgun);
    }
}