using GoofyAhh.BuckshotRoulette.Items;
using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette;

public class Game
{
    private readonly UiService _uiService;
    private readonly Random _random;

    public Game(
        UiService uiService,
        Random random)
    {
        _uiService = uiService;
        _random = random;
    }

    public void Start()
    {
        int initialHealth = _uiService.SelectInt("Select the amount of health for you and the dealer", 1, 5);


        Health playerHealth = new(initialHealth);
        Inventory playerInventory = new();

        Health dealerHealth = new(initialHealth);
        Inventory dealerInventory = new();

        Context context = new();

        Shotgun shotgun = new(context, _uiService, _random, playerHealth, dealerHealth);
        GameUi gameUi = new(_uiService, shotgun, playerHealth, dealerHealth);

        SawFactory sawFactory = new(context, _uiService, shotgun);
        CigaretteFactory cigaretteFactory = new(context, _uiService, playerHealth, dealerHealth);

        DealItemsState dealItemsState = new(context, _uiService, playerInventory, dealerInventory, _random, sawFactory, cigaretteFactory);
        ReloadState reloadState = new(context, _uiService, shotgun);
        PlayerTurnState playerTurnState = new(_uiService, playerInventory, gameUi, shotgun);
        DealerTurnState dealerTurnState = new(_uiService, dealerInventory, gameUi, _random, shotgun);
        WinState winState = new(_uiService);
        LoseState loseState = new(_uiService);

        context.AddState(dealItemsState);
        context.AddState(reloadState);
        context.AddState(playerTurnState);
        context.AddState(dealerTurnState);
        context.AddState(winState);
        context.AddState(loseState);

        _uiService.PrintPause("Starting game");

        context.TransitionTo<ReloadState>();
    }
}