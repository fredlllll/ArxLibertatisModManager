using ArxLibertatisModManager.Classes;
using ArxLibertatisModManager.UserControls;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArxLibertatisModManager.ViewModels
{
    public partial class ModsPageViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ModViewModel> allMods = new();
        public ObservableCollection<ModViewModel> AllMods { get { return allMods; } }

        [ObservableProperty]
        private bool gameActive = false;

        [ObservableProperty]
        private bool pageActive = false;

        public async Task UpdateModsClicked()
        {
            await UpdateAllMods();
        }

        public async Task UpdateAllMods()
        {
            AllMods.Clear();
            var files = Directory.EnumerateFiles(ConfigurationPage.Instance.ViewModel.ModsFolder, "*.zip");
            foreach (var file in files)
            {
                var mod = new ModViewModel(file);
                await mod.LoadFromZip();
                if (mod.CanBeActivated)
                {
                    mod.Active = Directory.Exists(mod.ModCachePath);
                }
                AllMods.Add(mod);
            }
        }

        private GameProcess? gameProcess = null;

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

        public async void StartGameModsClicked()
        {
            gameProcess = new GameProcess(
                Path.Combine(ConfigurationPage.Instance.ViewModel.ArxLibertatisFolder, "arx.exe"),
                AllMods.Where(mod => mod.Active)
                );
            gameProcess.ProcessExit += GameProcess_ProcessExit;
            gameProcess.ProcessStart += GameProcess_ProcessStart;
            await gameProcess.StartWithMods();
        }

        private void GameProcess_ProcessExit()
        {
            GameActive = false;
        }

        private void GameProcess_ProcessStart()
        {
            GameActive = true;
        }

        public void StartGameClicked()
        {
            gameProcess = new GameProcess(
                Path.Combine(ConfigurationPage.Instance.ViewModel.ArxLibertatisFolder, "arx.exe"),
                Enumerable.Empty<ModViewModel>()
                );
            gameProcess.ProcessExit += GameProcess_ProcessExit;
            gameProcess.ProcessStart += GameProcess_ProcessStart;
            gameProcess.StartWithoutMods();
        }

        public void SelectAllClicked()
        {
            foreach (var mod in AllMods)
            {
                if (mod.CanBeActivated)
                {
                    mod.Active = true;
                }
            }
        }

        private void DeselectAllClicked()
        {
            foreach(var mod in AllMods)
            {
                mod.Active = false;
            }
        }
    }
}
