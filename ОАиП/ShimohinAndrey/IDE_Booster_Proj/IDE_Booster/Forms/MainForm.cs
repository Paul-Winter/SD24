using System.Xml;

namespace IDE_Booster;

public partial class MainForm : Form
{
    private readonly IdeManager _ideManager = new();

    public MainForm()
    {
        InitializeComponent();
    }

    // Выбор IDE
    private void selectButton_Click(object? sender, EventArgs e)
    {
        if (_ideManager.SelectIde())
        {
            nameLabel.Text = _ideManager.GetIdeStatus();
            pathLabel.Text = $"Путь: {_ideManager.SelectedIdePath}";
        }
    }

    // Запуск IDE
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
