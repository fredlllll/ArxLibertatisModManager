using ArxLibertatisModManager.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.IO;

namespace ArxLibertatisModManager.UserControls;

public partial class ModsPage : UserControl
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static ModsPage Instance { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public ModsPageViewModel ViewModel { get; private set; }

    public ModsPage()
    {
        Instance = this;
        InitializeComponent();
        DataContext = ViewModel = new ModsPageViewModel();
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (!Design.IsDesignMode)
        {
            ConfigurationPage.Instance.ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }
    }

    private async void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ConfigurationPageViewModel.ArxLibertatisFolder))
        {
            if (Directory.Exists(ConfigurationPage.Instance.ViewModel.ArxLibertatisFolder))
            {
                await ViewModel.UpdateAllMods();
            }
        }
    }
}