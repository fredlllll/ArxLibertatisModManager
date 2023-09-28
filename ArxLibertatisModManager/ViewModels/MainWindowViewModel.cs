using ArxLibertatisModManager.Classes;
using ArxLibertatisModManager.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ArxLibertatisModManager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private PageEnum activePage = PageEnum.None;

        private readonly MainWindow mainWindow;

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.PropertyChanged += MainWindowViewModel_PropertyChanged;
        }

        private void MainWindowViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ActivePage))
            {
                mainWindow.NavConfiguration.SetActive(false);
                mainWindow.NavMods.SetActive(false);
                switch (ActivePage)
                {
                    case PageEnum.Mods:
                        mainWindow.NavMods.SetActive(true);
                        break;
                    case PageEnum.Configuration:
                        mainWindow.NavConfiguration.SetActive(true);
                        break;
                }
            }
        }

        public void ModsClicked()
        {
            ActivePage = PageEnum.Mods;
        }

        public void ConfigurationClicked()
        {
            ActivePage = PageEnum.Configuration;
        }
    }
}