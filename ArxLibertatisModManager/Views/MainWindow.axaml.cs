using ArxLibertatisModManager.ViewModels;
using Avalonia.Controls;

namespace ArxLibertatisModManager.Views
{
    public partial class MainWindow : Window
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static MainWindow Instance { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public MainWindow()
        {
            Instance = this;

            InitializeComponent();

            DataContext = new MainWindowViewModel();

            //NavMods.DataContext = DataContext;
            NavMods.LblTitle.Content = "Mods"; // jesus fucking christ why does this work in the other project with the Title in xaml but not here???
            //NavConfiguration.DataContext = DataContext;
            NavConfiguration.LblTitle.Content = "Configuration";
        }
    }
}