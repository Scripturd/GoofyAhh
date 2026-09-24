using GoofyAhh.Common;

namespace GoofyAhh.BuckshotRoulette;

public class GameUi
{
    private readonly UiService _uiService;
    private readonly Shotgun _shotgun;
    private readonly Health _playerHealth;
    private readonly Health _dealerHealth;

    public GameUi(
        UiService uiService,
        Shotgun shotgun,
        Health playerHealth,
        Health dealerHealth)
    {
        _uiService = uiService;
        _shotgun = shotgun;
        _playerHealth = playerHealth;
        _dealerHealth = dealerHealth;
    }

    public void PrintInfo()
    {
        string playerHealthBar = "";
        for (int i = 0; i < _playerHealth.Current; i++)
        {
            playerHealthBar += "X";
        }

        string dealerHealthBar = "";
        for (int i = 0; i < _dealerHealth.Current; i++)
        {
            dealerHealthBar += "X";
        }

        string liveShellBar = "";
        for (int i = 0; i < _shotgun.LiveShellAmount; i++)
        {
            liveShellBar += "|";
        }

        string blankShellBar = "";
        for (int i = 0; i < _shotgun.BlankShellAmount; i++)
        {
            blankShellBar += "|";
        }

        _uiService.Print($"Your health: {playerHealthBar}");
        _uiService.Print($"Dealer's health: {dealerHealthBar}");
        _uiService.Space();
        _uiService.Print($"Lives:  [ {liveShellBar} ]");
        _uiService.Print($"Blanks: [ {blankShellBar} ]");
        if (_shotgun.IsSawedOff)
            _uiService.Print($"The barrel is sawed off, the gun deals 2 damage.");
        else
            _uiService.Space();
        _uiService.HorizontalBar();
    }
}