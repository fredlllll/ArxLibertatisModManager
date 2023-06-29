using ArxLibertatisModManager.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ArxLibertatisModManager.UserControls;

public partial class ConfigurationPage : UserControl
{

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static ConfigurationPage Instance { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ConfigurationPageViewModel ViewModel { get; private set; }

    public ConfigurationPage()
    {
        Instance = this;
        InitializeComponent();
        DataContext = ViewModel = new ConfigurationPageViewModel();
        ViewModel.LoadFromDisk();
    }
}