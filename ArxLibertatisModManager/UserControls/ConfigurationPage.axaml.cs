using ArxLibertatisModManager.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ArxLibertatisModManager.UserControls;

public partial class ConfigurationPage : UserControl
{

    public static ConfigurationPage Instance { get; set; }

    public ConfigurationPageViewModel ViewModel { get; private set; }

    public ConfigurationPage()
    {
        Instance = this;
        InitializeComponent();
        DataContext = ViewModel = new ConfigurationPageViewModel();
    }
}