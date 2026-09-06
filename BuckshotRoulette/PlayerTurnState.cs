using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette;

internal class PlayerTurnState : IState
{
    private readonly Context _context;
    private readonly GameUi _gameUi; 
    private readonly UiService _uiService;
    private readonly Shotgun _shotgun;
    private readonly Player _player;
    private readonly Dealer _dealer;

    public PlayerTurnState(
        Context context,
        GameUi gameUi,
        UiService uiService,
        Shotgun shotgun,
        Player player, 
        Dealer dealer)
    {
        _context = context;
        _gameUi = gameUi;
        _uiService = uiService;
        _shotgun = shotgun;
        _player = player;
        _dealer = dealer;
    }

    public void Start()
    {
        _uiService.LoadingAnimation(3000, "Your turn");
        SelectTarget();
    }

    private void SelectTarget()
    {
        _gameUi.UpdateAmmoText();
        _gameUi.Clear();

        if (_shotgun.IsEmpty)
        {
            _uiService.LoadingAnimation(3000, "The gun is empty");
            _context.TransitionTo<ReloadState>();
        }

        _gameUi.Clear();
        
        Target selectedTarget = _uiService.SelectEnum<Target>("Select your target:");
        _uiService.LoadingAnimation(3000);

        _gameUi.Clear();

        _uiService.LoadingAnimation(3000, "You pull the trigger");

        if (selectedTarget == Target.You)
        {
            ShootAtSelf();
        }
        else
        {
            ShootAtOpponent();
        }
    }

    private void ShootAtSelf()
    {
        ShellType shellType = _shotgun.ShootAt(_player);
        if (shellType == ShellType.Live)
        {
            _uiService.LoadingAnimation(3000, "It was live, you shot yourself");

            if (!_player.IsAlive)
            {
                _context.TransitionTo<LoseState>();
                return;
            }

            _context.TransitionTo<DealerTurnState>();
        }
        else
        {
            _uiService.LoadingAnimation(3000, "It was blank, your turn again");
            SelectTarget();
        }
    }
    private void ShootAtOpponent()
    {
        ShellType shellType = _shotgun.ShootAt(_dealer);
        if (shellType == ShellType.Live)
        {
            _uiService.LoadingAnimation(3000, "It was live, you shot the dealer");

            if (!_dealer.IsAlive)
            {
                _context.TransitionTo<WinState>();
                return;
            }
        }
        else
        {
            _uiService.LoadingAnimation(3000, "It was blank, the dealer is unharmed");
        }

        _context.TransitionTo<DealerTurnState>();
    }

    private enum Target
    {
        You,
        Dealer
    }
}