namespace GoofyAhh.BuckshotRoulette.Items;

public interface IItem
{
    string Name { get; }
    void PlayerUse();
    void DealerUse();
}