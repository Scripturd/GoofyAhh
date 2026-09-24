using GoofyAhh.BuckshotRoulette.Items;
using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette;

public class Shotgun : IItem
{
    private readonly Context _context;
    private readonly UiService _uiService;
    private readonly Random _random;
    private readonly ShellSequence _shellSequence;
    private readonly Health _playerHealth;
    private readonly Health _dealerHealth;

    public string Name => "Shotgun";
    public bool IsSawedOff { get; private set; }
    public bool IsEmpty => _shellSequence.IsEmpty;
    public int LiveShellAmount => _shellSequence.LiveShellAmount;
    public int BlankShellAmount => _shellSequence.BlankShellAmount;

    public Shotgun(
        Context context,
        UiService uiService,
        Random random,
        Health playerHealth,
        Health dealerHealth)
    {
        _context = context;
        _uiService = uiService;
        _random = random;
        _playerHealth = playerHealth;
        _dealerHealth = dealerHealth;
        _shellSequence = new ShellSequence(_random);
    }

    public void PlayerUse()
    {
        Target selectedTarget = _uiService.SelectEnum<Target>("Select your target:");
        _uiService.PrintPause("You pull the trigger");

        if (selectedTarget == Target.You)
        {
            PlayerShootAtPlayer();
        }
        else
        {
            PlayerShootAtDealer();
        }
    }
    private void PlayerShootAtPlayer()
    {
        ShellType shellType = ShootAt(_playerHealth);
        if (shellType == ShellType.Live)
        {
            _uiService.PrintPause("It was live");

            if (!_playerHealth.IsAlive)
            {
                _context.TransitionTo<LoseState>();
                return;
            }

            if (IsEmpty)
                _context.TransitionTo<ReloadState>();
            else
                _context.TransitionTo<DealerTurnState>();
        }
        else
        {
            _uiService.PrintPause("It was blank");

            if (IsEmpty)
                _context.TransitionTo<ReloadState>();
            else
                _context.TransitionTo<PlayerTurnState>();
        }
    }
    private void PlayerShootAtDealer()
    {
        ShellType shellType = ShootAt(_dealerHealth);
        if (shellType == ShellType.Live)
        {
            _uiService.PrintPause("It was live, you shot the dealer");

            if (!_dealerHealth.IsAlive)
            {
                _context.TransitionTo<WinState>();
                return;
            }
        }
        else
        {
            _uiService.PrintPause("It was blank, he is unharmed");
        }

        if (IsEmpty)
            _context.TransitionTo<ReloadState>();
        else
        _context.TransitionTo<DealerTurnState>();
    }


    public void DealerUse()
    {
        _uiService.PrintPause("The dealer selects his target");
        Target selectedTarget = (Target)_random.Next(0, 2);

        if (selectedTarget == Target.You)
        {
            _uiService.PrintPause("He chooses himself");
            _uiService.PrintPause("He pulls the trigger");

            DealerShootAtDealer();
        }
        else
        {
            _uiService.PrintPause("He chooses you");
            _uiService.PrintPause("He pulls the trigger");

            DealerShootAtPlayer();
        }
    }
    private void DealerShootAtDealer()
    {
        ShellType shellType = ShootAt(_dealerHealth);
        if (shellType == ShellType.Live)
        {
            _uiService.PrintPause("It was live, he shot himself");

            if (!_dealerHealth.IsAlive)
            {
                _context.TransitionTo<WinState>();
                return;
            }

            _context.TransitionTo<PlayerTurnState>();
        }
        else
        {
            _uiService.PrintPause("It was blank");

            _context.TransitionTo<DealerTurnState>();
        }
    }
    private void DealerShootAtPlayer()
    {
        ShellType shellType = ShootAt(_playerHealth);
        if (shellType == ShellType.Live)
        {
            _uiService.PrintPause("It was live, he shot you");

            if (!_playerHealth.IsAlive)
            {
                _context.TransitionTo<LoseState>();
                return;
            }
        }
        else
        {
            _uiService.PrintPause("It was blank, you are unharmed");
        }

        _context.TransitionTo<PlayerTurnState>();
    }

    public ShellType ShootAt(Health target)
    {
        ShellType firedShell = _shellSequence.Rack();
        if (firedShell == ShellType.Live)
        {
            int damage = IsSawedOff ? 2 : 1;
            target.Damage(damage);
        }

        IsSawedOff = false;

        return firedShell;
    }

    public ShellType Rack()
        => _shellSequence.Rack();

    public void SawOff()
        => IsSawedOff = true;

    public void Reload()
    {
        if (!_shellSequence.IsEmpty)
            throw new InvalidOperationException
                ("The gun must be empty before reloading");

        int shellAmount = _random.Next(2, 9);
        int liveShellsAmount = _random.Next(1, shellAmount);
        int blankShellsAmount = shellAmount - liveShellsAmount;

        List<ShellType> shells = [];

        for (int i = 0; i < liveShellsAmount; i++)
            shells.Add(ShellType.Live);

        for (int i = 0; i < blankShellsAmount; i++)
            shells.Add(ShellType.Blank);

        _shellSequence.Load(shells);
    }

    private enum Target
    {
        You,
        Dealer
    }
}