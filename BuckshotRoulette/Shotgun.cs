namespace GoofyAhh.BuckshotRoulette;

internal class Shotgun
{
    private readonly Random _random;
    private readonly ShellSequence _shellSequence;

    public bool IsSawedOff { get; private set; }
    public bool IsEmpty => _shellSequence.IsEmpty;
    public int LiveShellAmount => _shellSequence.LiveShellAmount;
    public int BlankShellAmount => _shellSequence.BlankShellAmount;

    public Shotgun(Random random)
    {
        _random = random;
        _shellSequence = new ShellSequence(_random);
    }

    public ShellType ShootAt(IHealth target)
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
}