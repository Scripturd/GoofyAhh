namespace GoofyAhh.BuckshotRoulette.Items;

public class Inventory
{
    private readonly List<IItem> _items = [];

    public IReadOnlyList<IItem> Items => _items;

    public void Add(IItem item)
        => _items.Add(item);

    public void Remove(IItem item)
        => _items.Remove(item);

    public bool Contains(IItem item)
        => _items.Contains(item);
}