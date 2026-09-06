using GoofyAhh.Common;
using System.Diagnostics;

namespace GoofyAhh.BuckshotRoulette;

internal class GameUi
{
    private readonly UiService _uiService;
    private readonly Player _player;
    private readonly Dealer _dealer;
    private readonly Shotgun _shotgun;

    private int _titleTop = 0;
    private int _healthTop = 15;
    private string _healthText = $"Your health: X. Dealer's health: X";

    private string _titleText;
    public string AmmoText { get; set; } = $"Lives: X. Blanks: X";

    public GameUi(
        UiService uiService,
        Player player,
        Dealer dealer,
        Shotgun shotgun)
    {
        _uiService = uiService;
        _player = player;
        _dealer = dealer;
        _shotgun = shotgun;

        _player.HealthChanged += UpdateHealthText;
        _dealer.HealthChanged += UpdateHealthText;

        _titleText = File.ReadAllText("C:\\Users\\08TOP001\\source\\repos\\GoofyAhh\\GoofyAhh\\BuckshotRoulette\\Title.txt");
    }

    public void Clear()
    {
        _uiService.Clear();

        _uiService.HorizontalBar();

        Scroll();
    }

    private void UpdateHealthText()
    {
        _healthText = $"Your health: {_player.Health}. Dealer's health: {_dealer.Health}";
        _uiService.ReplaceLine(_healthTop, _healthText);
    }

    public void UpdateAmmoText()
    {
        AmmoText = $"Lives: {_shotgun.LiveShellAmount}. Blanks: {_shotgun.BlankShellAmount}";
    }

    public void Scroll()
    {
        ScrollText(_titleText, _titleTop, 100000);
    }


    public void ScrollText(string text, int top, int durationMilliseconds)
    {
        string[] lines = text.Split(Environment.NewLine);

        int width = Console.WindowWidth - 1;
        int textWidth = lines.Max(line => line.Length);
        int gap = 50;
        int cycleLength = textWidth + gap;

        int delay = 50;

        Stopwatch stopwatch = Stopwatch.StartNew();

        int position = width;

        while (stopwatch.ElapsedMilliseconds < durationMilliseconds)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].PadRight(textWidth);

                // Two copies of the line with a gap between them.
                string scrollingLine = line + new string(' ', gap) + line;

                int offset = position % cycleLength;

                if (offset < 0)
                    offset += cycleLength;

                string frame = "";

                for (int x = 0; x < width; x++)
                {
                    int index = (offset + x) % cycleLength;
                    frame += scrollingLine[index];
                }

                _uiService.ReplaceLine(top + i, frame);
            }

            Thread.Sleep(delay);
            position--;

            if (position <= 0)
                position = cycleLength;
        }
    }
}