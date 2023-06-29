using ArxLibertatisModManager.Classes;
using ArxLibertatisModManager.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ArxLibertatisModManager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ModsPageActive))]
        private PageEnum activePage = PageEnum.Mods;

        [ObservableProperty]
        private bool modsPageActive = false;

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