using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArxLibertatisModManager.ViewModels
{
    public partial class ModsPageViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ModViewModel> allMods = new();
        public ObservableCollection<ModViewModel> AllMods { get { return allMods; } }
    }
}
