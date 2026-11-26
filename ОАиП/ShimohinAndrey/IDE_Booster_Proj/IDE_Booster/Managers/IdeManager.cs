
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace IDE_Booster;

public class IdeManager
{
    public string SelectedIdePath { get; private set; } = "";
    public string SelectedIdeName { get; private set; } = "";

    public bool SelectIde()
    {
        using OpenFileDialog dialog = new OpenFileDialog();

        dialog.Title = "Выберите IDE";
        dialog.Filter = "Исполняемые файлы (*.exe)|*.exe";
        dialog.Multiselect = false;

        if (dialog.ShowDialog() != DialogResult.OK)
            return false;

        SelectedIdePath = dialog.FileName;
        SelectedIdeName = Path.GetFileNameWithoutExtension(
            SelectedIdePath
        );

        return true;
    }

    public bool IsIdeSelected()
    {
        return !string.IsNullOrWhiteSpace(SelectedIdePath)
            && File.Exists(SelectedIdePath);
    }

    public bool LaunchIde()
    {
        if (!IsIdeSelected())
            return false;

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = SelectedIdePath,
                UseShellExecute = true
            });

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string GetIdeStatus()
    {
        if (!IsIdeSelected())
            return "IDE не выбрана";

        return $"Выбрана IDE: {SelectedIdeName}";
    }
}
