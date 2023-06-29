using ArxLibertatisModManager.Classes;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System;

namespace ArxLibertatisModManager.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        private bool _loading = false;
        private readonly TriggerAfterTimeout tat;

        public ViewModelBase(bool persist = false)
        {
            tat = new TriggerAfterTimeout(500, SaveToDiskAsync);
            if (persist)
            {
                PropertyChanged += MyOnPropertyChanged;
            }
        }

        protected virtual void MyOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (!_loading)
            {
                tat.Trigger();
            }
        }

        /// <summary>
        /// The filename to be used to persist this instance
        /// </summary>
        /// <returns></returns>
        protected virtual string GetPersistenceFileName()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "persistence");
            Directory.CreateDirectory(dir);
            return Path.Combine(dir, $"{GetType().Name}.json");
        }

        protected virtual IEnumerable<PropertyInfo> GetPersistableProperties()
        {
            var props = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (!prop.CanWrite || !prop.CanWrite) //skip properties that only have one of the two acessors
                {
                    continue;
                }
                if (prop.GetCustomAttribute<IgnoreDataMemberAttribute>() != null)
                {
                    continue; //skip properties with ignoreDataMember attribute
                }
                yield return prop;
            }
        }

        public void SaveToDisk()
        {
            SaveToDiskAsync().ConfigureAwait(false);
        }

        public async Task SaveToDiskAsync()
        {
            JsonObject jobj = new();

            foreach (var prop in GetPersistableProperties())
            {
                if (prop.PropertyType == typeof(string))
                {
                    jobj[prop.Name] = JsonValue.Create<string>((string?)prop.GetValue(this));
                }
                else if (prop.PropertyType == typeof(int))
                {
                    jobj[prop.Name] = JsonValue.Create<int?>((int?)prop.GetValue(this));
                }
                else
                {
                    throw new Exception("unsupported type " + prop.PropertyType.Name);
                }
            }
            var filePath = GetPersistenceFileName();
            await File.WriteAllTextAsync(filePath, jobj.ToJsonString());
        }

        public void LoadFromDisk()
        {
            LoadFromDiskAsync().ConfigureAwait(false);
        }

        public async Task LoadFromDiskAsync()
        {
            try
            {
                _loading = true;
                var filePath = GetPersistenceFileName();
                if (!File.Exists(filePath))
                {
                    return;
                }
                var jsonString = await File.ReadAllTextAsync(filePath);
                var jnode = JsonNode.Parse(jsonString);
                if (jnode is JsonObject jobj)
                {
                    foreach (var prop in GetPersistableProperties())
                    {
                        prop.SetValue(this, jobj[prop.Name].Deserialize(prop.PropertyType));
                    }
                }
            }
            finally
            {
                _loading = false;
            }
        }
    }
}