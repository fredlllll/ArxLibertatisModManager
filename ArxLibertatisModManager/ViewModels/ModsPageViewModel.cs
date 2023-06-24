using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArxLibertatisModManager.ViewModels
{
    public partial class ModsPageViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ModViewModel> allMods = new();
        public ObservableCollection<ModViewModel> AllMods { get { return allMods; } }

        public bool GameActive
        {
            get
            {
                if (gameProcess == null)
                {
                    return false;
                }
                return !gameProcess.HasExited;
            }
        }

        private Process? gameProcess = null;

        public ModsPageViewModel()
        {
            if (Design.IsDesignMode)
            {
                AllMods.Add(new ModViewModel("test.zip")
                {
                    Active = false,
                    Name = "test mod",
                    Description = "this mod does nothing",
                });
            }
        }

        public async void StartGameMods()
        {

        }

        public async void StartGame()
        {

        }
    }
}
