using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette;

internal class DealerTurnState : IState
{
    private readonly Context _context;
    private readonly GameUi _gameUi;
    private readonly UiService _uiService;
    private readonly Random _random;
    private readonly Shotgun _shotgun;
    private readonly Player _player;
    private readonly Dealer _dealer;

    public DealerTurnState(
        Context context,
        GameUi gameUi,
        UiService uiService,
        Random random,
        Shotgun shotgun,
        Player player,
        Dealer dealer)
    {
        _context = context;
        _gameUi = gameUi;
        _uiService = uiService;
        _random = random;
        _shotgun = shotgun;
        _player = player;
        _dealer = dealer;
    }

    public void Start()
    {
        _uiService.LoadingAnimation(3000, "Dealer's turn");
        SelectTarget();
    }

    private void SelectTarget()
    {
        _gameUi.UpdateAmmoText();
        _gameUi.Clear();

        if (_shotgun.IsEmpty)
        {
            _uiService.Print("The gun is empty");
            _context.TransitionTo<ReloadState>();
        }

        _gameUi.Clear();
        _uiService.LoadingAnimation(3000, "The dealer selects his target");
        Target selectedTarget = (Target)_random.Next(0, 2);

        if (selectedTarget == Target.You)
        {
            _uiService.LoadingAnimation(3000, "He chooses himself");
            _gameUi.Clear();
            _uiService.LoadingAnimation(3000, "He pulls the trigger");

            ShootAtSelf();
        }
        else
        {
            _uiService.LoadingAnimation(3000, "He chooses you");
            _gameUi.Clear();
            _uiService.LoadingAnimation(3000, "He pulls the trigger");

            ShootAtOpponent();
        }
    }

    private void ShootAtSelf()
    {
        ShellType shellType = _shotgun.ShootAt(_dealer);
        if (shellType == ShellType.Live)
        {
            _uiService.LoadingAnimation(3000, "It was live, he shot himself");

            if (!_dealer.IsAlive)
            {
                _context.TransitionTo<WinState>();
                return;
            }

            _context.TransitionTo<PlayerTurnState>();
        }
        else
        {
            _uiService.LoadingAnimation(3000, "It was blank, dealer's turn again");

            SelectTarget();
        }
    }
    private void ShootAtOpponent()
    {
        ShellType shellType = _shotgun.ShootAt(_player);
        if (shellType == ShellType.Live)
        {
            _uiService.LoadingAnimation(3000, "It was live, he shot you");

            if (!_player.IsAlive)
            {
                _context.TransitionTo<LoseState>();
                return;
            }
        }
        else
        {
            _uiService.LoadingAnimation(3000, "It was blank, you are unharmed");
        }

        _context.TransitionTo<PlayerTurnState>();
    }

    private enum Target
    {
        You,
        Opponent
    }
}