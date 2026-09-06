namespace GoofyAhh.BuckshotRoulette;

public class Dealer : IHealth
{
    private int _health;

    public int Health
    {
        get => _health;
        private set
        {
            if (_health == value)
                return;

            _health = value;
            HealthChanged?.Invoke();
        }
    }

    public bool IsAlive => Health > 0;

    public event Action? HealthChanged;

    public Dealer(int health)
    {
        _health = health;
    }

    public void Damage(int amount = 1)
    {
        Health -= amount;
    }

    public void Heal(int amount = 1)
    {
        Health += amount;
    }
}