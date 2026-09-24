namespace GoofyAhh.BuckshotRoulette;

public class Health
{
    private int _current;

    public int Current
    {
        get => _current;
        private set
        {
            if (_current == value)
                return;

            _current = value;
            Changed?.Invoke();
        }
    }

    public bool IsAlive => Current > 0;

    public event Action? Changed;

    public Health(int amount)
    {
        _current = amount;
    }

    public void Damage(int amount = 1)
    {
        Current -= amount;
    }

    public void Heal(int amount = 1)
    {
        Current += amount;
    }
}