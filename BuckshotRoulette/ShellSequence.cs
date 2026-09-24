namespace GoofyAhh.BuckshotRoulette;

public class ShellSequence
{
    private readonly Random _random;
    private List<ShellType> _shells = [];

    public ShellType ChamberedShell => _shells.FirstOrDefault();
    public bool IsEmpty => _shells.Count == 0;
    public int LiveShellAmount { get; private set; }
    public int BlankShellAmount { get; private set; }

    public ShellSequence(Random random)
    {
        _random = random;
    }

    public ShellType Rack()
    {
        if (IsEmpty)
            throw new InvalidOperationException
                ("The gun is empty");

        ShellType ejectedShell = ChamberedShell;

        _shells.RemoveAt(0);

        if (ejectedShell == ShellType.Live)
            LiveShellAmount--;
        else
            BlankShellAmount--;

        return ejectedShell;
    }

    public void Load(List<ShellType> shells)
    {
        _shells = [.. shells.OrderBy(_ => _random.Next())];

        LiveShellAmount = _shells.Count(shell => shell == ShellType.Live);
        BlankShellAmount = _shells.Count(shell => shell == ShellType.Blank);
    }
}