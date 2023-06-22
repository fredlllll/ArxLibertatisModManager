using ArxLibertatisModManager.Classes;
using ArxLibertatisModManager.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ArxLibertatisModManager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private PageEnum activePage = PageEnum.Mods;

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