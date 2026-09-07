using Spectre.Console;
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
    private int _ammoTop = 5;

    private Table _healthTable;

    private string _titleText;
    private string _ammoText = $"Lives: X. Blanks: X";

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

        _titleText = File.ReadAllText("C:\\Users\\08TOP001\\source\\repos\\GoofyAhh\\GoofyAhh\\BuckshotRoulette\\Title.txt");
    }

    public void Clear()
    {
        //_uiService.Clear();

        _uiService.Space(10);

        UpdateAmmoText();

        _uiService.HorizontalBar();

        //Scroll();
    }

    public void CreateLiveHealthTable(Action game)
    {
        _healthTable = CreateHealthTable();
        AnsiConsole.Live(_healthTable)
        .Start(ctx =>
        {
            _player.HealthChanged += () => UpdateHealthTable(ctx);
            _dealer.HealthChanged += () => UpdateHealthTable(ctx);

            game.Invoke();
        });
    }
    private Table CreateHealthTable()
    {
        Table table = new();

        table.AddColumn("Your Health:");
        table.AddColumn("Dealer's Health:");

        table.AddRow(
            _player.Health.ToString(),
            _dealer.Health.ToString());

        return table;
    }
    private void UpdateHealthTable(LiveDisplayContext tableContext)
    {
        _healthTable.UpdateCell(0, 0, _player.Health.ToString());
        _healthTable.UpdateCell(0, 1, _dealer.Health.ToString());

        tableContext.Refresh();
    }

    public void UpdateAmmoText()
    {
        _ammoText = $"Lives: {_shotgun.LiveShellAmount}. Blanks: {_shotgun.BlankShellAmount}";
        _uiService.ReplaceLine(_ammoTop, _ammoText);
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