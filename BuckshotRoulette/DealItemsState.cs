using GoofyAhh.BuckshotRoulette.Items;
using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette;

public class DealItemsState : IState
{
    private readonly Context _context;
    private readonly UiService _uiService;
    private readonly Inventory _playerItems;
    private readonly Inventory _dealerItems;
    private readonly SawFactory _sawFactory;
    private readonly CigaretteFactory _cigaretteFactory;
    private readonly Random _random;

    public DealItemsState(
        Context context,
        UiService uiService,
        Inventory playerItems, 
        Inventory dealerItems, 
        Random random,
        SawFactory sawFactory,
        CigaretteFactory cigaretteFactory)
    {
        _context = context;
        _uiService = uiService;
        _playerItems = playerItems;
        _dealerItems = dealerItems;
        _random = random;
        _sawFactory = sawFactory;
        _cigaretteFactory = cigaretteFactory;
    }

    public void Start()
    {
        int itemDealCount = _random.Next(2, 4);

        _uiService.PrintPause($"{itemDealCount} items have been dealt to you and the dealer each.");
        _uiService.Print("Press any key to continue.");
        _uiService.WaitForKeyPress();

        _uiService.HorizontalBar();
        _uiService.Print("Your items:");
        for (int i = 0; i < itemDealCount; i++)
        {
            IItem playerItem = CreateRandomItem();
            _uiService.Print($"[{playerItem.Name}]");
            _playerItems.Add(playerItem);
        }

        _uiService.HorizontalBar();
        _uiService.Print("Dealer's items:");
        for (int i = 0; i < itemDealCount; i++)
        {
            IItem dealerItem = CreateRandomItem();
            _uiService.Print($"[{dealerItem.Name}]");
            _dealerItems.Add(dealerItem);
        }

        _uiService.HorizontalBar();

        _context.TransitionTo<PlayerTurnState>();
    }

    private IItem CreateRandomItem()
    {
        int itemIndex = _random.Next(0, 2);
        if (itemIndex == 0)
            return _sawFactory.Create();
        
        return _cigaretteFactory.Create();
    }
}