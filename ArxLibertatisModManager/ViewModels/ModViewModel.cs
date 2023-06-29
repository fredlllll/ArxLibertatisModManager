using ArxLibertatisModManager.UserControls;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Text.Json;
using ArxLibertatisModManager.Classes;

namespace ArxLibertatisModManager.ViewModels
{
    public partial class ModViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string name = "";
        [ObservableProperty]
        private string description = "";
        [ObservableProperty]
        private string author = "";
        private readonly string zipName;
        public string ZipName { get { return zipName; } }
        [ObservableProperty]
        private bool active = false;
        [ObservableProperty]
        private bool canBeActivated = true;

        public ModViewModel(string zipName)
        {
            this.zipName = zipName;
        }

        public async Task LoadFromZip()
        {
            using var archive = ZipFile.OpenRead(ZipPath);
            var entry = archive.GetEntry("manifest.json");
            if (entry == null)
            {
                CanBeActivated = false;
                Name = "Error";
                Description = "No manifest.json found";
                return;
            }
            using var entryStream = entry.Open();
            try
            {
                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var manifest = JsonSerializer.Deserialize<ModManifest>(entryStream, serializeOptions) ?? throw new Exception();
                Name = manifest.Name;
                Description = manifest.Description;
                Author = manifest.Author;
            }
            catch
            {
                CanBeActivated = false;
                Name = "Error";
                Description = "Could not load manifest.json";
                return;
            }
        }


        public string ZipPath { get { return Path.Combine(ConfigurationPage.Instance.ViewModel.ModsFolder, ZipName); } }

        public string ModCachePath { get { return Path.Combine(ConfigurationPage.Instance.ViewModel.ModsCacheFolder, Name); } }
    }
}
