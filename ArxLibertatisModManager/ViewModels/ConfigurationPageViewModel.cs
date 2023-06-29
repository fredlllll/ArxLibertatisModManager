using ArxLibertatisModManager.Views;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace ArxLibertatisModManager.ViewModels
{
    public partial class ConfigurationPageViewModel : ViewModelBase
    {
        private readonly OpenFolderDialog ofd = new OpenFolderDialog();

        [ObservableProperty]
        private string arxLibertatisFolder = "";

        public string ModsFolder { get { return Path.Combine(ArxLibertatisFolder, "mods"); } }
        public string ModsCacheFolder { get { return Path.Combine(ArxLibertatisFolder, "modscache"); } }

        public ConfigurationPageViewModel() : base(true)
        {

        }

        public async Task SearchFolderClicked()
        {
            var result = await ofd.ShowAsync(MainWindow.Instance);
            if (result != null)
            {
                ArxLibertatisFolder = result;
            }
        }
    }
}
