using GoofyAhh.BuckshotRoulette.Items;
using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette;

public class PlayerTurnState : IState
{
    private readonly UiService _uiService;
    private readonly Inventory _playerInventory;
    private readonly GameUi _gameUi;
    private readonly Shotgun _shotgun;

    public PlayerTurnState(
        UiService uiService,
        Inventory playerInventory,
        GameUi gameUi,
        Shotgun shotgun)
    {
        _uiService = uiService;
        _playerInventory = playerInventory;
        _gameUi = gameUi;
        _shotgun = shotgun;
    }

    public void Start()
    {
        _uiService.Print("Press any key to start your turn");
        _uiService.WaitForKeyPress();
        _uiService.Clear();

        _gameUi.PrintInfo();

        IItem selectedItem = SelectItem("Choose an item to use");
        UseItem(selectedItem);
    }

    public IItem SelectItem(string question)
    {
        List<IItem> usableItems = [];
        usableItems.Add(_shotgun);
        usableItems.AddRange(_playerInventory.Items);

        string[] itemNames = [.. usableItems.Select(x => x.Name)];
        int selectedItemIndex = _uiService.SelectString(question, itemNames);
        IItem selectedItem = usableItems[selectedItemIndex];
        return selectedItem;
    }
    public void UseItem(IItem item)
    {
        if (_playerInventory.Contains(item))
            _playerInventory.Remove(item);

        item.PlayerUse();
    }
}