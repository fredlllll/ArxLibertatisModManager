using ArxLibertatisModManager.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ArxLibertatisModManager.UserControls;

public partial class ModsPage : UserControl
{
    public static ModsPage Instance { get; private set; }

    public ModsPage()
    {
        Instance = this;
        InitializeComponent();
        DataContext = new ModsPageViewModel();
    }
}