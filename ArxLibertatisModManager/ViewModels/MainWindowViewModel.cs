using ArxLibertatisModManager.Classes;
using PropertyChanged.SourceGenerator;

namespace ArxLibertatisModManager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";
        [Notify]
        PageEnum activePage = PageEnum.Mods;

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