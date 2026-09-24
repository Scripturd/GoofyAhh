using GoofyAhh.BuckshotRoulette.Items;
using GoofyAhh.Common;
using GoofyAhh.Mastermind;

namespace GoofyAhh.BuckshotRoulette;

public class DealerTurnState : IState
{
    private readonly UiService _uiService;
    private readonly Inventory _dealerInventory;
    private readonly GameUi _gameUi;
    private readonly Random _random;
    private readonly Shotgun _shotgun;

    public DealerTurnState(
        UiService uiService,
        Inventory dealerInventory,
        GameUi gameUi,
        Random random,
        Shotgun shotgun)
    {
        _uiService = uiService;
        _dealerInventory = dealerInventory;
        _gameUi = gameUi;
        _random = random;
        _shotgun = shotgun;
    }

    public void Start()
    {
        _uiService.PrintPause("Dealer's turn, press any key to continue");
        _uiService.WaitForKeyPress();
        _uiService.Clear();

        _gameUi.PrintInfo();

        IItem selectedItem = SelectItem();

        _uiService.PrintPause($"The dealer picks up the {selectedItem.Name}");

        UseItem(selectedItem);
    }

    public IItem SelectItem()
    {
        List<IItem> usableItems = [];
        usableItems.Add(_shotgun);
        usableItems.AddRange(_dealerInventory.Items);

        int selectedItemIndex = _random.Next(usableItems.Count);
        IItem selectedItem = usableItems[selectedItemIndex];
        return selectedItem;
    }
    public void UseItem(IItem item)
    {
        if (_dealerInventory.Contains(item))
            _dealerInventory.Remove(item);

        item.DealerUse();
    }
}