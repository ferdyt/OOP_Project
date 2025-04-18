using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace OOP_Project;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        string path = "C:\\Users\\Csgo2\\Source\\Repos\\OOP_Project\\OOP_Project\\Databases\\Current.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
