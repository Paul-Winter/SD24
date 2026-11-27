using System.Xml;

namespace IDE_Booster;

public partial class MainForm : Form
{
    private readonly IdeManager _ideManager = new();
    private readonly ProcessManager _processManager = new();
    private readonly OptimizationManager _optimizationManager;

    public MainForm()
    {
        InitializeComponent();

        _optimizationManager = new OptimizationManager(_processManager);

        FormClosing += (sender, e) =>
        {
            _optimizationManager.Stop();
        };
    }

    private void boostButton_Click(object? sender, EventArgs e)
    {
        if (!_ideManager.IsIdeSelected())
        {
            MessageBox.Show("Сначала выберите IDE!");
            return;
        }

        if (_optimizationManager.IsActive)
        {
            MessageBox.Show("Оптимизация уже запущена.");
            return;
        }

        bool result = _optimizationManager.Start(
            _ideManager.SelectedIdeName
        );

        if (result)
        {
            statusLabel.Text = "Статус: буст активен";

            MessageBox.Show(
                "Приоритет IDE успешно повышен!",
                "IDE Booster"
            );
        }
        else
        {
            MessageBox.Show(
                "Не удалось найти запущенную IDE или изменить её приоритет. Запустите IDE и попробуйте снова.",
                "Ошибка"
            );
        }
    }

    private void stopButton_Click(object? sender, EventArgs e)
    {
        _optimizationManager.Stop();

        statusLabel.Text = "Статус: оптимизация остановлена";

        MessageBox.Show("Исходные приоритеты восстановлены.");
    }

    private void selectButton_Click(object? sender, EventArgs e)
    {
        if (_ideManager.SelectIde())
        {
            nameLabel.Text = _ideManager.GetIdeStatus();
            pathLabel.Text = $"Путь: {_ideManager.SelectedIdePath}";
        }
    }

    private void launchButton_Click(object? sender, EventArgs e)
    {
        if (!_ideManager.IsIdeSelected())
        {
            MessageBox.Show(
                "Сначала выберите IDE!",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );

            return;
        }

        if (_ideManager.LaunchIde())
        {
            MessageBox.Show("IDE успешно запущена!");
        }
        else
        {
            MessageBox.Show(
                "Не удалось запустить IDE.",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }
    }
}
