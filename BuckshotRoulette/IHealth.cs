namespace GoofyAhh.BuckshotRoulette;

internal interface IHealth
{
    public bool IsAlive { get; }
    public int Health { get; }

    public void Damage(int amount = 1);
    public void Heal(int amount = 1);
}