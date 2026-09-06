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
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Red;

        Player player = new(0);
        Dealer dealer = new(0);

        Shotgun shotgun = new(_random);
        GameUi gameUi = new(_uiService, player, dealer, shotgun);
        gameUi.Clear();

        _uiService.PrintSlowly("Press enter to start");
        _uiService.ReadLine();
        gameUi.Clear();
        _uiService.LoadingAnimation(1000);
        gameUi.Clear();

        int initialHealth = _uiService.SelectInt("Select the amount of health for you and the dealer", 1, 5);
        player.Heal(initialHealth);
        dealer.Heal(initialHealth);

        Context context = new();
        ReloadState reloadState = new(context, gameUi, _uiService, shotgun);
        PlayerTurnState playerTurnState = new(context, gameUi, _uiService, shotgun, player, dealer);
        DealerTurnState dealerTurnState = new(context, gameUi, _uiService, _random, shotgun, player, dealer);
        WinState winState = new(_uiService);
        LoseState loseState = new(_uiService);

        context.AddState(reloadState);
        context.AddState(playerTurnState);
        context.AddState(dealerTurnState);
        context.AddState(winState);
        context.AddState(loseState);

        gameUi.Clear();
        _uiService.LoadingAnimation(3000, "Starting game");

        context.TransitionTo<ReloadState>();
    }
}